namespace IntergalacticCore.Operations.Transformations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Shears the image horizontally by a factor.
    /// </summary>
    public class HorizontalShearOperation : CopyOperation
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
            return "Horizontal Shear";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            HorizontalShearOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage), this.factor);
        }

        /// <summary>
        /// The native H shear processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        /// <param name="factor">Shear factor</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void HorizontalShearOperationExecute(ImageData src, ImageData dest, double factor);
    }
}
