namespace IntergalacticCore.Operations.Matlab
{
    using MathWorks.MATLAB.NET.Arrays;

    /// <summary>
    /// Local Histogram Equalization operation 
    /// </summary>
    public class LocalHistogramEqualizationOperation : MatlabOperation
    {
        /// <summary>
        /// Matlab Class Handle.
        /// </summary>
        private IntergalacticMatlab.Retinex matlabCls = new IntergalacticMatlab.Retinex();

        /// <summary>
        /// Window size.
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
            return "Window Size,int,10,300";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Local Histogram Equalization";
        }

        /// <summary>
        /// The main retinex operation
        /// </summary>
        protected override void BeforeOperate()
        {
            double[,] sourceRed, sourceGreen, sourceBlue;
            this.ImageToDoubles(this.Image, out sourceRed, out sourceGreen, out sourceBlue);

            MWArray[] retinexResult = this.matlabCls.LocalHistogramEqualization(
                3,
                (MWNumericArray)sourceRed,
                (MWNumericArray)sourceGreen,
                (MWNumericArray)sourceBlue,
                this.maskSize);

            double[,] redComponent = (double[,])retinexResult[0].ToArray();
            double[,] greenComponent = (double[,])retinexResult[1].ToArray();
            double[,] blueComponent = (double[,])retinexResult[2].ToArray();

            this.DoublesToImage(this.Image, redComponent, greenComponent, blueComponent);
        }
    } 
}
