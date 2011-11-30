namespace IntergalacticControls.PopupUI.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticControls.Classes;
    using IntergalacticControls.PopupUI.Notifications.Base;
    using IntergalacticCore;

    /// <summary>
    /// Interaction logic for LoadingNotificationView.xaml
    /// </summary>
    public partial class LoadingNotificationView : NotificationView
    {
        /// <summary>
        /// Initializes a new instance of the LoadingNotificationView class
        /// </summary>
        public LoadingNotificationView()
        {
            InitializeComponent();

            DoubleAnimation animtion = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(4));
            animtion.RepeatBehavior = RepeatBehavior.Forever;

            RotateTransform transform = new RotateTransform();

            this.loadingIndecator.RenderTransformOrigin = new Point(0.5, 0.5);
            this.loadingIndecator.RenderTransform = transform;

            transform.BeginAnimation(RotateTransform.AngleProperty, animtion);

            this.DisplayTimeout = null;
            this.AnimationType = NotificationAnimationType.Fade;

            Manager.Instance.OnOperationFinshed += this.HideNotification;
        }

        /// <summary>
        /// Linked to the OnOperationFinished event in the manager to hide the notification
        /// </summary>
        /// <param name="mng">The manager</param>
        /// <param name="operation">The operation</param>
        public void HideNotification(Manager mng, BaseOperation operation)
        {
            imgCheck.Visibility = System.Windows.Visibility.Visible;
            loadingIndecator.Visibility = System.Windows.Visibility.Hidden;
            lblTitle.Content = "Done in " + operation.OperatingTime.Seconds + "." + (operation.OperatingTime.Milliseconds / 100) + " Seconds.";

            UIHelpers.CallFunctionAfterDelay(3, this.Dispatcher, new Action(this.CloseNotification));
            Manager.Instance.OnOperationFinshed -= this.HideNotification;
        }
    }
}
