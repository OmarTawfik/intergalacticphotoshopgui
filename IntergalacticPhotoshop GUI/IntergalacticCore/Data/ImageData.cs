namespace IntergalacticCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Holds image data to be passed to native processing
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageData
    {
        /// <summary>
        /// The base pointer ofthe bitmap
        /// </summary>
        private byte* pbase;

        /// <summary>
        /// Stride, width and height of the bitmap
        /// </summary>
        private int stride, width, height;

        /// <summary>
        /// Initializes a new instance of the ImageData class
        /// </summary>
        /// <param name="pbase">Base pointer of the bitmap</param>
        /// <param name="stride">Stride of the bitmap</param>
        /// <param name="width">Width of the bitmap</param>
        /// <param name="height">Height of the bitmap</param>
        public ImageData(byte* pbase, int stride, int width, int height)
        {
            this.pbase = pbase;
            this.stride = stride;
            this.width = width;
            this.height = height;
        }
    }
}
