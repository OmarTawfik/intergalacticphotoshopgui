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
            this.imageViewParent.SizeChanged += new SizeChangedEventHandler(ImageViewParent_SizeChanged);
        }

        /// <summary>
        /// Updates the image view when the parent resizes
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event Args</param>
        void ImageViewParent_SizeChanged(object sender, SizeChangedEventArgs e)
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

            centerX = e.GetPosition(this).X / this.Width;
            centerY = e.GetPosition(this).Y / (this.Height - this.zoomSlider.Height);

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

            this.zoomSlider.Value = 1;
            this.UpdateZoom();
            this.imageView.Source = ((WPFBitmap)Manager.Instance.CurrentTab.Thumbnails.Peek()).GetImageSource();
            this.Height = (this.Width * (this.imageView.Source.Height / this.imageView.Source.Width)) + 25;
            this.targetedImage = (BitmapSource)this.targetedImageView.Source;
        }

        /// <summary>
        /// Updates the zooming center and the zoomRect position
        /// </summary>
        /// <param name="x">New X of the zoomRect</param>
        /// <param name="y">New Y of the zoomRect</param>
        /// <param name="xn">New X of the zooming center</param>
        /// <param name="yn">New Y of the zooming center</param>
        private void UpdateZoom()
        {
            double rectX = this.centerX * this.ActualWidth, rectY = this.centerY * this.ActualHeight;
            if (rectX < this.centerRect.Width / 2)
            {
                rectX = this.centerRect.Width / 2;
            }

            if (rectX > this.Width - (this.centerRect.Width / 2))
            {
                rectX = this.Width - (this.centerRect.Width / 2);
            }

            if (rectY < this.centerRect.Height / 2)
            {
                rectY = this.centerRect.Height / 2;
            }

            if (rectY > (this.Height - 25 - (this.centerRect.Width / 2)))
            {
                rectY = this.Height - 25 - (this.centerRect.Width / 2);
            }

            if (this.targetedImageView == null)
            {
                return;
            }


            this.centerRect.Width = widthRatio * this.ActualWidth;
            this.centerRect.Height = heightRatio * (this.ActualHeight - this.zoomSlider.Height);
            this.centerRect.Margin = new Thickness(rectX - (this.centerRect.Width / 2), rectY - (this.centerRect.Height / 2), 0, 0);

            if (this.centerX < 0)
            {
                this.centerX = 0;
            }

            if (this.centerY < 0)
            {
                this.centerY = 0;
            }

            if (this.centerX > 1)
            {
                this.centerX = 1;
            }

            if (this.centerY > 1)
            {
                this.centerY = 1;
            }

            this.targetedImageView.Margin = new Thickness(
                ((0.5 - this.centerX) * this.targetedImageView.ActualWidth) + ((this.imageViewParent.ActualWidth - this.targetedImageView.ActualWidth) / 2),
                ((0.5 - this.centerY) * this.targetedImageView.ActualHeight) + ((this.imageViewParent.ActualHeight - this.targetedImageView.ActualHeight) / 2),
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

            this.targetedImageView.Stretch = Stretch.Uniform;
            this.targetedImageView.Width = e.NewValue * this.targetedImage.PixelWidth;
            this.targetedImageView.Height = e.NewValue * this.targetedImage.PixelHeight;
            
            widthRatio = this.imageViewParent.ActualWidth / this.targetedImageView.ActualWidth;
            heightRatio = this.imageViewParent.ActualHeight / this.targetedImageView.ActualHeight;

            if (widthRatio > 1 && heightRatio > 1)
            {
                this.centerX = 0.5;
                this.centerX = 0.5;
            }

            if (widthRatio > 1)
            {
                widthRatio = 1;
            }

            if (heightRatio > 1)
            {
                heightRatio = 1;
            }

            this.UpdateZoom();
        }
    }
}
