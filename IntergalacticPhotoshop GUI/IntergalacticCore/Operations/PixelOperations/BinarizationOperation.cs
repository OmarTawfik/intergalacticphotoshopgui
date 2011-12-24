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
        /// Threshold for binarization
        /// </summary>
        private int threshold = 0;

        /// <summary>
        /// If the operation should calculate the threshold by its own.
        /// </summary>
        private bool calculateThreshold;

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Binarization";
        }

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.calculateThreshold = (bool)input[0];
            this.threshold = (int)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Calculate Threshold,bool|Threshold,int,0,255";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            BinarizationOperationExecute(
                this.GetCppData(this.Image),
                this.calculateThreshold,
                this.threshold);
        }

        /// <summary>
        /// The native binarization processing function.
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="calculate">true if calculating the threshold.</param>
        /// <param name="threshold">input threshold</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void BinarizationOperationExecute(ImageData src, bool calculate, int threshold);
    }
}
