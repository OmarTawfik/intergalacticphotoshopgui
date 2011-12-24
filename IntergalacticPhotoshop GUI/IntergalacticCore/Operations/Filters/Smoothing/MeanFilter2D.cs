namespace IntergalacticCore.Operations.Filters.Smoothing
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Does noise reduction using 2D mean filtering.
    /// </summary>
    public class MeanFilter2D : ConvolutionBase
    {
        /// <summary>
        /// Data to be used in the mask.
        /// </summary>
        private int maskSize;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.maskSize = (int)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask Size,int,1,50";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Mean Filter 2D";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            MeanFilter2DOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage), this.maskSize);
        }

        /// <summary>
        /// The mean filter 2D processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        /// <param name="maskSize">Mask size</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void MeanFilter2DOperationExecute(ImageData src, ImageData dest, int maskSize);
    }
}
