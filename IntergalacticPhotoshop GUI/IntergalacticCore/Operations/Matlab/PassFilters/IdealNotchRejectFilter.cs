namespace IntergalacticCore.Operations.Matlab.PassFilters
{
    using System;

    /// <summary>
    /// Performs an ideal notch reject filter with a specified notch radius and distance.
    /// </summary>
    public class IdealNotchRejectFilter : IdealNotchFilterOperation
    {
        /// <summary>
        /// Frequency of filter.
        /// </summary>
        private double radius;

        /// <summary>
        /// Notch horizontal position.
        /// </summary>
        private double notchXPos;

        /// <summary>
        /// Notch vertical position.
        /// </summary>
        private double notchYPos;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.radius = (double)input[0];
            this.notchXPos = (double)input[1];
            this.notchYPos = (double)input[2];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Radius,double,0,200|Notch X Pos,double,0,200|Notch Y Pos,double,0,200";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Ideal Notch Reject Filter";
        }

        /// <summary>
        /// Gets the pass value for this filter.
        /// </summary>
        /// <param name="dnode">node distance.</param>
        /// <param name="x">x position.</param>
        /// <param name="y">y position.</param>
        /// <returns>the pass value.</returns>
        protected override double GetPassValue(double dnode, int x, int y)
        {
            double part1 = x - this.notchXPos - (this.Image.Width / 2);
            double part2 = y - this.notchYPos - (this.Image.Height / 2);
            double d1 = Math.Sqrt((part1 * part1) + (part2 * part2));

            part1 = x + this.notchXPos - (this.Image.Width / 2);
            part2 = y + this.notchYPos - (this.Image.Height / 2);
            double d2 = Math.Sqrt((part1 * part1) + (part2 * part2));

            if (d1 > dnode && d2 > dnode)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
