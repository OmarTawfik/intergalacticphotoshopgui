namespace IntergalacticCore.Operations.ResizeOperations
{
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.Filters;

    /// <summary>
    /// Performs a bilinear resize on the image contents.
    /// </summary>
    public class BilinearResizeOperation : CopyOperation
    {
        /// <summary>
        /// Width of the resulting image.
        /// </summary>
        private int newWidth;

        /// <summary>
        /// Height of the resulting image.
        /// </summary>
        private int newHeight;

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public override void SetInput(params object[] input)
        {
            this.newWidth = (int)input[0];
            this.newHeight = (int)input[1];
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public override string GetInput()
        {
            return "New Width,int,1,10000|New Height,int,1,10000";
        }

        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Bilinear Resize";
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            this.ResultImage = this.Image.CreateEmptyClone(this.newWidth, this.newHeight);
            this.ResultImage.BeforeEdit();
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.ResultImage.Height; i++)
            {
                for (int j = 0; j < this.ResultImage.Width; j++)
                {
                    float widthRatio = (float)this.Image.Width / (float)this.ResultImage.Width;
                    float heightRatio = (float)this.Image.Height / (float)this.ResultImage.Height;

                    float oldX = widthRatio * j;
                    float oldY = heightRatio * i;

                    int x1 = (int)oldX;
                    int x2 = (x1 + 1 < this.Image.Width) ? x1 + 1 : x1;
                    int y1 = (int)oldY;
                    int y2 = (y1 + 1 < this.Image.Height) ? y1 + 1 : y1;

                    Pixel p1 = this.Image.GetPixel(x1, y1);
                    Pixel p2 = this.Image.GetPixel(x2, y1);
                    Pixel p3 = this.Image.GetPixel(x1, y2);
                    Pixel p4 = this.Image.GetPixel(x2, y2);

                    float xfraction = oldX - x1;
                    float yfraction = oldY - y1;

                    Pixel z1 = (p1 * (1.0f - xfraction)) + (p2 * xfraction);
                    Pixel z2 = (p3 * (1.0f - xfraction)) + (p4 * xfraction);

                    Pixel newPixel = (z1 * (1 - yfraction)) + (z2 * yfraction);
                    this.ResultImage.SetPixel(j, i, newPixel);
                }
            }
        }
    }
}
