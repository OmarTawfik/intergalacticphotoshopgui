namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.ResizeOperations;

    /// <summary>
    /// Adds the contents of two images together.
    /// </summary>
    public class AddOperation : BaseOperation
    {
        /// <summary>
        /// The image to add.
        /// </summary>
        private ImageBase otherImage;

        /// <summary>
        /// The addition factor.
        /// </summary>
        private float factor;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.otherImage = (ImageBase)input[0];
            this.factor = (float)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Other Image,image|Factor,float,0,1";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Add";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            BilinearResizeOperation resize = new BilinearResizeOperation();
            resize.SetInput(this.Image.Width, this.Image.Height);
            this.otherImage = resize.Execute(this.otherImage);

            this.otherImage.BeforeEdit();
        }

        /// <summary>
        /// Gets called after the operation ends.
        /// </summary>
        protected override void AfterOperate()
        {
            this.otherImage.AfterEdit();
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            AddOperationExecute(
                this.GetCppData(this.Image),
                this.GetCppData(this.otherImage),
                this.factor);
        }

        /// <summary>
        /// The native add processing function.
        /// </summary>
        /// <param name="src">Source image.</param>
        /// <param name="other">Other image.</param>
        /// <param name="factor">Addition factor.</param>
        [DllImport("IntergalacticNative.dll")]
        private static extern void AddOperationExecute(ImageData src, ImageData other, double factor);
    }
}
