namespace IntergalacticCore.Operations.Filters
{
    using System.Collections.Generic;
    using IntergalacticCore.Data;

    /// <summary>
    /// Acts as a base to all convulution operations.
    /// </summary>
    public abstract class ConvolutionBase : CopyOperation
    {
        /// <summary>
        /// List of masks to be applied to the image.
        /// </summary>
        private List<ConvolutionMask> masks = new List<ConvolutionMask>();

        /// <summary>
        /// Gets the list of masks to be applied to the image.
        /// </summary>
        protected List<ConvolutionMask> Masks
        {
            get { return this.masks; }
        }
        
        /// <summary>
        /// Does noise reduction using 2D mean filtering.
        /// </summary>
        protected override void Operate()
        {
            for (int m = 0; m < this.masks.Count; m++)
            {
                ConvolutionMask mask = this.masks[m];
                for (int i = 0; i < this.Image.Height; i++)
                {
                    for (int j = 0; j < this.Image.Width; j++)
                    {
                        double red = 0, green = 0, blue = 0;
                        for (int y = i - (mask.Height / 2), a = 0; y <= i + (mask.Height / 2); y++, a++)
                        {
                            for (int x = j - (mask.Width / 2), b = 0; x <= j + (mask.Width / 2); x++, b++)
                            {
                                if (mask.Data[a, b] != 0)
                                {
                                    Pixel p = this.GetLocation(x, y);
                                    red += mask.Data[a, b] * p.Red;
                                    green += mask.Data[a, b] * p.Green;
                                    blue += mask.Data[a, b] * p.Blue;
                                }
                            }
                        }

                        this.ResultImage.SetPixel(j, i, Pixel.CutOff((int)red, (int)green, (int)blue));
                    }
                }

                if (m + 1 < this.masks.Count)
                {
                    this.Image.AfterEdit();
                    this.ResultImage.AfterEdit();
                    this.Image = ResultImage.CreateCopyClone();
                    this.Image.BeforeEdit();
                    this.ResultImage.BeforeEdit();
                }
            }
        }

        /// <summary>
        /// Gets a pixel at a specified location or repeats if out of bounds.
        /// </summary>
        /// <param name="x">Horizontal location of pixel.</param>
        /// <param name="y">Vertical location of pixel.</param>
        /// <returns>Found pixel.</returns>
        protected Pixel GetLocation(int x, int y)
        {
            if (x < 0)
            {
                x = 0;
            }

            if (x >= this.Image.Width)
            {
                x = this.Image.Width - 1;
            }

            if (y < 0)
            {
                y = 0;
            }

            if (y >= this.Image.Height)
            {
                y = this.Image.Height - 1;
            }

            return this.Image.GetPixel(x, y);
        }
    }
}
