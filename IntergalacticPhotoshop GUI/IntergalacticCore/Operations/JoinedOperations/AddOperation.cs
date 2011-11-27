namespace IntergalacticCore.Operations.PixelOperations
{
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
        /// Adds the contents of two images together.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p1 = this.Image.GetPixel(j, i);
                    Pixel p2 = this.otherImage.GetPixel(j, i);

                    p1 = (p1 * this.factor) + (p2 * (1.0f - this.factor));
                    this.Image.SetPixel(j, i, p1);
                }
            }
        }
    }
}
