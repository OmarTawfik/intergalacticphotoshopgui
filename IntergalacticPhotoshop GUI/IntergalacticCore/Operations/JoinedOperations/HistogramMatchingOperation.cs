namespace IntergalacticCore.Operations.JoinedOperations
{
    using System;
    using System.Runtime.InteropServices;
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
        private ImageBase otherImage;

        /// <summary>
        /// Array of values used to match histograms
        /// </summary>
        private int[] gray1, gray2;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (the image to be matched).</param>
        public override void SetInput(params object[] input)
        {
            this.otherImage = (ImageBase)input[0];
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
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            HistogramCalculator operation1 = new HistogramCalculator();
            HistogramCalculator operation2 = new HistogramCalculator();

            operation1.Execute(this.Image);
            operation2.Execute(this.otherImage);

            this.gray1 = operation1.Gray;
            this.gray2 = operation2.Gray;
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            HistogramMatchingOperationExecute(
                this.GetCppData(this.Image),
                this.gray1,
                this.gray2,
                this.Image.Height * this.Image.Width,
                this.otherImage.Height * this.otherImage.Width);
        }

        /// <summary>
        /// The native histogram matching processing function.
        /// </summary>
        /// <param name="source">Soruce image.</param>
        /// <param name="imageGray">Gray histogram of image.</param>
        /// <param name="otherGray">Gray histogram of other image.</param>
        /// <param name="imageSum">Pixel count of image.</param>
        /// <param name="otherSum">Pixel count of other image.</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void HistogramMatchingOperationExecute(ImageData source, int[] imageGray, int[] otherGray, int imageSum, int otherSum);
    }
}
