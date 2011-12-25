namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Adjusts the brightness by a given value
    /// </summary>
    public class BrightnessOperation : BaseOperation
    {
        /// <summary>
        /// The brightness value
        /// </summary>
        private int brightness;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.brightness = (int)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Brightness,int,-255,255";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Brightness";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            BrightnessOperationExecute(this.GetCppData(this.Image), this.brightness);
        }

        /// <summary>
        /// The native brightness processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="brightness">brightness input.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void BrightnessOperationExecute(ImageData src, int brightness);
    }
}
