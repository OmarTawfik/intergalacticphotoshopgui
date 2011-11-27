namespace IntergalacticCore.Operations.Transformations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Flips the image horizontally.
    /// </summary>
    public class HorizontalFlipOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Horizontal Flip";
        }

        /// <summary>
        /// Flips the image horizontally.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width / 2; j++)
                {
                    Pixel leftPixel = this.Image.GetPixel(j, i);
                    Pixel rightPixel = this.Image.GetPixel(this.Image.Width - j - 1, i);

                    this.Image.SetPixel(j, i, rightPixel);
                    this.Image.SetPixel(this.Image.Width - j - 1, i, leftPixel);
                }
            }
        }
    }
}
