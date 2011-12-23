namespace IntergalacticCore.Operations.HistogramOperations
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Calculates all histogram values for an image.
    /// </summary>
    public class HistogramCalculator : BaseOperation
    {
        /// <summary>
        /// The histogram values for the red component.
        /// </summary>
        private int[] red = new int[256];

        /// <summary>
        /// The histogram values for the green component.
        /// </summary>
        private int[] green = new int[256];

        /// <summary>
        /// The histogram values for the blue component.
        /// </summary>
        private int[] blue = new int[256];

        /// <summary>
        /// The histogram values for the gray component.
        /// </summary>
        private int[] gray = new int[256];

        /// <summary>
        /// Gets the histogram values for the red component.
        /// </summary>
        public int[] Red
        {
            get { return this.red; }
        }

        /// <summary>
        /// Gets the histogram values for the green component.
        /// </summary>
        public int[] Green
        {
            get { return this.green; }
        }

        /// <summary>
        /// Gets the histogram values for the blue component.
        /// </summary>
        public int[] Blue
        {
            get { return this.blue; }
        }

        /// <summary>
        /// Gets the histogram values for the gray component.
        /// </summary>
        public int[] Gray
        {
            get { return this.gray; }
        }

        /// <summary>
        /// Gets the index of the minimum gray value;
        /// </summary>
        /// <returns>The index of the minimum value</returns>
        public byte GetGrayMin()
        {
            for (int i = 0; i < this.Gray.Length; i++)
            {
                if (this.Gray[i] > 0)
                {
                    return (byte)Math.Max(0, i - 1);
                }
            }

            return 255;
        }

        /// <summary>
        /// Gets the index of the maximum gray value;
        /// </summary>
        /// <returns>The index of the maximum value</returns>
        public byte GetGrayMax()
        {
            for (int i = this.Gray.Length - 1; i >= 0; i--)
            {
                if (this.Gray[i] > 0)
                {
                    return (byte)Math.Min(255, i + 1);
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Histogram Calculator";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            HistogramCalculatorExecute(
                this.GetCppData(this.Image),
                this.red,
                this.green,
                this.blue,
                this.gray);
        }

        /// <summary>
        /// The native histogram calculator processing function.
        /// </summary>
        /// <param name="source">source image.</param>
        /// <param name="red">red histogram.</param>
        /// <param name="green">green histogram.</param>
        /// <param name="blue">blue histogram.</param>
        /// <param name="gray">gray histogram.</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void HistogramCalculatorExecute(ImageData source, int[] red, int[] green, int[] blue, int[] gray);
    }
}
