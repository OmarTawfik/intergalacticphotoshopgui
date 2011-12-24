namespace IntergalacticCore.Operations.Filters.Smoothing
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Does noise reduction using 1D mean filtering.
    /// </summary>
    public class MeanFilter1D : ConvolutionBase
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
            return "Mean Filter 1D";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            this.ResultImage = this.Image.CreateCopyClone();
            this.ResultImage.BeforeEdit();
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            MeanFilter1DOperationExecute(this.GetCppData(this.ResultImage), this.GetCppData(this.Image), this.maskSize);
        }

        /// <summary>
        /// The mean filter 1D processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        /// <param name="maskSize">Mask size</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void MeanFilter1DOperationExecute(ImageData src, ImageData dest, int maskSize);
    }
}
