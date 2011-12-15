namespace IntergalacticCore.Operations.Matlab
{
    using System;
    using IntergalacticMatlab;
    using MathWorks.MATLAB.NET.Arrays;

    /// <summary>
    /// Acts as a base for all pass filters.
    /// </summary>
    public abstract class PassFilterOperation : MatlabOperation
    {
        /// <summary>
        /// Matlab Class Handle.
        /// </summary>
        private FrequencyDomain matlabCls = new FrequencyDomain();

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            double[,] sourceRed, sourceGreen, sourceBlue;
            this.ImageToDoubles(this.Image, out sourceRed, out sourceGreen, out sourceBlue);

            MWArray[] frequencyComponents = this.matlabCls.ConvertToFrequencyDomain(
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
                    double x = Math.Pow(j - (this.Image.Width / 2), 2);
                    double y = Math.Pow(i - (this.Image.Height / 2), 2);

                    double d = Math.Sqrt(x + y);
                    double passValue = this.GetPassValue(d);
                    
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

            MWArray[] spatialComponents = this.matlabCls.ConvertToSpatialDomain(
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
        /// <param name="d">D(x, y).</param>
        /// <returns>The pass value.</returns>
        protected abstract double GetPassValue(double d);
    }
}
