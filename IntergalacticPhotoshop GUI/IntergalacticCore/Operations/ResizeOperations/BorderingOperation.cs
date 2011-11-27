namespace IntergalacticCore.Operations.ResizeOperations
{
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;

    /// <summary>
    /// Adds a colored border with a specific width to the image.
    /// </summary>
    public class BorderingOperation : CopyOperation
    {
        /// <summary>
        /// Width of the border.
        /// </summary>
        private int borderWidth;

        /// <summary>
        /// Color of the border.
        /// </summary>
        private Pixel borderColor;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.borderWidth = (int)input[0];
            this.borderColor = (Pixel)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "Width,int,1,1000|Color,color";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Bordering";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            this.ResultImage = this.Image.CreateEmptyClone(
                this.Image.Width + (this.borderWidth * 2),
                this.Image.Height + (this.borderWidth * 2));
            this.ResultImage.BeforeEdit();
        }

        /// <summary>
        /// Adds a colored border with a specific width to the image.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.ResultImage.Height; i++)
            {
                for (int j = 0; j < this.ResultImage.Width; j++)
                {
                    if (i < this.borderWidth || i >= this.ResultImage.Height - this.borderWidth ||
                        j < this.borderWidth || j >= this.ResultImage.Width - this.borderWidth)
                    {
                        this.ResultImage.SetPixel(j, i, this.borderColor);
                    }
                    else
                    {
                        Pixel oldPixel = this.Image.GetPixel(j - this.borderWidth, i - this.borderWidth);
                        this.ResultImage.SetPixel(j, i, oldPixel);
                    }
                }
            }
        }
    }
}
