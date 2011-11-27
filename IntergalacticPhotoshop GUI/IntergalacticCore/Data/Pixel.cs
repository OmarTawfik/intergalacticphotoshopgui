namespace IntergalacticCore.Data
{
    /// <summary>
    /// Provides a base for all pixel based operations.
    /// </summary>
    public struct Pixel
    {
        /// <summary>
        /// The blue component of the pixel.
        /// </summary>
        private byte blue;

        /// <summary>
        /// The green component of the pixel.
        /// </summary>
        private byte green;

        /// <summary>
        /// The red component of the pixel.
        /// </summary>
        private byte red;

        /// <summary>
        /// Initializes a new instance of the Pixel struct.
        /// </summary>
        /// <param name="red">The red component of the pixel.</param>
        /// <param name="green">The green component of the pixel.</param>
        /// <param name="blue">The blue component of the pixel.</param>
        public Pixel(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        /// <summary>
        /// Gets a black pixel with all components set to 0.
        /// </summary>
        public static Pixel Black
        {
            get { return new Pixel(0, 0, 0); }
        }

        /// <summary>
        /// Gets a white pixel with all components set to 1.
        /// </summary>
        public static Pixel White
        {
            get { return new Pixel(255, 255, 255); }
        }

        /// <summary>
        /// Gets or sets the red component of the pixel.
        /// </summary>
        public byte Red
        {
            get { return this.red; }
            set { this.red = value; }
        }

        /// <summary>
        /// Gets or sets the green component of the pixel.
        /// </summary>
        public byte Green
        {
            get { return this.green; }
            set { this.green = value; }
        }

        /// <summary>
        /// Gets or sets the blue component of the pixel.
        /// </summary>
        public byte Blue
        {
            get { return this.blue; }
            set { this.blue = value; }
        }

        /// <summary>
        /// Multiplies the contents of this pixel by a floating value.
        /// </summary>
        /// <param name="p">The pixel to be multiplied.</param>
        /// <param name="value">The value to be multiplied with.</param>
        /// <returns>The resulting pixel.</returns>
        public static Pixel operator *(Pixel p, float value)
        {
            return new Pixel(
                (byte)(p.red * value),
                (byte)(p.green * value),
                (byte)(p.blue * value));
        }

        /// <summary>
        /// Adds the contents of two pixels.
        /// </summary>
        /// <param name="p1">The first pixel.</param>
        /// <param name="p2">The second pixel.</param>
        /// <returns>The resulting pixel.</returns>
        public static Pixel operator +(Pixel p1, Pixel p2)
        {
            return new Pixel(
                (byte)(p1.red + p2.red),
                (byte)(p1.green + p2.green),
                (byte)(p1.blue + p2.blue));
        }

        /// <summary>
        /// Returns a new pixel with values clampped between 0 and 255.
        /// </summary>
        /// <param name="red">Red component of pixel.</param>
        /// <param name="green">Green component of pixel.</param>
        /// <param name="blue">Blue component of pixel.</param>
        /// <returns>The new clampped pixel.</returns>
        public static Pixel CutOff(int red, int green, int blue)
        {
            if (red < 0)
            {
                red = 0;
            }
            else if (red > 255)
            {
                red = 255;
            }

            if (green < 0)
            {
                green = 0;
            }
            else if (green > 255)
            {
                green = 255;
            }

            if (blue < 0)
            {
                blue = 0;
            }
            else if (blue > 255)
            {
                blue = 255;
            }

            return new Pixel((byte)red, (byte)green, (byte)blue);
        }
    }
}
