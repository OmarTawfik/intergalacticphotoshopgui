namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Modifies each pixel by a uniform noise.
    /// </summary>
    public class AddUniformNoiseOperation : BaseOperation
    {
        /// <summary>
        /// Ammount of noise to add.
        /// </summary>
        private byte ammount;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.ammount = (byte)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Ammount,byte_slider,0,255";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Add Uniform Noise";
        }

        /// <summary>
        /// Converts all pixels of the image to a binary value (white or black).
        /// </summary>
        protected override void Operate()
        {
            Random rand = new Random();
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);

                    double randomRed = this.ammount * (rand.NextDouble() - 0.5);
                    double randomGreen = this.ammount * (rand.NextDouble() - 0.5);
                    double randomBlue = this.ammount * (rand.NextDouble() - 0.5);

                    p = Pixel.CutOff(p.Red + randomRed, p.Green + randomGreen, p.Blue + randomBlue);
                    this.Image.SetPixel(j, i, p);
                }
            }
        }
    }
}
