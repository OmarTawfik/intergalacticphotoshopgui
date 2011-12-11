namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Modifies each pixel by an exponential noise.
    /// </summary>
    public class AddExponentialNoiseOperation : BaseNoiseAdditionOperation
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
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.percentage = (double)input[0] / 100;
            this.mean = (double)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Ammount,double,0,100|Mean,double,0,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Add Exponential Noise";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            bool[,] table = this.GetRandomTable(this.percentage);
            Random rand = new Random();

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    if (table[i, j] == true)
                    {
                        Pixel p = this.Image.GetPixel(j, i);

                        p = Pixel.CutOff(
                            p.Red + this.GetValue(rand.NextDouble()),
                            p.Green + this.GetValue(rand.NextDouble()),
                            p.Blue + this.GetValue(rand.NextDouble()));

                        this.Image.SetPixel(j, i, p);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a distribution value from a random number.
        /// </summary>
        /// <param name="randomVariable">random variable to start with.</param>
        /// <returns>the exponential distribution member.</returns>
        private double GetValue(double randomVariable)
        {
            if (randomVariable != 0)
            {
                return -this.mean * Math.Log(randomVariable, Math.Exp(1));
            }
            else
            {
                return 0;
            }
        }
    }
}
