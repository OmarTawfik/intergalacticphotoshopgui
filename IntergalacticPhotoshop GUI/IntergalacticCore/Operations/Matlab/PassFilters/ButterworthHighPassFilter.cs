namespace IntergalacticCore.Operations.Matlab.PassFilters
{
    using System;

    /// <summary>
    /// Performs a butterworth high pass filter with a cut-off frequency.
    /// </summary>
    public class ButterworthHighPassFilter : PassFilterOperation
    {
        /// <summary>
        /// Frequency of filter.
        /// </summary>
        private double frequency;

        /// <summary>
        /// Order of filter.
        /// </summary>
        private int order;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.frequency = (double)input[0];
            this.order = (int)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Frequency,double,0,100|Order,int,0,5";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Butterworth High Pass Filter";
        }

        /// <summary>
        /// Gets the pass value for this filter.
        /// </summary>
        /// <param name="d">D(x, y).</param>
        /// <returns>The pass value.</returns>
        protected override double GetPassValue(double d)
        {
            return 1 / (1 + Math.Pow(this.frequency / d, 2 * this.order));
        }
    }
}
