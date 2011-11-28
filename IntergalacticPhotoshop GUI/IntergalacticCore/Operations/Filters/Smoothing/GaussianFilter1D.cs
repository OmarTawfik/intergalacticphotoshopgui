namespace IntergalacticCore.Operations.Filters.Smoothing
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Applys Gaussian blur 1D on the given image.
    /// </summary>
    public class GaussianFilter1D : ConvolutionBase
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
            return "Gaussian Filter 1D";
        }

        /// <summary>
        /// Applys Gaussian blur 1D on the given image.
        /// </summary>
        protected override void Operate()
        {
            int n = (int)((3.7 * this.sigma) - 0.5);
            double bracket = 1.0 / Math.Sqrt(2 * Math.PI * this.sigma);

            int maskSize = (2 * n) + 1;
            double[] horizontalMask = new double[maskSize];
            double[] verticalMask = new double[maskSize];

            for (int x = 0, a = -n; x < maskSize; x++, a++)
            {
                double power = -(a * a) / (2 * this.sigma * this.sigma);
                horizontalMask[x] = bracket * Math.Pow(Math.E, power);
            }

            for (int y = 0, b = 0; y < maskSize; y++, b++)
            {
                double power = -(b * b) / (2 * this.sigma * this.sigma);
                verticalMask[y] = bracket * Math.Pow(Math.E, power);
            }

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    double red = 0, green = 0, blue = 0;
                    for (int x = j - n, b = 0; x <= j + n; x++, b++)
                    {
                        Pixel p = this.GetLocation(x, i);
                        red += p.Red * horizontalMask[b];
                        green += p.Green * horizontalMask[b];
                        blue += p.Blue * horizontalMask[b];
                    }

                    this.ResultImage.SetPixel(j, i, new Pixel((byte)red, (byte)green, (byte)blue));
                }
            }

            this.Image.AfterEdit();
            this.ResultImage.AfterEdit();
            this.Image = ResultImage.CreateCopyClone();
            this.Image.BeforeEdit();
            this.ResultImage.BeforeEdit();

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    double red = 0, green = 0, blue = 0;
                    for (int y = i - n, a = 0; y <= i + n; y++, a++)
                    {
                        Pixel p = this.GetLocation(j, y);
                        red += p.Red * verticalMask[a];
                        green += p.Green * verticalMask[a];
                        blue += p.Blue * verticalMask[a];
                    }

                    this.ResultImage.SetPixel(j, i, new Pixel((byte)red, (byte)green, (byte)blue));
                }
            }
        }
    }
}
