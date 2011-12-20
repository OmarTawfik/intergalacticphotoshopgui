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
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticControls.PopupUI.Notifications.Base;

    /// <summary>
    /// Interaction logic for SideNotification.xaml
    /// </summary>
    public partial class SideNotification : NotificationView
    {
        /// <summary>
        /// Initializes a new instance of the SideNotification class
        /// </summary>
        public SideNotification()
        {
            InitializeComponent();
            this.BlocksUI = false;
            this.DisplayTimeout = 7;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.Margin = new Thickness(10, 10, 10, 10);
        }

        /// <summary>
        /// Sets the title of the notification view
        /// </summary>
        /// <param name="title">The title</param>
        public void SetTitle(string title)
        {
            this.lblTitle.Text = title;
        }

        /// <summary>
        /// Closes the notification.  For the close button.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.CloseNotification();
        }
    }
}
