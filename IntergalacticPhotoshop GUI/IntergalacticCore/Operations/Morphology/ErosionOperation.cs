namespace IntergalacticCore.Operations.Morphology
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Erosion Operation
    /// </summary>
    public class ErosionOperation : CopyOperation
    {
        /// <summary>
        /// Binary mask to be used in the operation
        /// </summary>
        private BinaryMask mask;

        /// <summary>
        /// Horizontal center of mask.
        /// </summary>
        private int centerX;

        /// <summary>
        /// Vertical center of mask.
        /// </summary>
        private int centerY;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.mask = (BinaryMask)input[0];
            this.centerX = (int)input[1];
            this.centerY = (int)input[2];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask,binaryMask|X Center,int,0,8|Y Center,int,0,8";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Erosion";
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
                    ErosionOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage), arrPointer, this.mask.Data.GetLength(1), this.mask.Data.GetLength(0), this.centerX, this.centerY);
                }
            }
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            base.BeforeOperate();
            BinarizationOperation operation = new BinarizationOperation();
            operation.SetInput(false, 128);
            this.Image = operation.Execute(this.Image);
        }

        /// <summary>
        /// The erosion native processing function
        /// </summary>
        /// <param name="src">Source image</param>
        /// <param name="dest">Destinated image</param>
        /// <param name="mask">The Binary mask</param>
        /// <param name="maskWidth">Mask width</param>
        /// <param name="maskHeight">Mask height</param>
        /// <param name="centerX">horizontal center.</param>
        /// <param name="centerY">vertical center.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void ErosionOperationExecute(ImageData src, ImageData dest, bool* mask, int maskWidth, int maskHeight, int centerX, int centerY);
    }
}
