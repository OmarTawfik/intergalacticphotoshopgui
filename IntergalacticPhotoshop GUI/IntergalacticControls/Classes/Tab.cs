namespace IntergalacticControls.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using IntergalacticControls.PopupUI.Notifications;
    using IntergalacticCore;
    using IntergalacticCore.Data;

    /// <summary>
    /// Responsible for loading and saving copies.
    /// </summary>
    public class Tab
    {
        /// <summary>
        /// Shows whether the operation didn't have exceptions
        /// </summary>
        private Exception exception = null;

        /// <summary>
        /// The current image.
        /// </summary>
        private ImageBase image;

        /// <summary>
        /// Name of the current image.
        /// </summary>
        private string name;

        /// <summary>
        /// Current version number.
        /// </summary>
        private int currentVersionNumber;

        /// <summary>
        /// Maximum version number.
        /// </summary>
        private int maxVersionNumber;

        /// <summary>
        /// Thumbnails of the previous operations.
        /// </summary>
        private Stack<ImageBase> thumbnails = new Stack<ImageBase>();

        /// <summary>
        /// Checks if the image is activated and loaded.
        /// </summary>
        private bool isActivated;

        /// <summary>
        /// Checks if the tab cab be deleted.
        /// </summary>
        private bool canBeDeleted;

        /// <summary>
        /// Initializes a new instance of the Tab class.
        /// </summary>
        /// <param name="image">Initial image to be stored.</param>
        /// <param name="name">Name of the image.</param>
        /// <param name="canBeDeleted">Indicates if the tab can be deleted.</param>
        public Tab(ImageBase image, string name, bool canBeDeleted)
        {
            this.image = image;
            this.name = name;
            this.currentVersionNumber = this.maxVersionNumber = 0;
            this.thumbnails.Push(this.image.GetThumbnail());
            this.isActivated = true;
            this.canBeDeleted = canBeDeleted;

            if (Directory.Exists(this.name))
            {
                Directory.Delete(this.name, true);
            }

            Directory.CreateDirectory(this.name);
            this.DeActivate();
        }

        /// <summary>
        /// Finalizes an instance of the Tab class.
        /// </summary>
        ~Tab()
        {
            if (Directory.Exists(this.name))
            {
                Directory.Delete(this.name, true);
            }
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        public ImageBase Image
        {
            get { return this.image; }
        }

        /// <summary>
        /// Gets the image name.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets a value indicating whether the tab can be deleted.
        /// </summary>
        public bool CanBeDeleted
        {
            get { return this.canBeDeleted; }
        }

        /// <summary>
        /// Gets the thumbnail aray.
        /// </summary>
        public Stack<ImageBase> Thumbnails
        {
            get { return this.thumbnails; }
        }

        /// <summary>
        /// Gets a value indicating whether the last operation didn't has exception
        /// </summary>
        public bool DidOperationComplete
        {
            get { return this.exception == null; }
        }

        /// <summary>
        /// Gets the exception occured in the last operation
        /// </summary>
        public Exception LastException
        {
            get
            {
                if (this.exception == null)
                {
                    throw new Exception("No exception occured in the last operation.");
                }

                return this.exception;
            }
        }

        /// <summary>
        /// Executes an operation on the image.
        /// </summary>
        /// <param name="operation">Operation to be executed.</param>
        public void DoOperation(BaseOperation operation)
        {
            this.exception = null;
            try
            {
                this.image = operation.Execute(this.image);
                for (int i = this.maxVersionNumber - this.currentVersionNumber; i > 0; i--)
                {
                    this.thumbnails.Pop();
                }

                this.currentVersionNumber++;
                this.maxVersionNumber = this.currentVersionNumber;
                this.thumbnails.Push(this.image.GetThumbnail());
                this.image.SaveImage(this.GetFullPath(), ImageFileType.BMP);
            }
            catch (Exception ex)
            {
                this.exception = ex;
            }
        }

        /// <summary>
        /// Undos a previous operation.
        /// </summary>
        public void UndoOperation()
        {
            if (this.currentVersionNumber > 0)
            {
                this.currentVersionNumber--;
                this.thumbnails.Pop();
                this.Activate();
            }
        }

        /// <summary>
        /// Redoes a previous operation.
        /// </summary>
        public void RedoOperation()
        {
            if (this.currentVersionNumber < this.maxVersionNumber)
            {
                this.currentVersionNumber++;
                this.Activate();
            }
        }

        /// <summary>
        /// Deactivates the image and saves it to harddisk.
        /// </summary>
        public void DeActivate()
        {
            if (!this.isActivated)
            {
                return;
            }

            this.image.SaveImage(this.GetFullPath(), ImageFileType.BMP);
            this.image = null;
            this.isActivated = false;
        }

        /// <summary>
        /// Activates the image and loads it from harddisk.
        /// </summary>
        public void Activate()
        {
            if (this.isActivated)
            {
                return;
            }

            this.image = new WPFBitmap();
            this.image.LoadImage(this.GetFullPath());
            this.isActivated = true;
        }

        /// <summary>
        /// Full path of the image.
        /// </summary>
        /// <returns>Returns the full path of the image.</returns>
        private string GetFullPath()
        {
            return this.name.ToString() + "\\"
                + this.name.ToString() + "-"
                + this.currentVersionNumber.ToString() + ".bmp";
        }
    }
}
