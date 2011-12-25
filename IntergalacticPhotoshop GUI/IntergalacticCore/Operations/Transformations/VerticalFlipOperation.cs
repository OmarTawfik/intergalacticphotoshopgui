namespace IntergalacticCore.Operations.Transformations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Flips the image vertically.
    /// </summary>
    public class VerticalFlipOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Vertical Flip";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            VerticalFlipOperationExecute(this.GetCppData(this.Image));
        }

        /// <summary>
        /// The native V flip processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void VerticalFlipOperationExecute(ImageData src);
    }
}
