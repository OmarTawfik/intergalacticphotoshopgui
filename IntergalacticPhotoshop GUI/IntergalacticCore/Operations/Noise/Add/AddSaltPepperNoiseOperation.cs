namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
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
        /// Converts all pixels of the image to a binary value (white or black).
        /// </summary>
        protected override void Operate()
        {
            NoiseType[,] check = new NoiseType[this.Image.Height, this.Image.Width];
            Random rand = new Random();

            int saltCount = (int)(this.saltPercentage * this.Image.Width * this.Image.Height);
            while (saltCount > 0)
            {
                int x = rand.Next(this.Image.Width);
                int y = rand.Next(this.Image.Height);

                if (check[y, x] == NoiseType.None)
                {
                    check[y, x] = NoiseType.Salt;
                    saltCount--;
                }
            }

            int pepperCount = (int)(this.pepperPercentage * this.Image.Width * this.Image.Height);
            while (pepperCount > 0)
            {
                int x = rand.Next(this.Image.Width);
                int y = rand.Next(this.Image.Height);

                if (check[y, x] == NoiseType.None)
                {
                    check[y, x] = NoiseType.Pepper;
                    pepperCount--;
                }
            }

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    if (check[i, j] == NoiseType.Pepper)
                    {
                        this.Image.SetPixel(j, i, Pixel.Black);
                    }
                    else if (check[i, j] == NoiseType.Salt)
                    {
                        this.Image.SetPixel(j, i, Pixel.White);
                    }
                }
            }
        }
    }
}
