namespace IntergalacticCore.Operations.Transformations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Shears the image vertically by a factor.
    /// </summary>
    public class VerticalShearOperation : CopyOperation
    {
        /// <summary>
        /// Shearing factor.
        /// </summary>
        private double factor;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.factor = (double)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Factor,double,0,5";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Vertical Shear";
        }

        /// <summary>
        /// Shears the image vertically by a factor.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.ResultImage.Height; i++)
            {
                for (int j = 0; j < this.ResultImage.Width; j++)
                {
                    int newY = ((int)(i + (this.factor * j))) % this.ResultImage.Height;

                    Pixel oldPixel = this.Image.GetPixel(j, i);
                    this.ResultImage.SetPixel(j, newY, oldPixel);
                }
            }
        }
    }
}
