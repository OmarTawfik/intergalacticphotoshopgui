namespace IntergalacticCore.Operations.ResizeOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;

    /// <summary>
    /// Performs a nearest pixel resize on the image contents.
    /// </summary>
    public class NearestNeighbourResizeOperation : CopyOperation
    {
        /// <summary>
        /// Width of the resulting image.
        /// </summary>
        private int newWidth;

        /// <summary>
        /// Height of the resulting image.
        /// </summary>
        private int newHeight;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.newWidth = (int)input[0];
            this.newHeight = (int)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "New Width,int,1,10000|New Height,int,1,10000";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Nearest Neighbour Resize";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            this.ResultImage = this.Image.CreateEmptyClone(this.newWidth, this.newHeight);
            this.ResultImage.BeforeEdit();
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            NearestNeighbourResizeOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage));
        }

        /// <summary>
        /// The native bilinear resize processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void NearestNeighbourResizeOperationExecute(ImageData src, ImageData dest);
    }
}
