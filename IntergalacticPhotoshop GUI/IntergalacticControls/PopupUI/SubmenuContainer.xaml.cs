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

    /// <summary>
    /// Interaction logic for SubmenuContainer.xaml
    /// </summary>
    public partial class SubmenuContainer : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the SubmenuContainer class
        /// </summary>
        public SubmenuContainer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the category of the menu
        /// </summary>
        /// <param name="category">The category</param>
        public void SetCategory(OperationCategory category)
        {
            lblTitle.Content = category.Title;
            menusPanel.Children.Clear();

            for (int i = 0; i < category.Count; i++)
            {
                SubMenu submenu = new SubMenu();
                submenu.SetMenuData(category.GetOperationAtIndex(i));
                submenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                submenu.MouseUp += new MouseButtonEventHandler(this.Submenu_MouseUp);
                menusPanel.Children.Add(submenu);
            }

            this.Height = (category.Count * 28) + 50;
        }

        /// <summary>
        /// Sets the actions of the menu
        /// </summary>
        /// <param name="category">The category</param>
        public void SetCategory(ActionCategory category)
        {
            lblTitle.Content = category.Title;
            menusPanel.Children.Clear();

            for (int i = 0; i < category.Count; i++)
            {
                SubMenu submenu = new SubMenu();
                submenu.SetMenuData(category.GetActionAtIndex(i), category.GetTitleAtIndex(i));
                submenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                submenu.MouseUp += new MouseButtonEventHandler(this.Submenu_MouseUp);
                menusPanel.Children.Add(submenu);
            }

            this.Height = (category.Count * 28) + 50;
        }

        /// <summary>
        /// MouseUp function to hide the popup after click
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void Submenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PopupViewManager.CurrentPopupManager.HideCurrentPopup();
        }
    }
}
