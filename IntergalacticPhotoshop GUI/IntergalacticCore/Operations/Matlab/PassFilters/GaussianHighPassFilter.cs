namespace IntergalacticCore.Operations.Matlab.PassFilters
{
    using System;

    /// <summary>
    /// Performs a gaussian high pass filter with a cut-off frequency.
    /// </summary>
    public class GaussianHighPassFilter : PassFilterOperation
    {
        /// <summary>
        /// Frequency of filter.
        /// </summary>
        private double frequency;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.frequency = (double)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Frequency,double,0,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Gaussian High Pass Filter";
        }

        /// <summary>
        /// Gets the pass value for this filter.
        /// </summary>
        /// <param name="d">D(x, y).</param>
        /// <returns>The pass value.</returns>
        protected override double GetPassValue(double d)
        {
            double power = (d * d) / (2 * this.frequency * this.frequency);
            return 1 - Math.Pow(Math.E, -power);
        }
    }
}
