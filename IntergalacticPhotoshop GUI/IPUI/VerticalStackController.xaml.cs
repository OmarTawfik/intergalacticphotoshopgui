
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
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for HorizontalStackController.xaml
    /// </summary>
    public partial class VerticalStackController : UserControl
    {
        DoubleAnimation fadeIn, fadeOut;

        public VerticalStackController()
        {
            InitializeComponent();

            this.Opacity = 0.4;

            this.fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3));
            this.fadeIn.AccelerationRatio = 0.3;
            this.fadeIn.DecelerationRatio = 0.3;
            this.fadeOut = new DoubleAnimation(0.4, TimeSpan.FromSeconds(0.3));
            this.fadeOut.AccelerationRatio = 0.3;
            this.fadeOut.DecelerationRatio = 0.3;
        }

        public void AddPanelButton(PanelButton button)
        {
            button.IsVertical = true;
            this.mainStackPanel.Children.Add(button);
            button.MouseEnter += new MouseEventHandler(this.PanelButton_MouseEnter);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.BeginAnimation(UserControl.OpacityProperty, fadeIn);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.BeginAnimation(UserControl.OpacityProperty, fadeOut);
        }

        private void PanelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            PanelButton source = sender as PanelButton;
            PopupViewManager.CurrentPopupManager.ViewPopup(source);
        }
    }
}
