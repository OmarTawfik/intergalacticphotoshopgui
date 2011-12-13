namespace IntergalacticCore.Operations.Noise.Remove
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;

    /// <summary>
    /// Does noise reduction using max filter.
    /// </summary>
    public class MaxFilter : BaseNoiseRemovalOperation
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
            return "Max Filter";
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
                    int max = int.MinValue;
                    for (int a = i - side; a <= i + side; a++)
                    {
                        for (int b = j - side; b <= j + side; b++)
                        {
                            max = Math.Max(max, this.GetBitmixedAt(b, a));
                        }
                    }

                    this.ResultImage.SetPixel(j, i, this.FromBitMixed(max));
                }
            }
        }
    }
}
