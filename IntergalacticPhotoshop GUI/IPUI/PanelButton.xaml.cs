
namespace IPUI
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
    /// Interaction logic for PanelButton.xaml
    /// </summary>
    public partial class PanelButton : UserControl
    {
        private bool isVertical;
        private bool isLockable = false;
        private Control subView;
        private OperationCategory category = null;
        private string title;
        private ImageSource icon;
        private Color buttonColor;

        public PanelButton()
        {
            InitializeComponent();
        }

        public Control SubView
        {
            get { return this.subView; }
            set { this.subView = value; }
        }

        public OperationCategory Category
        {
            get { return this.category; }
            set { this.category = value; }
        }

        public bool IsLockable
        {
            get { return this.isLockable; }
            set { this.isLockable = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public ImageSource Icon
        {
            get
            {
                return this.icon;
            }

            set
            {
                this.icon = value;
                this.imgIcon.Source = value;
            }
        }

        public Color ButtonColor
        {
            get
            {
                return this.buttonColor;
            }

            set
            {
                this.backRect.Fill = new SolidColorBrush(value);
                this.buttonColor = value;
            }
        }

        internal bool IsVertical
        {
            get { return this.isVertical; }
            set { this.isVertical = value; }
        }
    }
}
