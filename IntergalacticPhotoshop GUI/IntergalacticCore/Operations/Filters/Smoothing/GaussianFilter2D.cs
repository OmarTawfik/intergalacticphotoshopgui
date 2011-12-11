namespace IntergalacticCore.Operations.Filters.Smoothing
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Applys Gaussian blur 2D on the given image.
    /// </summary>
    public class GaussianFilter2D : ConvolutionBase
    {
        /// <summary>
        /// Data to be used in the mask.
        /// </summary>
        private double sigma;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.sigma = (double)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Sigma,double,0,5";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Gaussian Filter 2D";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            int n = (int)Math.Ceiling(((3.7 * this.sigma) - 0.5));
            double bracket = 1.0 / (2 * Math.PI * this.sigma * this.sigma);

            int maskSize = (2 * n) + 1;
            double[,] mask = new double[maskSize, maskSize];

            for (int y = -n, a = 0; y <= n; y++, a++)
            {
                for (int x = -n, b = 0; x <= n; x++, b++)
                {
                    double power = -((x * x) + (y * y)) / (2 * this.sigma * this.sigma);
                    mask[a, b] = bracket * Math.Pow(Math.E, power);
                }
            }

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    double red = 0, green = 0, blue = 0;
                    for (int y = i - n, a = 0; y <= i + n; y++, a++)
                    {
                        for (int x = j - n, b = 0; x <= j + n; x++, b++)
                        {
                            Pixel p = this.GetLocation(x, y);
                            red += p.Red * mask[a, b];
                            green += p.Green * mask[a, b];
                            blue += p.Blue * mask[a, b];
                        }
                    }

                    this.ResultImage.SetPixel(j, i, new Pixel((byte)red, (byte)green, (byte)blue));
                }
            }
        }
    }
}
