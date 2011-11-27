namespace IntergalacticCore.Operations.HistogramOperations
{
    using System;
    using IntergalacticCore.Data;

    /// <summary>
    /// Equalizes the histogram by the gray component.
    /// </summary>
    public class HistogramEqualizationOperation : BaseOperation
    {
        /// <summary>
        /// The histogram values for the gray component.
        /// </summary>
        private float[] gray = new float[256];

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Histogram Equalization";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            HistogramCalculator calculator = new HistogramCalculator();
            calculator.Execute(this.Image);

            float runningSum = 0, totalSum = 0;
            foreach (float value in calculator.Gray)
            {
                totalSum += value;
            }

            for (int i = 0; i < this.gray.Length; i++)
            {
                runningSum += calculator.Gray[i];
                this.gray[i] = (runningSum / totalSum) * 255.0f;
            }
        }

        /// <summary>
        /// Equalizes the histogram by the gray component.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel oldPixel = this.Image.GetPixel(j, i);

                    oldPixel.Red = (byte)Math.Round(this.gray[oldPixel.Red]);
                    oldPixel.Green = (byte)Math.Round(this.gray[oldPixel.Green]);
                    oldPixel.Blue = (byte)Math.Round(this.gray[oldPixel.Blue]);

                    this.Image.SetPixel(j, i, oldPixel);
                }
            }
        }
    }
}
