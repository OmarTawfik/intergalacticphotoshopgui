namespace IntergalacticCore.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Stores a binary mask.  Used in morphology operations
    /// </summary>
    public class BinaryMask
    {
        /// <summary>
        /// Mask data.
        /// </summary>
        private bool[,] data;

        /// <summary>
        /// Height of the mask.
        /// </summary>
        private int height;

        /// <summary>
        /// Width of the mask.
        /// </summary>
        private int width;

        /// <summary>
        /// Initializes a new instance of the BinaryMask class.
        /// </summary>
        /// <param name="width">Width of mask.</param>
        /// <param name="height">Height of mask.</param>
        /// <returns>The newly created mask.</returns>
        public BinaryMask(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.data = new bool[height, width];
        }

        /// <summary>
        /// Gets the width of the mask.
        /// </summary>
        public int Width
        {
            get { return this.width; }
        }

        /// <summary>
        /// Gets the height of the mask.
        /// </summary>
        public int Height
        {
            get { return this.height; }
        }

        /// <summary>
        /// Gets the mask data.
        /// </summary>
        public bool[,] Data
        {
            get { return this.data; }
        }
    }
}
