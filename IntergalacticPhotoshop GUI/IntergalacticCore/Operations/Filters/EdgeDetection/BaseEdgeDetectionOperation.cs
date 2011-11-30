namespace IntergalacticCore.Operations.Filters.EdgeDetection
{
    using IntergalacticCore.Operations.PixelOperations;

    /// <summary>
    /// Acts as a base for all edge detection operations.
    /// </summary>
    public abstract class BaseEdgeDetectionOperation : ConvolutionBase
    {
        /// <summary>
        /// Gets called after the operation ends.
        /// </summary>
        protected override void AfterOperate()
        {
            base.AfterOperate();
            NormalizationOperation operation = new NormalizationOperation();
            this.Image = operation.Execute(this.Image);
        }
    }
}
