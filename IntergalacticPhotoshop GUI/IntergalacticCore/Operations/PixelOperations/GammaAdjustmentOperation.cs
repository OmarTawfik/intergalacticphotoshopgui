namespace IntergalacticCore.Operations.PixelOperations
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Adjusts the gamma of the image
    /// </summary>
    public class GammaAdjustmentOperation : BaseOperation
    {
        /// <summary>
        /// The gamma value
        /// </summary>
        private double gammaValue;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (Gamma value).</param>
        public override void SetInput(params object[] input)
        {
            this.gammaValue = (double)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Gamma,double,0.1,10";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Gamma Adjustment";
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
                    Pixel color = this.Image.GetPixel(j, i);
                    double power = this.gammaValue;

                    color.Red = (byte)(Math.Pow(color.Red / 255.0, power) * 255);
                    color.Green = (byte)(Math.Pow(color.Green / 255.0, power) * 255);
                    color.Blue = (byte)(Math.Pow(color.Blue / 255.0, power) * 255);

                    this.Image.SetPixel(j, i, color);
                }
            }
        }
    }
}
