namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Sharpens image vertical lines using laplacian filter.
    /// </summary>
    public class VerticalLineSharpeningOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Vertical Line Sharpening";
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
                          right = this.GetLocation(j + 1, i),
                          left = this.GetLocation(j - 1, i);

                    int red = center.Red + left.Red - right.Red;
                    int green = center.Green + left.Green - right.Green;
                    int blue = center.Blue + left.Blue - right.Blue;

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
