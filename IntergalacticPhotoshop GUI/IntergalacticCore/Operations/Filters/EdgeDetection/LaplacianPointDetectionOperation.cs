namespace IntergalacticCore.Operations.Filters.EdgeDetection
{
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Detects points using laplacian filter.
    /// </summary>
    public class LaplacianPointDetectionOperation : BaseEdgeDetectionOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Laplacian Point Edge Detection";
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
                    red += 9 * center.Red;
                    green += 9 * center.Green;
                    blue += 9 * center.Blue;
                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
