namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Sharpens image back diagonal lines using laplacian filter.
    /// </summary>
    public class BackDiagonalLineSharpeningOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Back Diagonal Line Sharpening";
        }

        /// <summary>
        /// Sharpens image back diagonal lines using laplacian filter.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel center = this.Image.GetPixel(j, i),
                          rightUp = this.GetLocation(j + 1, i - 1),
                          downLeft = this.GetLocation(j - 1, i + 1);

                    int red = center.Red + rightUp.Red - downLeft.Red;
                    int green = center.Green + rightUp.Green - downLeft.Green;
                    int blue = center.Blue + rightUp.Blue - downLeft.Blue;

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
