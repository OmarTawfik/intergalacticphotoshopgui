namespace IntergalacticCore.Operations.Filters.EdgeDetection
{
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Detects vertical edges using laplacian filter.
    /// </summary>
    public class VerticalEdgeDetectionOperation : ConvolutionBase
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Vertical Edge Detection";
        }

        /// <summary>
        /// Detects vertical edges using laplacian filter.
        /// </summary>
        protected override void Operate()
        {
            for (int i = 0; i < this.Image.Height; i++)
            {
                for (int j = 0; j < this.Image.Width; j++)
                {
                    int red = 0, green = 0, blue = 0;

                    Pixel leftUp = this.GetLocation(j - 1, i - 1),
                          up = this.GetLocation(j, i - 1),
                          rightUp = this.GetLocation(j + 1, i - 1),
                          left = this.GetLocation(j - 1, i),
                          right = this.GetLocation(j + 1, i),
                          downLeft = this.GetLocation(j - 1, i + 1),
                          down = this.GetLocation(j, i + 1),
                          downRight = this.GetLocation(j + 1, i + 1);

                    red += (leftUp.Red * 5) + (left.Red * 5) + (downLeft.Red * 5);
                    green += (leftUp.Green * 5) + (left.Green * 5) + (downLeft.Green * 5);
                    blue += (leftUp.Blue * 5) + (left.Blue * 5) + (downLeft.Blue * 5);

                    red += -(up.Red * 3) - (rightUp.Red * 3) - (right.Red * 3) - (down.Red * 3) - (downRight.Red * 3);
                    green += -(up.Green * 3) - (rightUp.Green * 3) - (right.Green * 3) - (down.Green * 3) - (downRight.Green * 3);
                    blue += -(up.Blue * 3) - (rightUp.Blue * 3) - (right.Blue * 3) - (down.Blue * 3) - (downRight.Blue * 3);

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }

        /// <summary>
        /// Gets called after the operation ends.
        /// </summary>
        protected override void AfterOperate()
        {
            base.AfterOperate();

            HistogramCalculator histogram = new HistogramCalculator();
            histogram.Execute(this.Image);

            ContrastOperation contrast = new ContrastOperation();
            contrast.SetInput((int)histogram.GetGrayMin(), (int)histogram.GetGrayMax(), 0, 255);
            this.Image = contrast.Execute(this.Image);
        }
    }
}
