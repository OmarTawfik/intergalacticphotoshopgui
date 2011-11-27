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
    /// Interaction logic for PanelButton.xaml
    /// </summary>
    public partial class PanelButton : StackButtonBase
    {
        /// <summary>
        /// Button icon
        /// </summary>
        private ImageSource icon;

        /// <summary>
        /// Button color
        /// </summary>
        private Color buttonColor;

        /// <summary>
        /// Initializes a new instance of the PanelButton class
        /// </summary>
        public PanelButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the icon of the button
        /// </summary>
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

        /// <summary>
        /// Gets or sets the color of the button
        /// </summary>
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
    }
}
