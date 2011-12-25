namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Extracts certain colors from the image.
    /// </summary>
    public class ColorExtractionOperation : BaseOperation
    {
        /// <summary>
        /// Keeping Red Component.
        /// </summary>
        private bool red;

        /// <summary>
        /// Keeping Green Component.
        /// </summary>
        private bool green;

        /// <summary>
        /// Keeping Blue Component.
        /// </summary>
        private bool blue;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (oldMin/Max and newMin/Max).</param>
        public override void SetInput(params object[] input)
        {
            this.red = (bool)input[0];
            this.green = (bool)input[1];
            this.blue = (bool)input[2];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Red,bool|Green,bool|Blue,bool";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Color Extraction";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            ColorExtractionOperationExecute(
                this.GetCppData(this.Image),
                this.red,
                this.green,
                this.blue);
        }

        /// <summary>
        /// The native color extraction processing function.
        /// </summary>
        /// <param name="src">Source image.</param>
        /// <param name="r">Keep red.</param>
        /// <param name="g">Keep green.</param>
        /// <param name="b">Keep blue.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ColorExtractionOperationExecute(ImageData src, bool r, bool g, bool b);
    }
}
