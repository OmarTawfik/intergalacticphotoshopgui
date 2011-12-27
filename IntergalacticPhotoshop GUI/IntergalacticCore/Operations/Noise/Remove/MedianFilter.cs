namespace IntergalacticCore.Operations.Noise.Remove
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Does noise reduction using median filter.
    /// </summary>
    public class MedianFilter : CopyOperation
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
            return "Median Filter";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            int[,] bitmixed = new int[this.Image.Height, this.Image.Width];

            unsafe
            {
                fixed (int* ptr = &bitmixed[0, 0])
                {
                    RemoveMedianFilterExecute(
                        this.GetCppData(this.Image),
                        this.GetCppData(this.ResultImage),
                        ptr,
                        this.maskSize);
                }
            }
        }

        /// <summary>
        /// the native median filter function.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="res">result image.</param>
        /// <param name="ar">bitmixed array.</param>
        /// <param name="maskSize">mask size.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void RemoveMedianFilterExecute(ImageData src, ImageData res, int* ar, int maskSize);
    }
}
