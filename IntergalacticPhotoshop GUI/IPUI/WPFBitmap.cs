
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
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticCore2.Data;

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
        /// Initializes a new instance of the WPFBitmap class with a width and height
        /// </summary>
        /// <param name="width">Width of the new buffer</param>
        /// <param name="height">Height of the new buffer</param>
        public WPFBitmap(int width, int height)
        {
            this.buffer = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Bgr24, null);
        }

        /// <summary>
        /// Initializes a new instance of the WPFBitmap class with a width and height
        /// </summary>
        /// <param name="bitmap">Source of the buffer</param>
        public WPFBitmap(BitmapSource bitmap)
        {
            this.buffer = new WriteableBitmap(new FormatConvertedBitmap(bitmap, PixelFormats.Bgr24, null, 0));
        }

        /// <summary>
        /// Gets the height of the buffer
        /// </summary>
        public override int Height
        {
            get { return this.buffer.PixelHeight; }
        }

        /// <summary>
        /// Gets the width of the buffer
        /// </summary>
        public override int Width
        {
            get { return this.buffer.PixelHeight; }
        }

        /// <summary>
        /// This is called before editing the image to make any memory adjustments.
        /// </summary>
        public override void BeforeEdit()
        {
            this.buffer.Lock();
            this.bitBase = this.buffer.BackBuffer;
            this.bitStride = this.buffer.BackBufferStride;
        }

        /// <summary>
        /// This is called after editing the image to make any memory adjustments.
        /// </summary>
        public override void AfterEdit()
        {
            this.buffer.AddDirtyRect(new System.Windows.Int32Rect(0, 0, this.Width, this.Height));
            this.buffer.Unlock();
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
    }
}
