namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Applies a custom mask to the image.
    /// </summary>
    public class CustomMaskOperation : ConvolutionBase
    {
        /// <summary>
        /// Holds the mask data to be applied to the image.
        /// </summary>
        private ConvolutionMask mask;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.mask = (ConvolutionMask)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask,mask";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Custom Mask";
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
                    double red = 0, green = 0, blue = 0;
                    for (int y = i - (this.mask.Height / 2), a = 0; y <= i + (this.mask.Height / 2); y++, a++)
                    {
                        for (int x = j - (this.mask.Width / 2), b = 0; x <= j + (this.mask.Width / 2); x++, b++)
                        {
                            if (this.mask.Data[a, b] != 0)
                            {
                                Pixel p = this.GetLocation(x, y);
                                red += this.mask.Data[a, b] * p.Red;
                                green += this.mask.Data[a, b] * p.Green;
                                blue += this.mask.Data[a, b] * p.Blue;
                            }
                        }
                    }

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff((int)red, (int)green, (int)blue));
                }
            }
        }
    }
}
