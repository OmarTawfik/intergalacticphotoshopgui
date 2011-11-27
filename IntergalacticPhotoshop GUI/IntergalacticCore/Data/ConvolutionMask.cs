namespace IntergalacticCore.Data
{
    /// <summary>
    /// Stores different masks for convulution operations.
    /// </summary>
    public class ConvolutionMask
    {
        /// <summary>
        /// Mask data to be multiplied by the pixels.
        /// </summary>
        private double[,] data;

        /// <summary>
        /// Height of the mask.
        /// </summary>
        private int height;

        /// <summary>
        /// Width of the mask.
        /// </summary>
        private int width;

        /// <summary>
        /// Initializes a new instance of the ConvolutionMask class.
        /// </summary>
        /// <param name="width">Width of mask.</param>
        /// <param name="height">Height of mask.</param>
        /// <returns>The newly created mask.</returns>
        public ConvolutionMask(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.data = new double[height, width];
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
        /// Gets the mask data to be multiplied by the pixels.
        /// </summary>
        public double[,] Data
        {
            get { return this.data; }
        }
    }
}
