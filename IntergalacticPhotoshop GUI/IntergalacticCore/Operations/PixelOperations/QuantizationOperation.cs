namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Reduces the number of bits per channel for the image
    /// </summary>
    public class QuantizationOperation : BaseOperation
    {
        /// <summary>
        /// Number of bits per channel
        /// </summary>
        private byte bitsPerChannel;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (bits per channel).</param>
        public override void SetInput(params object[] input)
        {
            this.bitsPerChannel = (byte)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Bits Per Channel,byte_slider,1,7";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Quantization";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            QuantizationOperationExecute(this.GetCppData(this.Image), this.bitsPerChannel);
        }

        /// <summary>
        /// The native quantization processing function.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="bits">bits per channel</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void QuantizationOperationExecute(ImageData src, int bits);
    }
}
