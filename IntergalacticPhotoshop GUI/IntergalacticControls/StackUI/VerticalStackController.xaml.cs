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
    /// Interaction logic for HorizontalStackController.xaml
    /// </summary>
    public partial class VerticalStackController : StackControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the VerticalStackController class
        /// </summary>
        public VerticalStackController() : base()
        {
            InitializeComponent();
            this.MainStackPanel = this.stackPanel;
        }
    }
}
