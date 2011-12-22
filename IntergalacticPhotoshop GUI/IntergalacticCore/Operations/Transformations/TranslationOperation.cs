namespace IntergalacticCore.Operations.Transformations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Translates the image given X and Y displacements.
    /// </summary>
    public class TranslationOperation : CopyOperation
    {
        /// <summary>
        /// X displacement of image.
        /// </summary>
        private int displacementX;

        /// <summary>
        /// Y displacement of image.
        /// </summary>
        private int displacementY;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.displacementX = (int)input[0];
            this.displacementY = (int)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "X Displacement,int,-1000,1000|Y Displacement,int,-1000,1000";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Translation";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            TranslationOperationExecute(this.GetCppData(this.Image), this.GetCppData(this.ResultImage), this.displacementX, this.displacementY);
        }

        /// <summary>
        /// The native translation processing function
        /// </summary>
        /// <param name="src">Source image data</param>
        /// <param name="dest">Destination image data</param>
        /// <param name="displacementX">X displacement</param>
        /// <param name="displacementY">Y displacement</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void TranslationOperationExecute(ImageData src, ImageData dest, int displacementX, int displacementY);
    }
}
