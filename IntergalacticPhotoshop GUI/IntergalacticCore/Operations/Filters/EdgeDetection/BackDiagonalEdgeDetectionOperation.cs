namespace IntergalacticCore.Operations.Filters.EdgeDetection
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Detects back diagonal edges using laplacian filter.
    /// </summary>
    public class BackDiagonalEdgeDetectionOperation : BaseEdgeDetectionOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Back Diagonal Edge Detection";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            BackDiagonalEdgeDetectionOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage));
        }

        /// <summary>
        /// The back diagonal line edge detection processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void BackDiagonalEdgeDetectionOperationExecute(ImageData src, ImageData dest);
    }
}
