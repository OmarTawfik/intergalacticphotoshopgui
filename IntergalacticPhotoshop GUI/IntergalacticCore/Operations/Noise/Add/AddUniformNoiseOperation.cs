﻿namespace IntergalacticCore.Operations.Noise.Add
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Modifies each pixel by a uniform noise.
    /// </summary>
    public class AddUniformNoiseOperation : BaseOperation
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
        /// Applies the actual operation on the image.
        /// </summary>
        protected override void Operate()
        {
            bool[,] check = new bool[this.Image.Height, this.Image.Width];
            Random rand = new Random();

            int count = (int)(this.percentage * this.Image.Width * this.Image.Height);
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
