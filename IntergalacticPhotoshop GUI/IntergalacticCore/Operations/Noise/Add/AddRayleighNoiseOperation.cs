namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Modifies each pixel by a rayleigh noise.
    /// </summary>
    public class AddRayleighNoiseOperation : BaseNoiseAdditionOperation
    {
        /// <summary>
        /// Ammount of noise to add.
        /// </summary>
        private double percentage;

        /// <summary>
        /// Variance of the normal graph.
        /// </summary>
        private double variance;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.percentage = (double)input[0] / 100;
            this.variance = (double)input[1];

            if (this.variance == 0)
            {
                throw new Exception("Variance cannot be equal zero.");
            }
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Ammount,double,0,100|Variance,double,0,100";
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
            bool[,] table = this.GetRandomTable(this.percentage);

            Random rand = new Random();
            double div = Math.Sqrt(2 * Math.PI * this.variance);

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    if (table[i, j] == true)
                    {
                        Pixel p = this.Image.GetPixel(j, i);

                        p = Pixel.CutOff(
                            p.Red + this.GetNoise(rand.NextDouble()),
                            p.Green + this.GetNoise(rand.NextDouble()),
                            p.Blue + this.GetNoise(rand.NextDouble()));

                        this.Image.SetPixel(j, i, p);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the specified noise.
        /// </summary>
        /// <param name="x">Random Variable.</param>
        /// <returns>Rayleigh Noise.</returns>
        private double GetNoise(double x)
        {
            return x * Math.Exp(-x * x / (2 * this.variance)) / this.variance;
        }
    }
}
