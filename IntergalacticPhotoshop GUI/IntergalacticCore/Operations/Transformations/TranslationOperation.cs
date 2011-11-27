namespace IntergalacticCore.Operations.Transformations
{
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
        /// Translates the image given X and Y displacements.
        /// </summary>
        protected override void Operate()
        {
            while (this.displacementX < this.Image.Width)
            {
                this.displacementX += this.Image.Width;
            }

            while (this.displacementY < this.Image.Height)
            {
                this.displacementY += this.Image.Height;
            }

            for (int i = 0; i < this.ResultImage.Height; i++)
            {
                for (int j = 0; j < this.ResultImage.Width; j++)
                {
                    int newX = (j + this.displacementX) % this.ResultImage.Width;
                    int newY = (i + this.displacementY) % this.ResultImage.Height;

                    Pixel oldPixel = this.Image.GetPixel(j, i);
                    this.ResultImage.SetPixel(newX, newY, oldPixel);
                }
            }
        }
    }
}
