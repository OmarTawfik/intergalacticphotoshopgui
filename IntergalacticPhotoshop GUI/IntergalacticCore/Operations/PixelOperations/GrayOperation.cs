namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Converts the image colors to gray
    /// </summary>
    public class GrayOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Gray Colors";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            GrayOperationExecute(this.GetCppData(this.Image));
        }

        /// <summary>
        /// The native gray operation processing function.
        /// </summary>
        /// <param name="src">source image.</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void GrayOperationExecute(ImageData src);
    }
}
