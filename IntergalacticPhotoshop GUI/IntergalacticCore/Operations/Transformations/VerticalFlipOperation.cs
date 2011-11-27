namespace IntergalacticCore.Operations.Transformations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Flips the image vertically.
    /// </summary>
    public class VerticalFlipOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Vertical Flip";
        }

        /// <summary>
        /// Flips the image vertically.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height / 2; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel topPixel = this.Image.GetPixel(j, i);
                    Pixel bottomPixel = this.Image.GetPixel(j, this.Image.Height - i - 1);

                    this.Image.SetPixel(j, i, bottomPixel);
                    this.Image.SetPixel(j, this.Image.Height - i - 1, topPixel);
                }
            }
        }
    }
}
