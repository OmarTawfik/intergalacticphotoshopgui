
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
    using IntergalacticControls.PopupUI.Notifications.Base;

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
        }
    }
}
