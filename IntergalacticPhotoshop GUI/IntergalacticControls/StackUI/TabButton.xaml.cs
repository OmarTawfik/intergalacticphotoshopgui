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
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticControls.Classes;

    /// <summary>
    /// Interaction logic for TabButton.xaml
    /// </summary>
    public partial class TabButton : StackButtonBase
    {
        /// <summary>
        /// Tab name
        /// </summary>
        private Tab tab;

        /// <summary>
        /// Initializes a new instance of the TabButton class
        /// </summary>
        /// <param name="tab">The tab</param>
        public TabButton(Tab tab)
        {
            InitializeComponent();
            this.tab = tab;
            this.SubView = new Image();
            this.SubView.Width = 250;
            this.SubView.Height = 150;
            this.lblTitle.Content = tab.Name;

            if (tab.CanBeDeleted == false)
            {
                this.CloseBtn.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Gets or sets the tab name
        /// </summary>
        public Tab Tab
        {
            get { return this.tab; }
            set { this.tab = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this tab is selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.toggleRect.Visibility == System.Windows.Visibility.Visible;
            }

            set
            {
                if (value)
                {
                    this.toggleRect.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this.toggleRect.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Gets called when the user presses close on the tab.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="e">Mouse Button Events Arguments.</param>
        private void CloseBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Manager.Instance.DeleteTab(this.tab.Name);
        }
    }
}
