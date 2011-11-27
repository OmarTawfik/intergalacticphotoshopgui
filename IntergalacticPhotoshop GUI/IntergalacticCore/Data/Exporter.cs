namespace IntergalacticCore.Data
{
    using System.IO;

    /// <summary>
    /// Exports other bitmap formats easily.
    /// </summary>
    public class Exporter
    {
        /// <summary>
        /// Saves a PPM image with the P3 format.
        /// </summary>
        /// <param name="filePath">Location on disk where image is to be saved.</param>
        /// <param name="image">the image to be saved.</param>
        public static void SaveP3(string filePath, ImageBase image)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.WriteLine("P3");
            streamWriter.WriteLine(image.Width.ToString() + " " + image.Height.ToString());
            streamWriter.WriteLine("255");

            image.BeforeEdit();

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Pixel pixel = image.GetPixel(j, i);
                    string data =
                        pixel.Blue.ToString() + " " +
                        pixel.Green.ToString() + " " +
                        pixel.Red.ToString() + " ";
                    streamWriter.Write(data);
                }

                streamWriter.WriteLine();
            }

            image.AfterEdit();
            streamWriter.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Writes a mask to the specified file.
        /// </summary>
        /// <param name="filePath">File path for output.</param>
        /// <param name="mask">The mask to be written.</param>
        public static void SaveMask(string filePath, ConvolutionMask mask)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.WriteLine(mask.Width);
            streamWriter.WriteLine(mask.Height);

            for (int i = 0; i < mask.Height; i++)
            {
                for (int j = 0; j < mask.Width; j++)
                {
                    streamWriter.Write(mask.Data[i, j].ToString() + " ");
                }

                streamWriter.WriteLine();
            }

            streamWriter.Close();
            fileStream.Close();
        }
    }
}
