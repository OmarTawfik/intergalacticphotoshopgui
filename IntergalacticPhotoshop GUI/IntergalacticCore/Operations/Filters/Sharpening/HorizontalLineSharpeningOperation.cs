namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Sharpens image horizontal lines using laplacian filter.
    /// </summary>
    public class HorizontalLineSharpeningOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Horizontal Line Sharpening";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            HorizontalLineSharpeningOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage));
        }

        /// <summary>
        /// The horizonal line sharpening processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void HorizontalLineSharpeningOperationExecute(ImageData src, ImageData dest);
    }
}
