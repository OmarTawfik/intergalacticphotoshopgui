namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Inverts all pixels contained in this image.
    /// </summary>
    public class NotOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Color Inverse";
        }

        /// <summary>
        /// Inverts all pixels contained in this image.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);
                    p.Red = (byte)(255 - p.Red);
                    p.Green = (byte)(255 - p.Green);
                    p.Blue = (byte)(255 - p.Blue);
                    this.Image.SetPixel(j, i, p);
                }
            }
        }
    }
}
