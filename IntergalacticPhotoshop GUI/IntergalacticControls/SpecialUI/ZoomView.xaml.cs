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
        private Image targetedImageView;

        /// <summary>
        /// The targeted image view
        /// </summary>
        private Panel imageViewParent;

        /// <summary>
        /// The targeted image view
        /// </summary>
        private BitmapSource targetedImage;

        /// <summary>
        /// Center of zoom, mouse coords and ratios
        /// </summary>
        private double centerX, centerY, widthRatio, heightRatio;

        /// <summary>
        /// Initializes a new instance of the ZoomView class
        /// </summary>
        /// <param name="target">Targeted image view</param>
        public ZoomView(Image target)
        {
            InitializeComponent();
            this.targetedImageView = target;
            this.targetedImage = (BitmapSource)target.Source;
            this.imageViewParent = (Panel)this.targetedImageView.Parent;

            Manager.Instance.OnNewTabAdded += this.ImageUpdated;
            Manager.Instance.OnTabChanged += this.ImageUpdated;
            Manager.Instance.OnOperationFinshed += this.ImageUpdated;
            this.centerRect.MouseMove += this.CenterRect_MouseMove;
            this.imageViewParent.SizeChanged += new SizeChangedEventHandler(this.ImageViewParent_SizeChanged);
        }

        /// <summary>
        /// Updates the image view when the parent resizes
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event Args</param>
        private void ImageViewParent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateZoom();
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

            this.centerX = e.GetPosition(this).X / this.Width;
            this.centerY = e.GetPosition(this).Y / (this.Height - this.zoomSlider.Height);

            this.UpdateZoom();
        }

        /// <summary>
        /// Updates the zomm center when a new tab is added, changed or operation finished.
        /// </summary>
        /// <param name="mng">The manager</param>
        /// <param name="doesntMatter">Doesn't matter parameter</param>
        private void ImageUpdated(Manager mng, object doesntMatter)
        {
            this.centerX = 0.5;
            this.centerY = 0.5;
            this.widthRatio = 1;
            this.heightRatio = 1;

            this.zoomSlider.Value = 1;
            this.imageView.Source = ((WPFBitmap)Manager.Instance.CurrentTab.Thumbnails.Peek()).GetImageSource();
            this.Height = (this.Width * (this.imageView.Source.Height / this.imageView.Source.Width)) + 25;
            this.targetedImage = (BitmapSource)this.targetedImageView.Source;

            this.UpdateZoom();
        }

        /// <summary>
        /// Updates the zooming center and the zoomRect position
        /// </summary>
        private void UpdateZoom()
        {
            if (this.targetedImageView == null || this.targetedImage == null)
            {
                return;
            }

            this.targetedImageView.Width = this.widthRatio * this.targetedImage.PixelWidth;
            this.targetedImageView.Height = this.heightRatio * this.targetedImage.PixelHeight;

            if (this.widthRatio > 1 || this.heightRatio > 1)
            {
                this.centerX = 0.5;
                this.centerY = 0.5;
            }

            if (this.widthRatio > 1)
            {
                this.widthRatio = 1;
                this.centerRect.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this.centerRect.Visibility = System.Windows.Visibility.Visible;
            }

            if (this.heightRatio > 1)
            {
                this.heightRatio = 1;
                this.centerRect.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this.centerRect.Visibility = System.Windows.Visibility.Visible;
            }

            if (double.IsNaN(this.widthRatio))
            {
                return;
            }

            this.centerRect.Width = this.widthRatio * this.Width;
            this.centerRect.Height = this.heightRatio * (this.Height - this.zoomSlider.Height);

            double centerXEdge = this.widthRatio / 2;
            double centerYEdge = this.heightRatio / 2;

            if (this.centerX < centerXEdge)
            {
                this.centerX = centerXEdge;
            }

            if (this.centerY < centerYEdge)
            {
                this.centerY = centerYEdge;
            }

            if (this.centerX > (1 - centerXEdge))
            {
                this.centerX = 1 - centerXEdge;
            }

            if (this.centerY > (1 - centerYEdge))
            {
                this.centerY = 1 - centerYEdge;
            }

            double rectX = this.centerX * this.Width, rectY = this.centerY * (this.Height - this.zoomSlider.Height);

            this.centerRect.Margin = new Thickness(rectX - (this.centerRect.Width / 2), rectY - (this.centerRect.Height / 2), 0, 0);

            this.targetedImageView.Margin = new Thickness(
                ((0.5 - this.centerX) * this.targetedImageView.Width) + ((this.imageViewParent.ActualWidth - this.targetedImageView.Width) / 2),
                ((0.5 - this.centerY) * this.targetedImageView.Height) + ((this.imageViewParent.ActualHeight - this.targetedImageView.Height) / 2),
                0,
                0);
        }

        /// <summary>
        /// Handles valueChanged event in the zoom slider
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event Argument</param>
        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.targetedImageView == null)
            {
                return;
            }

            this.widthRatio = e.NewValue;
            this.heightRatio = e.NewValue;
            this.targetedImageView.Stretch = Stretch.Uniform;

            this.UpdateZoom();
        }
    }
}
