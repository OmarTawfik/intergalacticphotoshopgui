namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Pixelates the image with a pixel size.
    /// </summary>
    public class PixelationOperation : BaseOperation
    {
        /// <summary>
        /// Pixel Size.
        /// </summary>
        private int pixelSize;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (oldMin/Max and newMin/Max).</param>
        public override void SetInput(params object[] input)
        {
            this.pixelSize = (int)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Pixel Size,int,1,200";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Pixelate";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i += this.pixelSize)
            {
                for (int j = 0; j < this.Image.Width; j += this.pixelSize)
                {
                    int totalRed = 0, totalGreen = 0, totalBlue = 0;
                    for (int a = i; a < i + this.pixelSize && a < this.Image.Height; a++)
                    {
                        for (int b = j; b < j + this.pixelSize && b < this.Image.Width; b++)
                        {
                            Pixel p = this.Image.GetPixel(b, a);
                            totalRed += p.Red;
                            totalGreen += p.Green;
                            totalBlue += p.Blue;
                        }
                    }

                    Pixel newPixel = Pixel.CutOff(
                        totalRed / (this.pixelSize * this.pixelSize),
                        totalGreen / (this.pixelSize * this.pixelSize),
                        totalBlue / (this.pixelSize * this.pixelSize));

                    for (int a = i; a < i + this.pixelSize && a < this.Image.Height; a++)
                    {
                        for (int b = j; b < j + this.pixelSize && b < this.Image.Width; b++)
                        {
                            this.Image.SetPixel(b, a, newPixel);
                        }
                    }
                }
            }
        }
    }
}
