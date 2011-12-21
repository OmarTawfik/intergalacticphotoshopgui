namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;
    using IntergalacticCpp.PixelOperations;

    /// <summary>
    /// Converts all pixels of the image to a binary value (white or black).
    /// </summary>
    public class BinarizationOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Binarization";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected override void Operate()
        {
            CLRBinarizationOperation.Execute(this.GetCppData(this.Image));
        }
    }
}
