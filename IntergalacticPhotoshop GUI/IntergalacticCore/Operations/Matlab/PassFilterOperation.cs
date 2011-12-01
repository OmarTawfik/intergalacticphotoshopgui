namespace IntergalacticCore.Operations.Matlab
{
    using IntergalacticMatlab;
    using MathWorks.MATLAB.NET.Arrays;

    /// <summary>
    /// Acts as a base for all pass filters.
    /// </summary>
    public abstract class PassFilterOperation : MatlabOperation
    {
        /// <summary>
        /// Value of C.
        /// </summary>
        private double valueOfC;

        /// <summary>
        /// Value of N.
        /// </summary>
        private double valueofN;

        /// <summary>
        /// Gets the value of C.
        /// </summary>
        protected double C
        {
            get { return this.valueOfC; }
        }

        /// <summary>
        /// Gets the value of N.
        /// </summary>
        protected double N
        {
            get { return this.valueofN; }
        }

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.valueOfC = (double)input[0];
            this.valueofN = (double)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Value of C,double,0,1|Value of N,double,1,25";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            FrequencyDomain matlabCls = new FrequencyDomain();

            double[,] sourceRed, sourceGreen, sourceBlue;
            this.ImageToDoubles(this.Image, out sourceRed, out sourceGreen, out sourceBlue);

            MWArray[] frequencyComponents = matlabCls.ConvertToFrequencyDomain(
                6,
                (MWNumericArray)sourceRed,
                (MWNumericArray)sourceGreen,
                (MWNumericArray)sourceBlue);

            double[,] imagRed = (double[,])frequencyComponents[0].ToArray();
            double[,] realRed = (double[,])frequencyComponents[1].ToArray();
            double[,] imagGreen = (double[,])frequencyComponents[2].ToArray();
            double[,] realGreen = (double[,])frequencyComponents[3].ToArray();
            double[,] imagBlue = (double[,])frequencyComponents[4].ToArray();
            double[,] realBlue = (double[,])frequencyComponents[5].ToArray();

            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    double passValue = this.GetPassValue(j, i);
                    
                    imagRed[i, j] *= passValue;
                    realRed[i, j] *= passValue;
                    imagGreen[i, j] *= passValue;
                    realGreen[i, j] *= passValue;
                    imagBlue[i, j] *= passValue;
                    realGreen[i, j] *= passValue;
                    imagBlue[i, j] *= passValue;
                    realBlue[i, j] *= passValue;
                }
            }

            MWArray[] spatialComponents = matlabCls.ConvertToSpatialDomain(
                3,
                (MWNumericArray)imagRed,
                (MWNumericArray)realRed,
                (MWNumericArray)imagGreen,
                (MWNumericArray)realGreen,
                (MWNumericArray)imagBlue,
                (MWNumericArray)realBlue);

            double[,] redComponent = this.Normalize((double[,])spatialComponents[0].ToArray());
            double[,] greenComponent = this.Normalize((double[,])spatialComponents[1].ToArray());
            double[,] blueComponent = this.Normalize((double[,])spatialComponents[2].ToArray());

            this.DoublesToImage(this.Image, redComponent, greenComponent, blueComponent);
        }

        /// <summary>
        /// Gets the pass value for this filter.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <returns>The pass value.</returns>
        protected abstract double GetPassValue(int x, int y);
    }
}
