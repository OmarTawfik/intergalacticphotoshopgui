namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Applies a custom mask to the image.
    /// </summary>
    public class CustomMaskOperation : ConvolutionBase
    {
        /// <summary>
        /// Holds the mask data to be applied to the image.
        /// </summary>
        private ConvolutionMask mask;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.mask = (ConvolutionMask)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask,mask";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Custom Mask";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            unsafe
            {
                fixed (double* arrPointer = &this.mask.Data[0, 0])
                {
                    CustomMaskOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage), arrPointer, this.mask.Data.GetLength(0));
                }
            }
        }

        /// <summary>
        /// The custom mask processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        /// <param name="mask">mask array</param>
        /// <param name="maskSize">mask size</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void CustomMaskOperationExecute(ImageData src, ImageData dest, double* mask, int maskSize);
    }
}
