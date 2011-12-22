namespace IntergalacticCore.Operations.PixelOperations
{
    using System;
    using System.Runtime.InteropServices;
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
            GammaAdjustmentOperationExecute(this.GetCppData(this.Image), this.gammaValue);
        }

        /// <summary>
        /// The native gamma adjustment processing function.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="gamma">gamma value.</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void GammaAdjustmentOperationExecute(ImageData src, double gamma);
    }
}
