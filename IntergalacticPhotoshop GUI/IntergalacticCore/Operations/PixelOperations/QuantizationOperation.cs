namespace IntergalacticCore.Operations.PixelOperations
{
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
            return "Bits Per Channel,byte_slider,1,8";
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
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel color = this.Image.GetPixel(j, i);
                    byte shiftAmount = (byte)(8 - this.bitsPerChannel);

                    color.Red = (byte)((byte)(color.Red >> shiftAmount) << shiftAmount);
                    color.Green = (byte)((byte)(color.Green >> shiftAmount) << shiftAmount);
                    color.Blue = (byte)((byte)(color.Blue >> shiftAmount) << shiftAmount);

                    this.Image.SetPixel(j, i, color);
                }
            }
        }
    }
}
