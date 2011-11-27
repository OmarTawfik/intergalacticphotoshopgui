namespace IntergalacticCore.Data
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Imports other bitmap formats easily.
    /// </summary>
    public class Importer
    {
        /// <summary>
        /// Loads a PPM image with the PPM format.
        /// </summary>
        /// <param name="filePath">Location on disk where image is stored.</param>
        /// <param name="image">the image to be loaded.</param>
        public static void LoadPPM(string filePath, ImageBase image)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);

            string versionString = streamReader.ReadLine();
            if (versionString != "P3")
            {
                streamReader.Close();
                fileStream.Close();
                throw new Exception("Only P3 Images are supported!");
            }

            string sizeLine;
            do
            {
                sizeLine = streamReader.ReadLine();
            }
            while (sizeLine[0] == '#');

            string[] dimentions = sizeLine.Split(' ');
            int width = Convert.ToInt32(dimentions[0]);
            int height = Convert.ToInt32(dimentions[1]);

            if (streamReader.ReadLine() != "255")
            {
                streamReader.Close();
                fileStream.Close();
                throw new Exception("Only base 255 is supported!");
            }

            image.SetSize(width, height);
            image.BeforeEdit();

            if (versionString == "P3")
            {
                LoadP3(streamReader, image);
            }

            image.AfterEdit();
            streamReader.Close();
            fileStream.Close();
        }

        /// <summary>
        /// Loads a convulution mask from a file.
        /// </summary>
        /// <param name="filePath">File path to be read.</param>
        /// <returns>The newly constructed mask.</returns>
        public static ConvolutionMask LoadMask(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);

            int width = Convert.ToInt32(streamReader.ReadLine());
            int height = Convert.ToInt32(streamReader.ReadLine());
            ConvolutionMask newMask = new ConvolutionMask(width, height);

            char[] splitters = { ' ', '\n', '\r', '\t' };
            string restOfTheFile = streamReader.ReadToEnd();
            string[] data = restOfTheFile.Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            int index = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    newMask.Data[i, j] = Convert.ToDouble(data[index]);
                    index++;
                }
            }

            streamReader.Close();
            fileStream.Close();
            return newMask;
        }

        /// <summary>
        /// Loads a PPM image with the P3 specification.
        /// </summary>
        /// <param name="streamReader">Stream reader to read data from.</param>
        /// <param name="image">The image to be loaded.</param>
        private static void LoadP3(StreamReader streamReader, ImageBase image)
        {
            char[] splitters = { ' ', '\n', '\r', '\t' };
            string restOfTheFile = streamReader.ReadToEnd();
            string[] data = restOfTheFile.Split(splitters, StringSplitOptions.RemoveEmptyEntries);

            int index = 0;
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Pixel newPixel = new Pixel(
                        Convert.ToByte(data[index]),
                        Convert.ToByte(data[index + 1]),
                        Convert.ToByte(data[index + 2]));
                    image.SetPixel(j, i, newPixel);
                    index += 3;
                }
            }
        }
    }
}
