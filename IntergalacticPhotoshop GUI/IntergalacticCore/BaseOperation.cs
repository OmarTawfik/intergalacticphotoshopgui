namespace IntergalacticCore
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using IntergalacticCore.Data;

    /// <summary>
    /// Provides a base for all image based operations.
    /// </summary>
    public abstract class BaseOperation
    {
        /// <summary>
        /// Provides a storage to the image being executed.
        /// </summary>
        private ImageBase image;

        /// <summary>
        /// Total time the operation executed in.
        /// </summary>
        private TimeSpan operatingTime;

        /// <summary>
        /// Gets the time the operation executed in.
        /// </summary>
        public TimeSpan OperatingTime
        {
            get { return this.operatingTime; }
        }

        /// <summary>
        /// Gets or sets the image being executed.
        /// </summary>
        protected ImageBase Image
        {
            get { return this.image; }
            set { this.image = value; }
        }

        /// <summary>
        /// Sets all input associated with this operation.
        /// </summary>
        /// <param name="input">Array of input to be used.</param>
        public virtual void SetInput(params object[] input)
        {
        }

        /// <summary>
        /// Gets all input types associated with this operation.
        /// </summary>
        /// <returns>Information about input types.</returns>
        public virtual string GetInput()
        {
            return string.Empty;
        }

        /// <summary>
        /// Prepares the variables and calls the actual operation.
        /// </summary>
        /// <param name="image">The input image to be processed.</param>
        /// <returns>The resulting image.</returns>
        public ImageBase Execute(ImageBase image)
        {
            DateTime start = DateTime.Now;
            this.image = image;
            this.BeforeOperate();

            this.image.BeforeEdit();
            this.Operate();
            this.image.AfterEdit();

            this.AfterOperate();
            this.operatingTime = DateTime.Now - start;

            return this.image;
        }

        /// <summary>
        /// Gets called before the operation begins.
        /// </summary>
        protected virtual void BeforeOperate()
        {
        }

        /// <summary>
        /// Gets called after the operation ends.
        /// </summary>
        protected virtual void AfterOperate()
        {
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected virtual void Operate()
        {
        }
    }
}
