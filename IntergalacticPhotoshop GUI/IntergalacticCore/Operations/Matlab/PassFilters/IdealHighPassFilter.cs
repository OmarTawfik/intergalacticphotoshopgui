namespace IntergalacticCore.Operations.Matlab.PassFilters
{
    /// <summary>
    /// Performs an ideal high pass filter with a cut-off frequency.
    /// </summary>
    public class IdealHighPassFilter : PassFilterOperation
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
            return "Ideal High Pass Filter";
        }

        /// <summary>
        /// Gets the pass value for this filter.
        /// </summary>
        /// <param name="d">D(x, y).</param>
        /// <returns>The pass value.</returns>
        protected override double GetPassValue(double d)
        {
            return (d <= this.frequency) ? 0 : 1;
        }
    }
}
