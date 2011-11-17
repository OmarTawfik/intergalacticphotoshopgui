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
using IntergalacticCore2;
using IntergalacticCore2.Data;

namespace IPUI
{
    /// <summary>
    /// Interaction logic for SubMenu.xaml
    /// </summary>
    public partial class SubMenu : UserControl
    {
        private BaseOperation imageProcess;

        public BaseOperation ImageProcess
        {
            get { return this.imageProcess; }
        }

        private DoubleAnimation fadeIn, fadeOut;
        public SubMenu()
        {
            InitializeComponent();
            this.fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
            this.fadeOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.1));
        }

        public void SetMenuData(BaseOperation operation)
        {
            this.lblMenuTitle.Content = operation.Title;
            this.imageProcess = operation;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            rectHover.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            rectHover.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }
    }
}
