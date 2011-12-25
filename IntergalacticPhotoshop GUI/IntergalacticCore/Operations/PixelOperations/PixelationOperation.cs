namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Pixelates the image with a pixel size.
    /// </summary>
    public class PixelationOperation : BaseOperation
    {
        /// <summary>
        /// Pixel Size.
        /// </summary>
        private int pixelSize;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (oldMin/Max and newMin/Max).</param>
        public override void SetInput(params object[] input)
        {
            this.pixelSize = (int)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Pixel Size,int,1,200";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Pixelate";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            PixelationOperationExecute(this.GetCppData(this.Image), this.pixelSize);
        }

        /// <summary>
        /// The native pixelation processing function.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="size">pixel size.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void PixelationOperationExecute(ImageData src, int size);
    }
}
