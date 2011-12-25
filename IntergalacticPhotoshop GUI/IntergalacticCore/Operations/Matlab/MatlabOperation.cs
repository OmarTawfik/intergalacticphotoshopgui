namespace IntergalacticCore.Operations.Matlab
{
    using System.Runtime.InteropServices;
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

            unsafe
            {
                fixed (double* pred = &red[0, 0], pgreen = &green[0, 0], pblue = &blue[0, 0])
                {
                    DoubleToImageExecute(this.GetCppData(image), pred, pgreen, pblue);
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

            unsafe
            {
                fixed (double* pred = &mred[0, 0], pgreen = &mgreen[0, 0], pblue = &mblue[0, 0])
                {
                    ImageToDoubleExecute(this.GetCppData(image), pred, pgreen, pblue);
                }
            }

            red = mred;
            green = mgreen;
            blue = mblue;

            image.AfterEdit();
        }

        /// <summary>
        /// Converts three double arrays back into the image.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="red">red component.</param>
        /// <param name="green">green component.</param>
        /// <param name="blue">blue component.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void DoubleToImageExecute(ImageData src, double* red, double* green, double* blue);

        /// <summary>
        /// Converts the image to three double arrays.
        /// </summary>
        /// <param name="src">source image.</param>
        /// <param name="red">red component.</param>
        /// <param name="green">green component.</param>
        /// <param name="blue">blue component.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static unsafe extern void ImageToDoubleExecute(ImageData src, double* red, double* green, double* blue);
    }
}
