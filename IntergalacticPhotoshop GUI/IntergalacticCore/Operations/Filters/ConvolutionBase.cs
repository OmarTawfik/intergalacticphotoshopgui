namespace IntergalacticCore.Operations.Filters
{
    using System.Collections.Generic;
    using IntergalacticCore.Data;

    /// <summary>
    /// Acts as a base to all convulution operations.
    /// </summary>
    public abstract class ConvolutionBase : CopyOperation
    {
        /// <summary>
        /// Gets a pixel at a specified location or repeats if out of bounds.
        /// </summary>
        /// <param name="x">Horizontal location of pixel.</param>
        /// <param name="y">Vertical location of pixel.</param>
        /// <returns>Found pixel.</returns>
        protected Pixel GetLocation(int x, int y)
        {
            if (x < 0)
            {
                x = 0;
            }

            if (x >= this.Image.Width)
            {
                x = this.Image.Width - 1;
            }

            if (y < 0)
            {
                y = 0;
            }

            if (y >= this.Image.Height)
            {
                y = this.Image.Height - 1;
            }

            return this.Image.GetPixel(x, y);
        }
    }
}
