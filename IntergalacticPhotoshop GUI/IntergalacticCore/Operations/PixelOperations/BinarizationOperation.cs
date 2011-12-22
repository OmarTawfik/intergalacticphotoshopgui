namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Converts all pixels of the image to a binary value (white or black).
    /// </summary>
    public class BinarizationOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Binarization";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            BinarizationOperationExecute(this.GetCppData(this.Image));
        }

        /// <summary>
        /// The native binarization processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void BinarizationOperationExecute(ImageData src);
    }
}
