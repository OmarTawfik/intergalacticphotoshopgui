namespace IntergalacticCore
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Provides a base for all copy based operations.
    /// </summary>
    public class CopyOperation : BaseOperation
    {
        /// <summary>
        /// The resulting image.
        /// </summary>
        private ImageBase resultImage;

        /// <summary>
        /// Gets or sets the resulting image.
        /// </summary>
        protected ImageBase ResultImage
        {
            get { return this.resultImage; }
            set { this.resultImage = value; }
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected override void BeforeOperate()
        {
            this.resultImage = this.Image.CreateEmptyClone(this.Image.Width, this.Image.Height);
            this.resultImage.BeforeEdit();
        }

        /// <summary>
        /// Gets called after the operation ends.
        /// </summary>
        protected override void AfterOperate()
        {
            this.resultImage.AfterEdit();
            this.Image = this.resultImage;
        }
    }
}
