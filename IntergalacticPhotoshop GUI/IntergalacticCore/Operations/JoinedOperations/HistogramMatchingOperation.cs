namespace IntergalacticCore.Operations.JoinedOperations
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;

    /// <summary>
    /// Matches the histogram of an image to another one
    /// </summary>
    public class HistogramMatchingOperation : BaseOperation
    {
        /// <summary>
        /// The bitmap to be matched
        /// </summary>
        private ImageBase otherBitmap;

        /// <summary>
        /// Array of values used to match histograms
        /// </summary>
        private int[] gray;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (the image to be matched).</param>
        public override void SetInput(params object[] input)
        {
            this.otherBitmap = (ImageBase)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Other Image,image";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Histogram Matching";
        }

        /// <summary>
        /// Pre operations function
        /// </summary>
        protected override void BeforeOperate()
        {
            HistogramCalculator operation = new HistogramCalculator();
            operation.Execute(this.Image);

            int[] gray1 = (int[])operation.Gray.Clone();

            float totalSum = this.Sum(gray1), runningSum = 0.0f;
            for (int i = 0; i < gray1.Length; i++)
            {
                runningSum += gray1[i];
                gray1[i] = (int)((runningSum / totalSum) * 255);
            }

            operation.Execute(this.otherBitmap);

            int[] gray2 = (int[])operation.Gray.Clone();

            totalSum = this.Sum(gray2);
            runningSum = 0.0f;
            for (int i = 0; i < gray2.Length; i++)
            {
                runningSum += gray2[i];
                gray2[i] = (int)((runningSum / totalSum) * 255);
            }

            this.gray = new int[256];
            for (int i = 0; i < gray1.Length; i++)
            {
                int finalValue = 0;
                int different = 255;
                int currentDifference;
                for (int j = 0; j < gray2.Length; j++)
                {
                    currentDifference = (int)Math.Abs(gray1[i] - gray2[j]);
                    if (different > currentDifference)
                    {
                        different = currentDifference;
                        finalValue = j;
                    }
                }

                this.gray[i] = finalValue;
            }
        }

        /// <summary>
        /// Matches the histogram of the two input images
        /// </summary>
        protected override void Operate()
        {
            for (int y = 0; y < this.Image.Height; y++)
            {
                for (int x = 0; x < this.Image.Width; x++)
                {
                    Pixel oldPixel = this.Image.GetPixel(x, y);

                    oldPixel.Red = (byte)this.gray[oldPixel.Red];
                    oldPixel.Green = (byte)this.gray[oldPixel.Green];
                    oldPixel.Blue = (byte)this.gray[oldPixel.Blue];

                    this.Image.SetPixel(x, y, oldPixel);
                }
            }
        }

        /// <summary>
        /// Sums the input array
        /// </summary>
        /// <param name="array">The array to sum</param>
        /// <returns>Sum of the array</returns>
        private int Sum(int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }

            return sum;
        }
    }
}
