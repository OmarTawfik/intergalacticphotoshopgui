namespace IntergalacticCore.Operations.Morphology
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;

    /// <summary>
    /// Erosion Operation
    /// </summary>
    public class DialationOperation : ConvolutionBase
    {
        /// <summary>
        /// Binary mask to be used in the operation
        /// </summary>
        private BinaryMask mask;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.mask = (BinaryMask)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask,binaryMask";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Dialation";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            unsafe
            {
                fixed (bool* arrPointer = &this.mask.Data[0, 0])
                {
                    DialationOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage), arrPointer, this.mask.Data.GetLength(1), this.mask.Data.GetLength(0));
                }
            }
        }

        /// <summary>
        /// The dialation native processing function
        /// </summary>
        /// <param name="src">Source image</param>
        /// <param name="dest">Destinated image</param>
        /// <param name="mask">The Binary mask</param>
        /// <param name="maskWidth">Mask width</param>
        /// <param name="maskHeight">Mask height</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void DialationOperationExecute(ImageData src, ImageData dest, bool* mask, int maskWidth, int maskHeight);
    }
}
