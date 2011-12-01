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
    using IntergalacticCore;
    using IntergalacticControls;
    using IntergalacticControls.Classes;

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

        void CenterRect_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                return;
            }

            double centerX = e.GetPosition(this).X / this.ActualWidth;
            double centerY = e.GetPosition(this).Y / (this.ActualHeight - this.zoomSlider.ActualHeight);

            this.UpdateTransformCenter(e.GetPosition(this).X, e.GetPosition(this).Y, centerX, centerY);
        }

        private void ImageUpdated(Manager mng, object doesntMatter)
        {
            this.zoomSlider.Value = 1;
            this.UpdateTransformCenter(this.ActualWidth / 2, (this.ActualHeight - this.zoomSlider.ActualHeight) / 2, 0.5, 0.5);
            this.imageView.Source = ((WPFBitmap)Manager.Instance.CurrentTab.Thumbnails.Peek()).GetImageSource();
            this.Height = this.Width * (this.imageView.Source.Height / this.imageView.Source.Width) + 25;
        }

        private void UpdateTransformCenter(double x, double y, double xn, double yn)
        {
            this.targetedImage.RenderTransformOrigin = new Point(xn, yn);
            this.centerRect.Margin = new Thickness(x - this.centerRect.Width / 2, y - this.centerRect.Height / 2, 0, 0);
        }
    }
}
