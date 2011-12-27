namespace IntergalacticControls
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

    /// <summary>
    /// Interaction logic for WelcomeControl.xaml
    /// </summary>
    public partial class WelcomeControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the WelcomeControl class
        /// </summary>
        public WelcomeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the action to open images
        /// </summary>
        public Action OpenAnImageAction
        {
            get;
            set;
        }

        /// <summary>
        /// Opens the image.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event args</param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            this.OpenAnImageAction();
        }
    }
}
