namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Sharpens image points using laplacian filter.
    /// </summary>
    public class LaplacianPointSharpeningOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Laplacian Point Sharpening";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int y = i - 1; y <= i + 1; y++)
                    {
                        for (int x = j - 1; x <= j + 1; x++)
                        {
                            Pixel p = this.GetLocation(x, y);
                            red -= p.Red;
                            green -= p.Green;
                            blue -= p.Blue;
                        }
                    }

                    Pixel center = this.Image.GetPixel(j, i);
                    red += 10 * center.Red;
                    green += 10 * center.Green;
                    blue += 10 * center.Blue;
                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
