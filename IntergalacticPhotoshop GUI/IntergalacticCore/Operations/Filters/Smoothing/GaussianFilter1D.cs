namespace IntergalacticCore.Operations.Filters.Smoothing
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Applys Gaussian blur 1D on the given image.
    /// </summary>
    public class GaussianFilter1D : ConvolutionBase
    {
        /// <summary>
        /// Data to be used in the mask.
        /// </summary>
        private double sigma;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.sigma = (double)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Sigma,double,0,5";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Gaussian Filter 1D";
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
            GaussianFilter1DOperationExecute(this.GetCppData(this.ResultImage), this.GetCppData(this.Image), this.sigma);
        }

        /// <summary>
        /// The gaussian filter 1D processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        /// <param name="sigma">Sigma value</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void GaussianFilter1DOperationExecute(ImageData src, ImageData dest, double sigma);
    }
}
