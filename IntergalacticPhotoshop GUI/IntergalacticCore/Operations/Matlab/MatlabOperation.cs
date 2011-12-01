namespace IntergalacticCore.Operations.Matlab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using IntergalacticCore.Data;

    /// <summary>
    /// Acts as a base for all Matlab operations.
    /// </summary>
    public class MatlabOperation : BaseOperation
    {
        /// <summary>
        /// Converts three components of color into an image.
        /// </summary>
        /// <param name="image">Image to be saved.</param>
        /// <param name="red">Red component.</param>
        /// <param name="green">Green component.</param>
        /// <param name="blue">Blue component.</param>
        protected void DoublesToImage(ImageBase image, double[,] red, double[,] green, double[,] blue)
        {
            image.SetSize(red.GetLength(1), red.GetLength(0));
            image.BeforeEdit();

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    image.SetPixel(j, i, new Pixel((byte)red[i, j], (byte)green[i, j], (byte)blue[i, j]));
                }
            }

            image.AfterEdit();
        }

        /// <summary>
        /// Converts an image to the three components of color.
        /// </summary>
        /// <param name="image">Image to be saved.</param>
        /// <param name="red">Red component.</param>
        /// <param name="green">Green component.</param>
        /// <param name="blue">Blue component.</param>
        protected void ImageToDoubles(ImageBase image, out double[,] red, out double[,] green, out double[,] blue)
        {
            double[,] mred = new double[image.Height, image.Width];
            double[,] mgreen = new double[image.Height, image.Width];
            double[,] mblue = new double[image.Height, image.Width];

            image.BeforeEdit();

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Pixel p = image.GetPixel(j, i);
                    mred[i, j] = p.Red;
                    mgreen[i, j] = p.Green;
                    mblue[i, j] = p.Blue;
                }
            }

            red = mred;
            green = mgreen;
            blue = mblue;

            image.AfterEdit();
        }

        /// <summary>
        /// Normalizes an array in the range of 0 and 255.
        /// </summary>
        /// <param name="ar">Input array.</param>
        /// <returns>Normalized array.</returns>
        protected double[,] Normalize(double[,] ar)
        {
            double min = double.MaxValue, max = double.MinValue;

            for (int i = 0; i < ar.GetLength(0); i++)
            {
                for (int j = 0; j < ar.GetLength(1); j++)
                {
                    min = Math.Min(min, ar[i, j]);
                    max = Math.Max(max, ar[i, j]);
                }
            }

            double ratio = (max - min) / 255.0;

            for (int i = 0; i < ar.GetLength(0); i++)
            {
                for (int j = 0; j < ar.GetLength(1); j++)
                {
                    ar[i, j] = (ar[i, j] - min) / ratio;
                }
            }

            return ar;
        }
    }
}
