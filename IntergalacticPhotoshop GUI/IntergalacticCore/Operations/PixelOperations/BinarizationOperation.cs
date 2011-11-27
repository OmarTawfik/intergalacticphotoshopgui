namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Converts all pixels of the image to a binary value (white or black).
    /// </summary>
    public class BinarizationOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Binarization";
        }

        /// <summary>
        /// Converts all pixels of the image to a binary value (white or black).
        /// </summary>
        protected override void Operate()
        {
            int totalGray = 0;
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);
                    totalGray += (p.Red + p.Green + p.Blue) / 3;
                }
            }

            int threshold = totalGray / (this.Image.Width * this.Image.Height);
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    Pixel p = this.Image.GetPixel(j, i);
                    int gray = (p.Red + p.Green + p.Blue) / 3;
                    
                    if (gray < threshold)
                    {
                        this.Image.SetPixel(j, i, Pixel.Black);
                    }
                    else if (gray >= threshold)
                    {
                        this.Image.SetPixel(j, i, Pixel.White);
                    }
                }
            }
        }
    }
}
