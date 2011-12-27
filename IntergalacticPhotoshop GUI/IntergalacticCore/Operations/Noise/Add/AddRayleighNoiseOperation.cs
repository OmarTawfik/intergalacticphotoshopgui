namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Modifies each pixel by a rayleigh noise.
    /// </summary>
    public class AddRayleighNoiseOperation : BaseOperation
    {
        /// <summary>
        /// Ammount of noise to add.
        /// </summary>
        private double percentage;

        /// <summary>
        /// Variance of the rayleigh graph.
        /// </summary>
        private double sigma;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.percentage = (double)input[0] / 100;
            this.sigma = (double)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Ammount,double,0,100|Sigma,double,0,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Add Rayleigh Noise";
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
                    AddRayleighNoiseExecute(
                        this.GetCppData(this.Image),
                        pred,
                        pgreen,
                        pblue,
                        this.percentage,
                        this.sigma);
                }
            }
        }

        /// <summary>
        /// The native rayleigh noise processing function.
        /// </summary>
        /// <param name="source">source image.</param>
        /// <param name="red">random red.</param>
        /// <param name="green">random green.</param>
        /// <param name="blue">random blue.</param>
        /// <param name="percentage">percentage of noise.</param>
        /// <param name="sigma">sigma of rayleigh graph.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void AddRayleighNoiseExecute(ImageData source, int* red, int* green, int* blue, double percentage, double sigma);
    }
}
