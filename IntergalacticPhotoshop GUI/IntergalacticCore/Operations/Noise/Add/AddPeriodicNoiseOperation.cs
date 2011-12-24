namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Modifies each pixel by a periodic noise.
    /// </summary>
    public class AddPeriodicNoiseOperation : BaseOperation
    {
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
            this.amplitude = (byte)input[0];
            this.frequencyX = (double)input[1];
            this.frequencyY = (double)input[2];
            this.shiftX = (double)input[3];
            this.shiftY = (double)input[4];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Amplitude,byte_slider,0,255|Frequency in X,double,0,100|"
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
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);

                    double part1 = (2 * Math.PI * this.frequencyX * j) / (this.Image.Width + this.shiftX);
                    double part2 = (2 * Math.PI * this.frequencyY * i) / (this.Image.Height + this.shiftY);
                    double noise = this.amplitude * Math.Sin(part1 + part2);

                    p.Red = (byte)(p.Red + noise);
                    p.Green = (byte)(p.Green + noise);
                    p.Blue = (byte)(p.Blue + noise);

                    this.Image.SetPixel(j, i, p);
                }
            }
        }

        /// <summary>
        /// Gets called after the operation ends.
        /// </summary>
        protected override void AfterOperate()
        {
            NormalizationOperation operation = new NormalizationOperation();
            this.Image = operation.Execute(this.Image);
        }
    }
}
