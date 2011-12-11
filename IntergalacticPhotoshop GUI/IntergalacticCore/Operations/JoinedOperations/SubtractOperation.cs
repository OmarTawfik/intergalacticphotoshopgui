namespace IntergalacticCore.Operations.PixelOperations
{
    using System;
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;
    using IntergalacticCore.Operations.PixelOperations;
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
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p1 = this.Image.GetPixel(j, i);
                    Pixel p2 = this.otherImage.GetPixel(j, i);
                    p1.Red = (byte)Math.Abs(p1.Red - p2.Red);
                    p1.Green = (byte)Math.Abs(p1.Green - p2.Green);
                    p1.Blue = (byte)Math.Abs(p1.Blue - p2.Blue);
                    
                    this.Image.SetPixel(j, i, p1);
                }
            }
        }
    }
}
