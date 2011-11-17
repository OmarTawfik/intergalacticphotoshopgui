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

namespace IPUI
{
    /// <summary>
    /// Interaction logic for SubmenuContainer.xaml
    /// </summary>
    public partial class SubmenuContainer : UserControl
    {
        public SubmenuContainer()
        {
            InitializeComponent();
        }

        public void SetCategory(OperationCategory category)
        {
            lblTitle.Content = category.Title;
            menusPanel.Children.Clear();

            for (int i = 0; i < category.OperationsList.Count; i++)
            {
                SubMenu submenu = new SubMenu();
                submenu.SetMenuData(category.OperationsList[i]);
                submenu.MouseUp += new MouseButtonEventHandler(Submenu_MouseUp);
                menusPanel.Children.Add(submenu);
            }
        }

        void  Submenu_MouseUp(object sender, MouseButtonEventArgs e)
        {
 	        ////TODO: Call the popup manager to bring on the operation input panel
        }
    }
}
