namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Sharpens image front diagonal lines using laplacian filter.
    /// </summary>
    public class FrontDiagonalLineSharpeningOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Front Diagonal Line Sharpening";
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
                          leftUp = this.GetLocation(j - 1, i - 1),
                          downRight = this.GetLocation(j + 1, i + 1);

                    int red = center.Red + leftUp.Red - downRight.Red;
                    int green = center.Green + leftUp.Green - downRight.Green;
                    int blue = center.Blue + leftUp.Blue - downRight.Blue;

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
