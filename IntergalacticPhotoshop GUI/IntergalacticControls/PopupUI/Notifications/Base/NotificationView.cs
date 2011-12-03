namespace IntergalacticControls.PopupUI.Notifications.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Has the style of the notificaition view
    /// </summary>
    public class NotificationView : UserControl
    {
        /// <summary>
        /// Time until the notification is hidden
        /// </summary>
        private double? displayTimeout;

        /// <summary>
        /// Indicates whether this notification blocks UI.
        /// </summary>
        private bool blocksUI;

        /// <summary>
        /// Notification show/hide animation type
        /// </summary>
        private NotificationAnimationType animationType;

        /// <summary>
        /// Initializes a new instance of the NotificationView class
        /// </summary>
        public NotificationView()
        {
            LinearGradientBrush brush = new LinearGradientBrush(Colors.White, Color.FromArgb(255, 208, 208, 208), new Point(0, 0), new Point(0, 1));
            DropShadowEffect effect = new DropShadowEffect();
            effect.BlurRadius = 30;
            effect.Opacity = 0.5;

            this.Effect = effect;
            this.Background = brush;

            this.animationType = NotificationAnimationType.Fade;
            this.displayTimeout = 3;

            this.blocksUI = true;
        }

        /// <summary>
        /// Gets or sets the animation type of the notification
        /// </summary>
        public NotificationAnimationType AnimationType
        {
            get { return this.animationType; }
            set { this.animationType = value; }
        }

        /// <summary>
        /// Gets or sets the display timeout
        /// </summary>
        public double? DisplayTimeout
        {
            get { return this.displayTimeout; }
            set { this.displayTimeout = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this notification blocks UI
        /// </summary>
        public bool BlocksUI
        {
            get { return this.blocksUI; }
            set { this.blocksUI = value; }
        }

        /// <summary>
        /// Shows the current notification
        /// </summary>
        public virtual void ShowNotification()
        {
            UIManager.CurrentUIManager.ViewNotification(this, this.animationType, this.displayTimeout);
        }

        /// <summary>
        /// Closes this notification
        /// </summary>
        protected virtual void CloseNotification()
        {
            UIManager.CurrentUIManager.CloseNoification(this);
        }
    }
}
