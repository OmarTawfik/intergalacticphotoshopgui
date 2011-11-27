﻿namespace IntergalacticCore.Operations.Filters.Smoothing
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Does noise reduction using 2D mean filtering.
    /// </summary>
    public class MeanFilter2D : ConvolutionBase
    {
        /// <summary>
        /// Data to be used in the mask.
        /// </summary>
        private int maskSize;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.maskSize = (int)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask Size,int,1,50";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Mean Filter 2D";
        }

        /// <summary>
        /// Does noise reduction using 2D mean filtering.
        /// </summary>
        protected override void Operate()
        {
            int side = (int)this.maskSize / 2;
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int a = i - side; a < i + side; a++)
                    {
                        for (int b = j - side; b < j + side; b++)
                        {
                            Pixel p = this.GetLocation(b, a);
                            red += p.Red;
                            green += p.Green;
                            blue += p.Blue;
                        }
                    }

                    Pixel newPixel = new Pixel(
                       (byte)(red / (this.maskSize * this.maskSize)),
                       (byte)(green / (this.maskSize * this.maskSize)),
                       (byte)(blue / (this.maskSize * this.maskSize)));
                    this.ResultImage.SetPixel(j, i, newPixel);
                }
            }
        }
    }
}