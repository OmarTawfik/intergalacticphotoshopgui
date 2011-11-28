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
        /// The label title
        /// </summary>
        private Label lblTitle;

        /// <summary>
        /// Initializes a new instance of the SubmenuContainer class
        /// </summary>
        public SubmenuContainer()
        {
            InitializeComponent();

            this.lblTitle = new Label();
            this.lblTitle.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.lblTitle.Content = "Title of The menu";
            this.lblTitle.FontSize = 18;
            this.lblTitle.Height = 35;
            this.IsTitleShown = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the title is shown
        /// </summary>
        public bool IsTitleShown
        {
            get
            {
                return this.menusPanel.Children.Contains(this.lblTitle);
            }

            set
            {
                if (value)
                {
                    if (!this.menusPanel.Children.Contains(this.lblTitle))
                    {
                        this.menusPanel.Children.Insert(0, this.lblTitle);
                    }
                }
                else
                {
                    if (this.menusPanel.Children.Contains(this.lblTitle))
                    {
                        this.menusPanel.Children.Remove(this.lblTitle);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the category of the menu
        /// </summary>
        /// <param name="category">The category</param>
        public void SetCategory(OperationCategory category)
        {
            this.lblTitle.Content = category.Title;

            if (this.IsTitleShown)
            {
                this.menusPanel.Children.RemoveRange(1, this.menusPanel.Children.Count - 1);
            }
            else
            {
                this.menusPanel.Children.Clear();
            }

            for (int i = 0; i < category.Count; i++)
            {
                SubMenu submenu = new SubMenu();
                submenu.Height = 28;
                submenu.SetMenuData(category.GetOperationAtIndex(i));
                submenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                submenu.MouseUp += new MouseButtonEventHandler(this.Submenu_MouseUp);
                menusPanel.Children.Add(submenu);
            }

            this.Height = (category.Count * 28) + (this.IsTitleShown ? this.lblTitle.Height : 0);
        }

        /// <summary>
        /// Sets the actions of the menu
        /// </summary>
        /// <param name="category">The category</param>
        public void SetCategory(ActionCategory category)
        {
            this.lblTitle.Content = category.Title;

            if (this.IsTitleShown)
            {
                this.menusPanel.Children.RemoveRange(1, this.menusPanel.Children.Count - 1);
            }
            else
            {
                this.menusPanel.Children.Clear();
            }

            for (int i = 0; i < category.Count; i++)
            {
                SubMenu submenu = new SubMenu();
                submenu.Height = 28;
                submenu.SetMenuData(category.GetActionAtIndex(i), category.GetTitleAtIndex(i));
                submenu.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                submenu.MouseUp += new MouseButtonEventHandler(this.Submenu_MouseUp);
                menusPanel.Children.Add(submenu);
            }

            this.Height = (category.Count * 28) + (this.IsTitleShown ? this.lblTitle.Height : 0);
        }

        /// <summary>
        /// MouseUp function to hide the popup after click
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void Submenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UIManager.CurrentPopupManager.HideCurrentPopup();
        }
    }
}
