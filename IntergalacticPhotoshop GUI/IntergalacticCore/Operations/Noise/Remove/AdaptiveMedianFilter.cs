namespace IntergalacticCore.Operations.Noise.Remove
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;

    /// <summary>
    /// Does noise reduction using adaptive median filter.
    /// </summary>
    public class AdaptiveMedianFilter : BaseNoiseRemovalOperation
    {
        /// <summary>
        /// Data to be used in the mask.
        /// </summary>
        private int maxMaskSize;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.maxMaskSize = (int)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Max Mask Size,int,3,50";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Adaptive Median Filter";
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
                    for (int currentSize = 3; currentSize <= this.maxMaskSize; currentSize += 2)
                    {
                        int side = currentSize / 2;
                        int[] array = new int[currentSize * currentSize];
                        int arrayPtr = 0;

                        for (int a = i - side; a <= i + side; a++)
                        {
                            for (int b = j - side; b <= j + side; b++)
                            {
                                array[arrayPtr++] = this.GetBitmixedAt(b, a);
                            }
                        }

                        Array.Sort(array);

                        int min = array[0], max = array[array.Length - 1];
                        int current = this.GetBitmixedAt(j, i), mid = array[array.Length / 2];

                        if (mid != min && mid != max)
                        {
                            if (current != min && current != max)
                            {
                                this.ResultImage.SetPixel(j, i, this.FromBitMixed(current));
                            }
                            else
                            {
                                this.ResultImage.SetPixel(j, i, this.FromBitMixed(mid));
                            }

                            break;
                        }
                        else
                        {
                            if (currentSize == this.maxMaskSize)
                            {
                                this.ResultImage.SetPixel(j, i, this.FromBitMixed(mid));
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
