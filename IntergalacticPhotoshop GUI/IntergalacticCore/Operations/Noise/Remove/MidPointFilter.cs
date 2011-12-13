namespace IntergalacticCore.Operations.Noise.Remove
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;

    /// <summary>
    /// Does noise reduction using midpoint filter.
    /// </summary>
    public class MidPointFilter : BaseNoiseRemovalOperation
    {
        /// <summary>
        /// Data to be used in the mask.
        /// </summary>
        private int maskSize;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.maskSize = (int)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask Size,int,1,50";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Mid Point Filter";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            int side = (int)this.maskSize / 2;
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    int[] array = new int[this.maskSize * this.maskSize];
                    int arrayPtr = 0;
                    for (int a = i - side; a <= i + side; a++)
                    {
                        for (int b = j - side; b <= j + side; b++)
                        {
                            array[arrayPtr++] = this.GetBitmixedAt(b, a);
                        }
                    }

                    Array.Sort(array);
                    Pixel min = this.FromBitMixed(array[0]);
                    Pixel max = this.FromBitMixed(array[array.Length - 1]);

                    this.ResultImage.SetPixel(j, i, Pixel.Interpolate(min, max));
                }
            }
        }
    }
}
