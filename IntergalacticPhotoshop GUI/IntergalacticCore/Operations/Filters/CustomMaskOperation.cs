namespace IntergalacticCore.Operations.Filters.Sharpening
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Applies a custom mask to the image.
    /// </summary>
    public class CustomMaskOperation : ConvolutionBase
    {
        /// <summary>
        /// Holds the mask data to be applied to the image.
        /// </summary>
        private ConvolutionMask mask;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.mask = (ConvolutionMask)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Mask,mask";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Custom Mask";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            this.Masks.Add(this.mask);

            this.ResultImage = this.Image.CreateEmptyClone(this.Image.Width, this.Image.Height);
            this.ResultImage.BeforeEdit();
        }
    }
}
