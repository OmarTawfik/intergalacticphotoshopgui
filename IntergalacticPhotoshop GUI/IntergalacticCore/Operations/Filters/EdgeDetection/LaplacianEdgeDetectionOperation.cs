namespace IntergalacticCore.Operations.Filters.EdgeDetection
{
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Detects edges using laplacian filter.
    /// </summary>
    public class LaplacianEdgeDetectionOperation : BaseEdgeDetectionOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Laplacian Edge Detection";
        }

        /// <summary>
        /// Detects edges using laplacian filter.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel center = this.Image.GetPixel(j, i),
                          up = this.GetLocation(j, i - 1),
                          down = this.GetLocation(j, i + 1),
                          right = this.GetLocation(j + 1, i),
                          left = this.GetLocation(j - 1, i);

                    int red = -up.Red - down.Red - right.Red - left.Red + (4 * center.Red);
                    int green = -up.Green - down.Green - right.Green - left.Green + (4 * center.Green);
                    int blue = -up.Blue - down.Blue - right.Blue - left.Blue + (4 * center.Blue);

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
