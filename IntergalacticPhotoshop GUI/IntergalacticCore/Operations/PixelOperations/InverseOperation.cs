namespace IntergalacticCore.Operations.PixelOperations
{
    using System.Runtime.InteropServices;
    using IntergalacticCore.Data;

    /// <summary>
    /// Inverts all pixels contained in this image.
    /// </summary>
    public class InverseOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Inverse";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected unsafe override void Operate()
        {
            InverseOperationExecute(this.GetCppData(this.Image));
        }

        /// <summary>
        /// The native inverse processing function.
        /// </summary>
        /// <param name="src">source image.</param>
        [DllImport("IntergalacticNative.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void InverseOperationExecute(ImageData src);
    }
}
