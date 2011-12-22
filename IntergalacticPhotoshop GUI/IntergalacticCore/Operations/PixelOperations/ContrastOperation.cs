namespace IntergalacticCore.Operations.PixelOperations
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Adjusts the contrast of the image
    /// </summary>
    public class ContrastOperation : BaseOperation
    {
        /// <summary>
        /// Min/Max histogram values
        /// </summary>
        private int oldMin, oldMax, newMin, newMax;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used (oldMin/Max and newMin/Max).</param>
        public override void SetInput(params object[] input)
        {
            this.oldMin = (int)input[0];
            this.oldMax = (int)input[1];
            this.newMin = (int)input[2];
            this.newMax = (int)input[3];

            if (this.oldMin >= this.oldMax)
            {
                throw new Exception("Old min value cannot be larger than old max.");
            }

            if (this.newMin >= this.newMax)
            {
                throw new Exception("New min value cannot be larger than new max.");
            }
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Old Min,int,0,255|Old Max,int,0,255|New Min,int,0,255|New Max,int,0,255";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Contrast";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            ContrastOperationExecute(
                this.GetCppData(this.Image),
                this.oldMin,
                this.oldMax,
                this.newMin,
                this.newMax);
        }

        /// <summary>
        /// The native cotrast processing function.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="oldMin">old min.</param>
        /// <param name="oldMax">old max.</param>
        /// <param name="newMin">new min.</param>
        /// <param name="newMax">new max.</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void ContrastOperationExecute(ImageData src, int oldMin, int oldMax, int newMin, int newMax);
    }
}
