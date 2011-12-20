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
        }

        /// <summary>
        /// Linked to the OnOperationFinished event in the manager to hide the notification
        /// </summary>
        /// <param name="mng">The manager</param>
        /// <param name="operation">The operation</param>
        public void HideNotification(Manager mng, BaseOperation operation)
        {
            loadingIndecator.Visibility = System.Windows.Visibility.Hidden;
            if (mng.CurrentTab.DidOperationComplete)
            {
                imgCheck.Visibility = System.Windows.Visibility.Visible;
                lblTitle.Content = "Done!";
                SideNotification finishingNotification = new SideNotification();
                finishingNotification.SetTitle("Done in " + operation.OperatingTime.Seconds + "." + (operation.OperatingTime.Milliseconds / 100) + " Seconds.");
                finishingNotification.DisplayTimeout = 3;
                finishingNotification.AnimationType = NotificationAnimationType.Fade;
                finishingNotification.ShowNotification();
            }
            else
            {
                imgError.Visibility = System.Windows.Visibility.Visible;
                lblTitle.Content = "Error!";
                SideNotification notification = new SideNotification();
                notification.Height = 150;
                notification.SetTitle("Error while performing operation: " + mng.CurrentTab.LastException.Message);
                notification.ShowNotification();
            }

            this.CloseNotification();
        }
    }
}
