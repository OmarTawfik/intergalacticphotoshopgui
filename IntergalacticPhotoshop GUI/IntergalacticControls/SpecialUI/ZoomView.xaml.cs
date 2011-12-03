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
    using IntergalacticControls;
    using IntergalacticControls.Classes;
    using IntergalacticCore;

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

            Manager.Instance.OnNewTabAdded += this.ImageUpdated;
            Manager.Instance.OnTabChanged += this.ImageUpdated;
            Manager.Instance.OnOperationFinshed += this.ImageUpdated;
            this.centerRect.MouseMove += this.CenterRect_MouseMove;
        }

        /// <summary>
        /// MouseMove move event to update the zoomRect and the zoom center
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void CenterRect_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                return;
            }

            double centerX = e.GetPosition(this).X / this.Width;
            double centerY = e.GetPosition(this).Y / (this.Height - this.zoomSlider.Height);

            this.UpdateTransformCenter(e.GetPosition(this).X, e.GetPosition(this).Y, centerX, centerY);
        }

        /// <summary>
        /// Updates the zomm center when a new tab is added, changed or operation finished.
        /// </summary>
        /// <param name="mng">The manager</param>
        /// <param name="doesntMatter">Doesn't matter parameter</param>
        private void ImageUpdated(Manager mng, object doesntMatter)
        {
            this.zoomSlider.Value = 1;
            this.UpdateTransformCenter(this.Width / 2, (this.Height - this.zoomSlider.Height) / 2, 0.5, 0.5);
            this.imageView.Source = ((WPFBitmap)Manager.Instance.CurrentTab.Thumbnails.Peek()).GetImageSource();
            this.Height = (this.Width * (this.imageView.Source.Height / this.imageView.Source.Width)) + 25;
        }

        /// <summary>
        /// Updates the zooming center and the zoomRect position
        /// </summary>
        /// <param name="x">New X of the zoomRect</param>
        /// <param name="y">New Y of the zoomRect</param>
        /// <param name="xn">New X of the zooming center</param>
        /// <param name="yn">New Y of the zooming center</param>
        private void UpdateTransformCenter(double x, double y, double xn, double yn)
        {
            if (xn < 0)
            {
                xn = 0;
            }

            if (yn < 0)
            {
                yn = 0;
            }

            if (xn > 1)
            {
                xn = 1;
            }

            if (yn > 1)
            {
                yn = 1;
            }

            this.targetedImage.RenderTransformOrigin = new Point(xn, yn);

            if (x < this.centerRect.Width / 2)
            {
                x = this.centerRect.Width / 2;
            }

            if (x > this.Width - (this.centerRect.Width / 2))
            {
                x = this.Width - (this.centerRect.Width / 2);
            }

            if (y < this.centerRect.Height / 2)
            {
                y = this.centerRect.Height / 2;
            }

            if (y > (this.Height - 25 - (this.centerRect.Width / 2)))
            {
                y = this.Height - 25 - (this.centerRect.Width / 2);
            }

            this.centerRect.Margin = new Thickness(x - (this.centerRect.Width / 2), y - (this.centerRect.Height / 2), 0, 0);
        }
    }
}
