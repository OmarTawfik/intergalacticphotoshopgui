namespace IntergalacticCore.Data
{
    /// <summary>
    /// Provides a base for all image based operations.
    /// </summary>
    public abstract class ImageBase
    {
        /// <summary>
        /// Gets the height (in pixels) of this image.
        /// </summary>
        public abstract int Height { get; }

        /// <summary>
        /// Gets the width (in pixels) of this image.
        /// </summary>
        public abstract int Width { get; }
               
        /// <summary>
        /// This is called before editing the image to make any memory adjustments.
        /// </summary>
        public abstract void BeforeEdit();

        /// <summary>
        /// This is called after editing the image to make any memory adjustments.
        /// </summary>
        public abstract void AfterEdit();
            
        /// <summary>
        /// Returns the pixel at location x and y.
        /// </summary>
        /// <param name="x">The horizontal location of the pixel.</param>
        /// <param name="y">The vertical location of the pixel.</param>
        /// <returns>The pixel at location x and y.</returns>
        public abstract Pixel GetPixel(int x, int y);

        /// <summary>
        /// Sets the pixel at location x and y with value p.
        /// </summary>
        /// <param name="x">The horizontal location of the pixel.</param>
        /// <param name="y">The vertical location of the pixel.</param>
        /// <param name="p">The pixel to be set at location x and y.</param>
        public abstract void SetPixel(int x, int y, Pixel p);

        /// <summary>
        /// Sets the image to a new size.
        /// </summary>
        /// <param name="width">Width of the new image</param>
        /// <param name="height">Height of the new image.</param>
        public abstract void SetSize(int width, int height);

        /// <summary>
        /// Creates an empty clone with the same type.
        /// </summary>
        /// <param name="width">Width of the clone.</param>
        /// <param name="height">Height of the clone.</param>
        /// <returns>The resulting empty clone.</returns>
        public abstract ImageBase CreateEmptyClone(int width, int height);

        /// <summary>
        /// Creates a copy with the same pixels and size.
        /// </summary>
        /// <returns>The resulting copy.</returns>
        public abstract ImageBase CreateCopyClone();

        /// <summary>
        /// Saves the image (uncompressed) to a file path.
        /// </summary>
        /// <param name="filePath">Location of the image.</param>
        public abstract void SaveImage(string filePath);
        
        /// <summary>
        /// Loads the image (uncompressed) from a file path.
        /// </summary>
        /// <param name="filePath">Location of the image.</param>
        public abstract void LoadImage(string filePath);

        /// <summary>
        /// Returns a thumbnail of the image
        /// </summary>
        /// <returns>Thumbnail object</returns>
        public abstract ImageBase GetThumbnail();
    }
}
