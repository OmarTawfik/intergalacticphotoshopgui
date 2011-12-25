namespace IntergalacticCore.Operations.Matlab.Retinex
{
    using System;
    using IntergalacticCore.Operations.Matlab;
    using IntergalacticMatlab;
    using MathWorks.MATLAB.NET.Arrays;

    /// <summary>
    /// Multi-Scale Retinex operation.
    /// </summary>
    public class MultiScaleRetinexOperation : MatlabOperation
    {
        /// <summary>
        /// Matlab Class Handle.
        /// </summary>
        private Retinex matlabCls = new Retinex();

        /// <summary>
        /// Array of Retinex sigmas and weights
        /// </summary>
        private double[] sigmas, weights;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.sigmas = (double[])input[0];
            this.weights = (double[])input[1];

            if (this.sigmas.Length != this.weights.Length)
            {
                throw new Exception("Number of sigmas and weights must be equal.");
            }
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Sigmas,doubleArray,1,300|Weights,doubleArray,1,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Multi-Scale Retinex";
        }

        /// <summary>
        /// The main retinex operation
        /// </summary>
        protected override void BeforeOperate()
        {
            double[,] sourceRed, sourceGreen, sourceBlue;
            this.ImageToDoubles(this.Image, out sourceRed, out sourceGreen, out sourceBlue);

            MWArray[] retinexResult = this.matlabCls.MultiScaleRetinex(
                3,
                (MWNumericArray)sourceRed,
                (MWNumericArray)sourceGreen,
                (MWNumericArray)sourceBlue,
                (MWNumericArray)this.sigmas,
                (MWNumericArray)this.weights);

            double[,] redComponent = (double[,])retinexResult[0].ToArray();
            double[,] greenComponent = (double[,])retinexResult[1].ToArray();
            double[,] blueComponent = (double[,])retinexResult[2].ToArray();

            this.DoublesToImage(this.Image, redComponent, greenComponent, blueComponent);
        }
    } 
}
