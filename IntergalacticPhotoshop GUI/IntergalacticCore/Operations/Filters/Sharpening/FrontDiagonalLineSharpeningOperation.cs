namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Sharpens image front diagonal lines using laplacian filter.
    /// </summary>
    public class FrontDiagonalLineSharpeningOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Front Diagonal Line Sharpening";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            FrontDiagonalLineSharpeningOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage));
        }

        /// <summary>
        /// The front diagonal line sharpening processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void FrontDiagonalLineSharpeningOperationExecute(ImageData src, ImageData dest);
    }
}
