namespace IntergalacticCore.Operations.PixelOperations
{
    using System;
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.ResizeOperations;

    /// <summary>
    /// Subtracts the contents of an image from another.
    /// </summary>
    public class SubtractOperation : BaseOperation
    {
        /// <summary>
        /// The image to add.
        /// </summary>
        private ImageBase otherImage;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.otherImage = (ImageBase)input[0];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Other Image,image";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Subtract";
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

            NormalizationOperation operation = new NormalizationOperation();
            this.Image = operation.Execute(this.Image);
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            SubtractOperationExecute(
                this.GetCppData(this.Image),
                this.GetCppData(this.otherImage));
        }

        /// <summary>
        /// The native subtract processing function.
        /// </summary>
        /// <param name="src">Source image.</param>
        /// <param name="other">Other image.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SubtractOperationExecute(ImageData src, ImageData other);
    }
}
