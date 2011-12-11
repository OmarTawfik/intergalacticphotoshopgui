﻿namespace IntergalacticCore.Operations.Filters.EdgeDetection
{
    using IntergalacticCore.Data;
    using IntergalacticCore.Operations.HistogramOperations;
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Detects horizontal edges using laplacian filter.
    /// </summary>
    public class HorizontalEdgeDetectionOperation : BaseEdgeDetectionOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Horizontal Edge Detection";
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
                    int red = 0, green = 0, blue = 0;

                    Pixel leftUp = this.GetLocation(j - 1, i - 1),
                          up = this.GetLocation(j, i - 1),
                          rightUp = this.GetLocation(j + 1, i - 1),
                          left = this.GetLocation(j - 1, i),
                          right = this.GetLocation(j + 1, i),
                          downLeft = this.GetLocation(j - 1, i + 1),
                          down = this.GetLocation(j, i + 1),
                          downRight = this.GetLocation(j + 1, i + 1);

                    red += (leftUp.Red * 5) + (up.Red * 5) + (rightUp.Red * 5);
                    green += (leftUp.Green * 5) + (up.Green * 5) + (rightUp.Green * 5);
                    blue += (leftUp.Blue * 5) + (up.Blue * 5) + (rightUp.Blue * 5);

                    red += -(left.Red * 3) - (right.Red * 3) - (downLeft.Red * 3) - (down.Red * 3) - (downRight.Red * 3);
                    green += -(left.Green * 3) - (right.Green * 3) - (downLeft.Green * 3) - (down.Green * 3) - (downRight.Green * 3);
                    blue += -(left.Blue * 3) - (right.Blue * 3) - (downLeft.Blue * 3) - (down.Blue * 3) - (downRight.Blue * 3);

                    this.ResultImage.SetPixel(j, i, Pixel.CutOff(red, green, blue));
                }
            }
        }
    }
}
