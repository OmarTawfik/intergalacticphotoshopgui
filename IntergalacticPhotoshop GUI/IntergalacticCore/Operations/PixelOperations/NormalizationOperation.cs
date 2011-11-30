namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Operations.HistogramOperations;

    /// <summary>
    /// Normalizes the contrast of the image.
    /// </summary>
    public class NormalizationOperation : BaseOperation
    {
        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected virtual void BeforeOperate()
        {
            HistogramCalculator histogram = new HistogramCalculator();
            histogram.Execute(this.Image);

            ContrastOperation contrast = new ContrastOperation();
            contrast.SetInput((int)histogram.GetGrayMin(), (int)histogram.GetGrayMax(), 0, 255);
            this.Image = contrast.Execute(this.Image);
        }
    }
}
