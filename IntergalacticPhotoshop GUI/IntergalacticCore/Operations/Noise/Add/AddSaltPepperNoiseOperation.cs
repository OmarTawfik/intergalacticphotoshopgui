namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// The noise type for this pixel.
    /// </summary>
    public enum NoiseType
    {
        /// <summary>
        /// No noise. Original pixel is put.
        /// </summary>
        None,

        /// <summary>
        /// Salt noise. White pixel is put.
        /// </summary>
        Salt,

        /// <summary>
        /// Pepper noise. Black pixel is put.
        /// </summary>
        Pepper,
    }

    /// <summary>
    /// Adds random white and black pixels to the image.
    /// </summary>
    public class AddSaltPepperNoiseOperation : BaseOperation
    {
        /// <summary>
        /// Black pixels percentage.
        /// </summary>
        private double pepperPercentage;

        /// <summary>
        /// White pixels percentage.
        /// </summary>
        private double saltPercentage;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.saltPercentage = (double)input[0] / 100.0;
            this.pepperPercentage = (double)input[1] / 100.0;

            if (this.saltPercentage + this.pepperPercentage > 1.0)
            {
                throw new Exception("Salt Percentage + Pepper Percentage must be less than or equal 100%");
            }
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Salt Percentage,double,0,100|Pepper Percentage,double,0,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Add Salt & Pepper Noise";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            int[,] random = new int[this.Image.Height, this.Image.Width];
            unsafe
            {
                fixed (int* ptr = &random[0, 0])
                {
                    SaltandPepperNoiseAdditionOperationExecute(
                        this.GetCppData(this.Image),
                        ptr,
                        this.saltPercentage,
                        this.pepperPercentage);
                }
            }
        }

        /// <summary>
        /// the native salt and pepper processing function.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="ar">random array.</param>
        /// <param name="salt">salt percentage.</param>
        /// <param name="pepper">pepper percentage.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void SaltandPepperNoiseAdditionOperationExecute(ImageData src, int* ar, double salt, double pepper);
    }
}
