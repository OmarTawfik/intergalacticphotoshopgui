﻿namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Modifies each pixel by a uniform noise.
    /// </summary>
    public class AddUniformNoiseOperation : BaseNoiseAdditionOperation
    {
        /// <summary>
        /// Ammount of noise to add.
        /// </summary>
        private double percentage;

        /// <summary>
        /// Start of the uniform graph.
        /// </summary>
        private byte start;

        /// <summary>
        /// End of the uniform graph.
        /// </summary>
        private byte end;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.percentage = (double)input[0] / 100;
            this.start = (byte)input[1];
            this.end = (byte)input[2];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Ammount,double,0,100|Start,byte_slider,0,255|End,byte_slider,0,255";
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
                            p.Red + this.start + (rand.NextDouble() * (this.end - this.start)),
                            p.Green + this.start + (rand.NextDouble() * (this.end - this.start)),
                            p.Blue + this.start + (rand.NextDouble() * (this.end - this.start)));
                        this.Image.SetPixel(j, i, p);
                    }
                }
            }
        }
    }
}
