namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Sharpens image horizontal lines using laplacian filter.
    /// </summary>
    public class HorizontalLineSharpeningOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Horizontal Line Sharpening";
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
                    Pixel center = this.Image.GetPixel(j, i),
                          up = this.GetLocation(j, i - 1),
                          down = this.GetLocation(j, i + 1);

                    int red = center.Red + up.Red - down.Red;
                    int green = center.Green + up.Green - down.Green;
                    int blue = center.Blue + up.Blue - down.Blue;

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
