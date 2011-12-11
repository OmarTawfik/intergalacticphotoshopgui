namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Modifies each pixel by a gaussian noise.
    /// </summary>
    public class AddGaussianNoiseOperation : BaseNoiseAdditionOperation
    {
        /// <summary>
        /// Ammount of noise to add.
        /// </summary>
        private double percentage;

        /// <summary>
        /// Mean of the normal graph.
        /// </summary>
        private double mean;

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
            this.mean = (double)input[1];
            this.variance = (double)input[2];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Ammount,double,0,100|Mean,double,0,100|Variance,double,0,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Add Gaussian Noise";
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
                        
                        double powerRed = Math.Pow(rand.NextDouble() - this.mean, 2) / (2 * this.variance);
                        double powerGreen = Math.Pow(rand.NextDouble() - this.mean, 2) / (2 * this.variance);
                        double powerBlue = Math.Pow(rand.NextDouble() - this.mean, 2) / (2 * this.variance);

                        p = Pixel.CutOff(
                            p.Red + (Math.Pow(Math.E, -powerRed) / div),
                            p.Green + (Math.Pow(Math.E, -powerGreen) / div),
                            p.Blue + (Math.Pow(Math.E, -powerBlue) / div));

                        this.Image.SetPixel(j, i, p);
                    }
                }
            }
        }
    }
}
