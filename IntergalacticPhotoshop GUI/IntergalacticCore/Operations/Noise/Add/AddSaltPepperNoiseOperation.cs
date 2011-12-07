namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;

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
            this.saltPercentage = (double)input[0];
            this.pepperPercentage = (double)input[1];
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
        /// Converts all pixels of the image to a binary value (white or black).
        /// </summary>
        protected override void Operate()
        {
            this.AddNoise(Pixel.White, this.saltPercentage);
            this.AddNoise(Pixel.Black, this.pepperPercentage);
        }

        /// <summary>
        /// Adds the specified color as a noise.
        /// </summary>
        /// <param name="color">Color to be added.</param>
        /// <param name="percentage">Percentage of noise.</param>
        private void AddNoise(Pixel color, double percentage)
        {
            bool[,] check = new bool[this.Image.Height, this.Image.Width];
            int count = (int)((this.Image.Width * this.Image.Height) * percentage / 100);
            Random rand = new Random();

            while (count > 0)
            {
                int x = rand.Next(this.Image.Width);
                int y = rand.Next(this.Image.Height);

                if (check[y, x] == false)
                {
                    check[y, x] = true;
                    count--;
                }
            }

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    if (check[i, j] == true)
                    {
                        this.Image.SetPixel(j, i, color);
                    }
                }
            }
        }
    }
}
