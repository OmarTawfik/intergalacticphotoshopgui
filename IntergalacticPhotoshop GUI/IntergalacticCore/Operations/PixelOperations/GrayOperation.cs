namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Converts the image colors to gray
    /// </summary>
    public class GrayOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Gray Colors";
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
                    Pixel p = this.Image.GetPixel(j, i);
                    int g = (p.Red + p.Green + p.Blue) / 3;
                    p.Red = (byte)g;
                    p.Green = (byte)g;
                    p.Blue = (byte)g;
                    this.Image.SetPixel(j, i, p);
                }
            }
        }
    }
}
