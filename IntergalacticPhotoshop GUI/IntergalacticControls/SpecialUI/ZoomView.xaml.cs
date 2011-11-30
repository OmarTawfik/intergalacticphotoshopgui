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
    /// Interaction logic for ZoomView.xaml
    /// </summary>
    public partial class ZoomView : UserControl
    {
        /// <summary>
        /// The targeted image view
        /// </summary>
        private Image targetedImage;

        /// <summary>
        /// The scale transform used to scale the image view
        /// </summary>
        private ScaleTransform scaler;

        /// <summary>
        /// Initializes a new instance of the ZoomView class
        /// </summary>
        /// <param name="target">Targeted image view</param>
        public ZoomView(Image target)
        {
            InitializeComponent();
            this.targetedImage = target;
            this.scaler = new ScaleTransform();
            this.targetedImage.RenderTransformOrigin = new Point(0.5, 0.5);
            this.targetedImage.RenderTransform = this.scaler;

            Binding scaleBind = new Binding();
            scaleBind.Source = this.zoomSlider;
            scaleBind.Path = new PropertyPath(Slider.ValueProperty);

            BindingOperations.SetBinding(this.scaler, ScaleTransform.ScaleXProperty, scaleBind);
            BindingOperations.SetBinding(this.scaler, ScaleTransform.ScaleYProperty, scaleBind);
        }

        /// <summary>
        /// To move the image around
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Released)
            {
                return;
            }

            double centerX = e.GetPosition(this).X / this.ActualWidth;
            double centerY = e.GetPosition(this).Y / this.ActualHeight;

            this.targetedImage.RenderTransformOrigin = new Point(centerX, centerY);
        }
    }
}
