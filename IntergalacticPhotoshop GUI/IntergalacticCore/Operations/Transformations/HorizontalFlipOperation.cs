namespace IntergalacticCore.Operations.Transformations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Flips the image horizontally.
    /// </summary>
    public class HorizontalFlipOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Horizontal Flip";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            HorizontalFlipOperationExecute(this.GetCppData(this.Image));
        }

        /// <summary>
        /// The native H flip processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void HorizontalFlipOperationExecute(ImageData src);
    }
}
