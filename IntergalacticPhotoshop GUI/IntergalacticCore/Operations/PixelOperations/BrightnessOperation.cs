namespace IntergalacticCore.Operations.PixelOperations
{
    using System;
    using System.Collections.Generic;
    using IntergalacticCore.Data;

    /// <summary>
    /// Adjusts the brightness by a given value
    /// </summary>
    public class BrightnessOperation : BaseOperation
    {
        /// <summary>
        /// The brightness value
        /// </summary>
        private byte brightness;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (brightness value).</param>
        public override void SetInput(params object[] input)
        {
            this.brightness = (byte)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Brightness,byte_slider,0,255";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Brightness";
        }

        /// <summary>
        /// Adjusts the brightness of the image.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);

                    int r = p.Red + this.brightness;
                    int g = p.Green + this.brightness;
                    int b = p.Blue + this.brightness;

                    this.Image.SetPixel(j, i, Pixel.CutOff(r, g, b));
                }
            }
        }
    }
}
