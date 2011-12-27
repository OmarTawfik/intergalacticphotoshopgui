namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Modifies each pixel by a periodic noise.
    /// </summary>
    public class AddPeriodicNoiseOperation : BaseOperation
    {
        /// <summary>
        /// Ammount of noise to add.
        /// </summary>
        private double percentage;

        /// <summary>
        /// Ammount of noise to add.
        /// </summary>
        private byte amplitude;

        /// <summary>
        /// Frequency Component in X direction.
        /// </summary>
        private double frequencyX;

        /// <summary>
        /// Frequency component in Y direction.
        /// </summary>
        private double frequencyY;

        /// <summary>
        /// Phase shift in X direction.
        /// </summary>
        private double shiftX;

        /// <summary>
        /// Phase shift in Y direction.
        /// </summary>
        private double shiftY;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.percentage = (double)input[0] / 100;
            this.amplitude = (byte)input[1];
            this.frequencyX = (double)input[2];
            this.frequencyY = (double)input[3];
            this.shiftX = (double)input[4];
            this.shiftY = (double)input[5];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Ammount,double,0,100|Amplitude,byte_slider,0,255|Frequency in X,double,0,100|"
                + "Frequency in Y,double,0,100|Shift in X,double,0,100|Shift in Y,double,0,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Add Periodic Noise";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            int[,] red = new int[this.Image.Height, this.Image.Width];
            int[,] green = new int[this.Image.Height, this.Image.Width];
            int[,] blue = new int[this.Image.Height, this.Image.Width];

            unsafe
            {
                fixed (int* pred = &red[0, 0], pgreen = &green[0, 0], pblue = &blue[0, 0])
                {
                    AddPeriodicNoiseExecute(
                        this.GetCppData(this.Image),
                        pred,
                        pgreen,
                        pblue,
                        this.percentage,
                        this.amplitude,
                        this.frequencyX,
                        this.frequencyY,
                        this.shiftX,
                        this.shiftY);
                }
            }
        }

        /// <summary>
        /// the native periodic noise processing function.
        /// </summary>
        /// <param name="source">source image</param>
        /// <param name="red">random red array</param>
        /// <param name="green">random green array</param>
        /// <param name="blue">random blue array</param>
        /// <param name="percentage">percentage of noise</param>
        /// <param name="amplitude">amplitude of periodic noise</param>
        /// <param name="frequencyX">horizontal frequency of periodic noise</param>
        /// <param name="frequencyY">vertical frequency of periodic noise</param>
        /// <param name="shiftX">horizontal shift of periodic noise</param>
        /// <param name="shiftY">vertical shift of periodic noise</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void AddPeriodicNoiseExecute(ImageData source, int* red, int* green, int* blue, double percentage, double amplitude, double frequencyX, double frequencyY, double shiftX, double shiftY);
    }
}
