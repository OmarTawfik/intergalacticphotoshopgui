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
    /// Forms the base of stack controllers
    /// </summary>
    public class StackControllerBase : UserControl
    {
        /// <summary>
        /// Fade in/out animations
        /// </summary>
        private DoubleAnimation fadeIn, fadeOut;

        /// <summary>
        /// The primary stack panel to use
        /// </summary>
        private StackPanel mainStackPanel = null;

        /// <summary>
        /// Initializes a new instance of the StackControllerBase class
        /// </summary>
        public StackControllerBase()
        {
            this.Opacity = 0.4;

            this.fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3));
            this.fadeIn.AccelerationRatio = 0.3;
            this.fadeIn.DecelerationRatio = 0.3;
            this.fadeOut = new DoubleAnimation(0.4, TimeSpan.FromSeconds(0.3));
            this.fadeOut.AccelerationRatio = 0.3;
            this.fadeOut.DecelerationRatio = 0.3;
        }

        /// <summary>
        /// Gets or sets the main stack panel
        /// </summary>
        protected StackPanel MainStackPanel
        {
            get { return this.mainStackPanel; }
            set { this.mainStackPanel = value; }
        }

        /// <summary>
        /// Adds a stack button to this controls
        /// </summary>
        /// <param name="button">The button</param>
        public void AddButton(StackButtonBase button)
        {
            button.IsVertical = this.mainStackPanel.Orientation == Orientation.Vertical;
            this.mainStackPanel.Children.Add(button);
            button.MouseEnter += new MouseEventHandler(this.PanelButton_MouseEnter);
        }

        /// <summary>
        /// MouseEnter function to add animation
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.BeginAnimation(UserControl.OpacityProperty, this.fadeIn);
        }

        /// <summary>
        /// MouseLeave function to add animation
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.BeginAnimation(UserControl.OpacityProperty, this.fadeOut);
        }

        /// <summary>
        /// MouseEnter function for the stack buttons added to this control
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void PanelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StackButtonBase source = sender as StackButtonBase;
            PopupViewManager.CurrentPopupManager.ViewPopup(source);
        }
    }
}
