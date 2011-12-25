namespace IntergalacticCore.Operations.Filters.EdgeDetection
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Detects horizontal edges using laplacian filter.
    /// </summary>
    public class HorizontalEdgeDetectionOperation : BaseEdgeDetectionOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Horizontal Edge Detection";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            HorizontalEdgeDetectionOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage));
        }

        /// <summary>
        /// The horizontal line edge detection processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void HorizontalEdgeDetectionOperationExecute(ImageData src, ImageData dest);
    }
}
