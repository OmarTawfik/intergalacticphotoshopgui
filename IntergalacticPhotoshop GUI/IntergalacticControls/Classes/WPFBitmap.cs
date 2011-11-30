namespace IntergalacticControls
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using IntergalacticControls.Classes;
    using IntergalacticCore.Data;

    /// <summary>
    /// WPF ImageBase to use in the IP core
    /// </summary>
    public class WPFBitmap : ImageBase
    {
        /// <summary>
        /// The bitmap buffer
        /// </summary>
        private WriteableBitmap buffer;

        /// <summary>
        /// The base pointer for the bitmap
        /// </summary>
        private IntPtr bitBase;

        /// <summary>
        /// The stride for the buffer
        /// </summary>
        private int bitStride;

        /// <summary>
        /// Dimentions of the buffer
        /// </summary>
        private int width, height;

        /// <summary>
        /// Holds the thumbnail temporarily at creation time
        /// </summary>
        private WPFBitmap thumbnailReturnObject = null;

        /// <summary>
        /// Initializes a new instance of the WPFBitmap class with a null buffer
        /// </summary>
        public WPFBitmap()
        {
            this.buffer = null;
            this.width = 0;
            this.height = 0;
        }

        /// <summary>
        /// Initializes a new instance of the WPFBitmap class with a width and height
        /// </summary>
        /// <param name="width">Width of the new buffer</param>
        /// <param name="height">Height of the new buffer</param>
        public WPFBitmap(int width, int height)
        {
            if (Manager.Instance.ManagerDispatcher.Thread == Thread.CurrentThread)
            {
                this.CreateBufferWithSize(width, height);
            }
            else
            {
                Manager.Instance.ManagerDispatcher.BeginInvoke(new BufferCreatorWithSizeDelegate(this.CreateBufferWithSize), width, height).Wait();
            }
        }

        /// <summary>
        /// Initializes a new instance of the WPFBitmap class with a width and height
        /// </summary>
        /// <param name="bitmap">Source of the buffer</param>
        public WPFBitmap(BitmapSource bitmap)
        {
            if (Manager.Instance.ManagerDispatcher.Thread == Thread.CurrentThread)
            {
                this.CreateBufferFromSource(bitmap);
            }
            else
            {
                Manager.Instance.ManagerDispatcher.BeginInvoke(new BufferCreatorFromSourceDelegate(this.CreateBufferFromSource), bitmap).Wait();
            }
        }

        /// <summary>
        /// Buffer creation delegate
        /// </summary>
        /// <param name="width">The width of buffer</param>
        /// <param name="height">The height of buffer</param>
        private delegate void BufferCreatorWithSizeDelegate(int width, int height);

        /// <summary>
        /// Buffer creation delegate
        /// </summary>
        /// <param name="stream">The source stream</param>
        private delegate void BufferCreatorFromStreamDelegate(Stream stream);

        /// <summary>
        /// Buffer creation delegate
        /// </summary>
        /// <param name="source">The bitmap source</param>
        private delegate void BufferCreatorFromSourceDelegate(BitmapSource source);

        /// <summary>
        /// Save file delegate
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <param name="type">The image file type</param>
        private delegate void SaveFileDelegate(string filePath, ImageFileType type);

        /// <summary>
        /// Gets the height of the buffer
        /// </summary>
        public override int Height
        {
            get { return this.height; }
        }

        /// <summary>
        /// Gets the width of the buffer
        /// </summary>
        public override int Width
        {
            get { return this.width; }
        }

        /// <summary>
        /// This is called before editing the image to make any memory adjustments.
        /// </summary>
        public override void BeforeEdit()
        {
            if (this.buffer.Dispatcher.Thread == Thread.CurrentThread)
            {
                this.buffer.Lock();
                this.bitBase = this.buffer.BackBuffer;
                this.bitStride = this.buffer.BackBufferStride;
            }
            else
            {
                this.buffer.Dispatcher.BeginInvoke(new Action(this.BeforeEdit)).Wait();
            }
        }

        /// <summary>
        /// This is called after editing the image to make any memory adjustments.
        /// </summary>
        public override void AfterEdit()
        {
            if (this.buffer.Dispatcher.Thread == Thread.CurrentThread)
            {
                this.buffer.AddDirtyRect(new System.Windows.Int32Rect(0, 0, this.Width, this.Height));
                this.buffer.Unlock();
            }
            else
            {
                this.buffer.Dispatcher.BeginInvoke(new Action(this.AfterEdit)).Wait();
            }
        }

        /// <summary>
        /// Returns the pixel at location x and y.
        /// </summary>
        /// <param name="x">The horizontal location of the pixel.</param>
        /// <param name="y">The vertical location of the pixel.</param>
        /// <returns>The pixel at location x and y.</returns>
        public override unsafe Pixel GetPixel(int x, int y)
        {
            Pixel* color = (Pixel*)(((byte*)this.bitBase) + (y * this.bitStride));
            return color[x];
        }

        /// <summary>
        /// Sets the pixel at location x and y with value p.
        /// </summary>
        /// <param name="x">The horizontal location of the pixel.</param>
        /// <param name="y">The vertical location of the pixel.</param>
        /// <param name="p">The pixel to be set at location x and y.</param>
        public override unsafe void SetPixel(int x, int y, Pixel p)
        {
            Pixel* target = (Pixel*)(((byte*)this.bitBase) + (y * this.bitStride));
            target[x].Red = p.Red;
            target[x].Green = p.Green;
            target[x].Blue = p.Blue;
        }

        /// <summary>
        /// Sets the image to a new size.
        /// </summary>
        /// <param name="width">Width of the new image</param>
        /// <param name="height">Height of the new image.</param>
        public override void SetSize(int width, int height)
        {
            this.buffer = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr24, null);
            this.width = this.buffer.PixelWidth;
            this.height = this.buffer.PixelHeight;
        }

        /// <summary>
        /// Creates an empty clone with the same type.
        /// </summary>
        /// <param name="width">Width of the clone.</param>
        /// <param name="height">Height of the clone.</param>
        /// <returns>The resulting empty clone.</returns>
        public override ImageBase CreateEmptyClone(int width, int height)
        {
            return new WPFBitmap(width, height);
        }

        /// <summary>
        /// Creates a copy with the same pixels and size.
        /// </summary>
        /// <returns>The resulting copy.</returns>
        public override ImageBase CreateCopyClone()
        {
            return new WPFBitmap(this.buffer);
        }

        /// <summary>
        /// Saves the image (uncompressed) to a file path.
        /// </summary>
        /// <param name="filePath">Location of the image.</param>
        /// <param name="type">Image file type</param>
        public override void SaveImage(string filePath, ImageFileType type)
        {
            if (Manager.Instance.ManagerDispatcher.Thread == Thread.CurrentThread)
            {
                if (type != ImageFileType.P3)
                {
                    FileStream file = new FileStream(filePath, FileMode.OpenOrCreate);
                    BitmapEncoder encoder = null;

                    switch (type)
                    {
                        case ImageFileType.BMP:
                            encoder = new BmpBitmapEncoder();
                            break;
                        case ImageFileType.PNG:
                            encoder = new PngBitmapEncoder();
                            break;
                        case ImageFileType.JPG:
                            JpegBitmapEncoder jpegEncoder = new JpegBitmapEncoder();
                            jpegEncoder.QualityLevel = 100;
                            encoder = jpegEncoder;
                            break;
                    }

                    encoder.Frames.Add(BitmapFrame.Create(this.buffer));
                    encoder.Save(file);
                    file.Close();
                }
                else
                {
                    Exporter.SaveP3(filePath, this);
                }
            }
            else
            {
                Manager.Instance.ManagerDispatcher.BeginInvoke(new SaveFileDelegate(this.SaveImage), filePath, type).Wait();
            }
        }

        /// <summary>
        /// Loads the image (uncompressed) from a file path.
        /// </summary>
        /// <param name="filePath">Location of the image.</param>
        public override void LoadImage(string filePath)
        {
            string[] splitFilename = System.IO.Path.GetFileName(filePath).Split('.');
            if (splitFilename[splitFilename.Length - 1] == "ppm")
            {
                Importer.LoadPPM(filePath, this);
                return;
            }

            FileStream file = new FileStream(Environment.CurrentDirectory + "\\" + filePath, FileMode.Open);
            byte[] fileByte = new byte[file.Length];
            file.Read(fileByte, 0, (int)file.Length);
            file.Close();

            MemoryStream stream = new MemoryStream(fileByte);

            if (Manager.Instance.ManagerDispatcher.Thread == Thread.CurrentThread)
            {
                this.CreateBufferFromStream(stream);
            }
            else
            {
                Manager.Instance.ManagerDispatcher.BeginInvoke(new BufferCreatorFromStreamDelegate(this.CreateBufferFromStream), stream).Wait();
            }
        }

        /// <summary>
        /// Returns a thumbnail of the image
        /// </summary>
        /// <returns>Thumbnail object</returns>
        public override ImageBase GetThumbnail()
        {
            if (Manager.Instance.ManagerDispatcher.Thread == Thread.CurrentThread)
            {
                this.CreateThumbnail();
            }
            else
            {
                Manager.Instance.ManagerDispatcher.BeginInvoke(new Action(this.CreateThumbnail)).Wait();
            }

            return this.thumbnailReturnObject;
        }

        /// <summary>
        /// Returns the image source
        /// </summary>
        /// <returns>The image source</returns>
        public ImageSource GetImageSource()
        {
            return this.buffer;
        }

        /// <summary>
        /// Creates a buffer from width and height
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        private void CreateBufferWithSize(int width, int height)
        {
            this.buffer = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgr24, null);
            this.width = this.buffer.PixelWidth;
            this.height = this.buffer.PixelHeight;
        }

        /// <summary>
        /// Creates a buffer from a stream
        /// </summary>
        /// <param name="stream">The stream</param>
        private void CreateBufferFromStream(Stream stream)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.StreamSource = stream;
            img.EndInit();

            this.buffer = new WriteableBitmap(new FormatConvertedBitmap(img, PixelFormats.Bgr24, null, 0));
            this.width = this.buffer.PixelWidth;
            this.height = this.buffer.PixelHeight;
        }

        /// <summary>
        /// Creates a buffer from bitmap source
        /// </summary>
        /// <param name="source">The source</param>
        private void CreateBufferFromSource(BitmapSource source)
        {
            this.buffer = new WriteableBitmap(new FormatConvertedBitmap(source, PixelFormats.Bgr24, null, 0));
            this.width = this.buffer.PixelWidth;
            this.height = this.buffer.PixelHeight;
        }

        /// <summary>
        /// Creates thumbnail
        /// </summary>
        private void CreateThumbnail()
        {
            int thumbnailWidth = 200;
            int thumbnailHeight = (int)(thumbnailWidth * ((float)this.buffer.PixelHeight / (float)this.buffer.PixelWidth));
            RenderTargetBitmap target = new RenderTargetBitmap(thumbnailWidth, thumbnailHeight, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual v = new DrawingVisual();
            DrawingContext c = v.RenderOpen();
            c.DrawImage(this.buffer, new Rect(0, 0, thumbnailWidth, thumbnailHeight));
            c.Close();
            target.Render(v);

            this.thumbnailReturnObject = new WPFBitmap(target);
        }
    }
}
