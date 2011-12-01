namespace IntergalacticCore.Operations.Matlab.PassFilters
{
    /// <summary>
    /// Zonal Low Pass Filter Operation.
    /// </summary>
    public class ZonalLowPassFilter : PassFilterOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Zonal Low Pass Filter";
        }

        /// <summary>
        /// Gets the pass value for this filter.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <returns>The pass value.</returns>
        protected override double GetPassValue(int x, int y)
        {
            bool cond1 = 0 <= x && x <= this.C - 1 && 0 <= y && y <= this.C - 1;
            bool cond2 = 0 <= x && x <= this.C - 1 && this.N + 1 - this.C <= y && y <= this.N - 1;
            bool cond3 = this.N + 1 - this.C <= x && x <= this.N - 1 && 0 <= y && y <= this.C - 1;
            bool cond4 = this.N + 1 - this.C <= x && x <= this.N - 1 && this.N + 1 - this.C <= y && y <= this.N - 1;

            if (cond1 || cond2 || cond3 || cond4)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
