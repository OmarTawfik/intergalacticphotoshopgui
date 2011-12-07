namespace IntergalacticCore.Operations.Matlab.PassFilters
{
    /// <summary>
    /// Performs a band reject filter with a cut-off frequency.
    /// </summary>
    public class IdealBandRejectFilter : PassFilterOperation
    {
        /// <summary>
        /// Starting frequency of filter.
        /// </summary>
        private double frequencyStart;

        /// <summary>
        /// Ending frequency of filter.
        /// </summary>
        private double frequencyEnd;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.frequencyStart = (double)input[0];
            this.frequencyEnd = (double)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Starting Frequency,double,0,100|Ending Frequency,double,0,100";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Ideal Band Reject Filter";
        }

        /// <summary>
        /// Gets the pass value for this filter.
        /// </summary>
        /// <param name="d">D(x, y).</param>
        /// <returns>The pass value.</returns>
        protected override double GetPassValue(double d)
        {
            return (this.frequencyStart <= d && d <= this.frequencyEnd) ? 0 : 1;
        }
    }
}
