namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Adjusts the contrast of the image
    /// </summary>
    public class ContrastOperation : BaseOperation
    {
        /// <summary>
        /// Min/Max histogram values
        /// </summary>
        private int oldMin, oldMax, newMin, newMax;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (oldMin/Max and newMin/Max).</param>
        public override void SetInput(params object[] input)
        {
            this.oldMin = (int)input[0];
            this.oldMax = (int)input[1];
            this.newMin = (int)input[2];
            this.newMax = (int)input[3];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Old Min,int,0,255|Old Max,int,0,255|New Min,int,0,255|New Max,int,0,255";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Contrast";
        }

        /// <summary>
        /// Adjusts the contrast of the image by given histogram values
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);

                    float oldMultiplier = (this.oldMax - this.oldMin) / 255.0f;
                    float newMultiplier = (this.newMax - this.newMin) / 255.0f;

                    int r = p.Red - this.oldMin;
                    int g = p.Green - this.oldMin;
                    int b = p.Blue - this.oldMin;

                    r = (int)(((r / oldMultiplier) * newMultiplier) + this.newMin);
                    g = (int)(((g / oldMultiplier) * newMultiplier) + this.newMin);
                    b = (int)(((b / oldMultiplier) * newMultiplier) + this.newMin);

                    if (r > 255)
                    {
                        r = 255;
                    }

                    if (g > 255)
                    {
                        g = 255;
                    }

                    if (b > 255)
                    {
                        b = 255;
                    }

                    if (r < 0)
                    {
                        r = 0;
                    }

                    if (g < 0)
                    {
                        g = 0;
                    }

                    if (b < 0)
                    {
                        b = 0;
                    }

                    p.Red = (byte)r;
                    p.Green = (byte)g;
                    p.Blue = (byte)b;

                    this.Image.SetPixel(j, i, p);
                }
            }
        }
    }
}
