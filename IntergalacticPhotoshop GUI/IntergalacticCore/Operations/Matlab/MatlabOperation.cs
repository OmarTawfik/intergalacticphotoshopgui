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
        protected void BytesToImage(ImageBase image, byte[,] red, byte[,] green, byte[,] blue)
        {
            image.SetSize(red.GetLength(1), red.GetLength(0));
            image.BeforeEdit();

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    image.SetPixel(j, i, new Pixel(red[i, j], green[i, j], blue[i, j]));
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
        protected void ImageToBytes(ImageBase image, out byte[,] red, out byte[,] green, out byte[,] blue)
        {
            byte[,] mred = new byte[image.Height, image.Width];
            byte[,] mgreen = new byte[image.Height, image.Width];
            byte[,] mblue = new byte[image.Height, image.Width];

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
    }
}
