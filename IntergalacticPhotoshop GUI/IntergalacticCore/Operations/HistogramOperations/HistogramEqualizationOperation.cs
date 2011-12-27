namespace IntergalacticCore.Operations.HistogramOperations
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Equalizes the histogram by the gray component.
    /// </summary>
    public class HistogramEqualizationOperation : BaseOperation
    {
        /// <summary>
        /// The histogram values for the RGB components.
        /// </summary>
        private int[] red = new int[256], green = new int[256], blue = new int[256];

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
            this.red = calculator.Red;
            this.green = calculator.Green;
            this.blue = calculator.Blue;
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            HistogramEqualizationOperationExecute(
                this.GetCppData(this.Image),
                this.red,
                this.green,
                this.blue);
        }

        /// <summary>
        /// The native histogram equalization processing function.
        /// </summary>
        /// <param name="source">source image.</param>
        /// <param name="red">Red histogram.</param>
        /// <param name="green">Green histogram.</param>
        /// <param name="blue">Blue histogram.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void HistogramEqualizationOperationExecute(ImageData source, int[] red, int[] green, int[] blue);
    }
}
