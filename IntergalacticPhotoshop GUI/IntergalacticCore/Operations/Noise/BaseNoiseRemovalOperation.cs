namespace IntergalacticCore.Operations.Noise
{
    using System;
    using System.Collections.Generic;
    using IntergalacticCore.Data;

    /// <summary>
    /// Acts as a base to all noise removal operations.
    /// </summary>
    public class BaseNoiseRemovalOperation : CopyOperation
    {
        /// <summary>
        /// Holds the bitmixed version of this image.
        /// </summary>
        private int[,] bitmixedImage;

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            base.BeforeOperate();
            this.Image.BeforeEdit();
            this.bitmixedImage = new int[this.Image.Height, this.Image.Width];

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    this.bitmixedImage[i, j] = this.ToBitMixed(this.Image.GetPixel(j, i));
                }
            }

            this.Image.AfterEdit();
        }

        /// <summary>
        /// Gets a bitmixed pixel at a specific location.
        /// </summary>
        /// <param name="x">Horizontal Position.</param>
        /// <param name="y">Vertical Position.</param>
        /// <returns>The returned pixel.</returns>
        protected int GetBitmixedAt(int x, int y)
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

            return this.bitmixedImage[y, x];
        }

        /// <summary>
        /// converts a pixel to its bitmixed version.
        /// </summary>
        /// <param name="p">Pixel to be converted.</param>
        /// <returns>The bitmixed version.</returns>
        protected int ToBitMixed(Pixel p)
        {
            int total = 0;
            for (int i = 0; i < 8; i++)
            {
                int red = p.Red & (1 << i),
                    green = p.Green & (1 << i),
                    blue = p.Blue & (1 << i);

                total |= red << ((i + 1) * 2);
                total |= green << ((i * 2) + 1);
                total |= blue << (i * 2);
            }

            return total;
        }

        /// <summary>
        /// Converts a bitmixed pixel back to its original version.
        /// </summary>
        /// <param name="bits">Bitmixed version.</param>
        /// <returns>The original version.</returns>
        protected Pixel FromBitMixed(int bits)
        {
            byte totalRed = 0, totalGreen = 0, totalBlue = 0;
            for (int i = 0; i < 8; i++)
            {
                int red = bits & (1 << ((i * 3) + 2)),
                    green = bits & (1 << ((i * 3) + 1)),
                    blue = bits & (1 << (i * 3));

                totalRed |= (byte)(red >> ((i * 2) + 2));
                totalGreen |= (byte)(green >> ((i * 2) + 1));
                totalBlue |= (byte)(blue >> (i * 2));
            }

            return new Pixel(totalRed, totalGreen, totalBlue);
        }
    }
}
