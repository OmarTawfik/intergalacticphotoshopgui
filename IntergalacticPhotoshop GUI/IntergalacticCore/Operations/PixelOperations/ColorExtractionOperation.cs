namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Extracts certain colors from the image.
    /// </summary>
    public class ColorExtractionOperation : BaseOperation
    {
        /// <summary>
        /// Keeping Red Component.
        /// </summary>
        private bool red;

        /// <summary>
        /// Keeping Green Component.
        /// </summary>
        private bool green;

        /// <summary>
        /// Keeping Blue Component.
        /// </summary>
        private bool blue;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (oldMin/Max and newMin/Max).</param>
        public override void SetInput(params object[] input)
        {
            this.red = (bool)input[0];
            this.green = (bool)input[1];
            this.blue = (bool)input[2];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Red,bool|Green,bool|Blue,bool";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Color Extraction";
        }

        /// <summary>
        /// Extracts certain colors from the image.
        /// </summary>
        protected override void Operate()
        {
            byte zero = 0;

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);

                    p.Red = this.red ? p.Red : zero;
                    p.Green = this.green ? p.Green : zero;
                    p.Blue = this.blue ? p.Blue : zero;

                    this.Image.SetPixel(j, i, p);
                }
            }
        }
    }
}
