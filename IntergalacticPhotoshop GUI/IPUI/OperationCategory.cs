
namespace IPUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticCore2;
    using IntergalacticCore2.Data;

    /// <summary>
    /// Used in UI to group operations into categories
    /// </summary>
    public class OperationCategory
    {
        /// <summary>
        /// The title of the category
        /// </summary>
        private string title;

        /// <summary>
        /// The icon of the category
        /// </summary>
        private ImageSource icon;

        /// <summary>
        /// List of processes
        /// </summary>
        private List<BaseOperation> processes;

        /// <summary>
        /// Initializes a new instance of the OperationCategory class
        /// </summary>
        /// <param name="categoryTitle">Category title</param>
        /// <param name="categoryIcon">Category icon</param>
        public OperationCategory(string categoryTitle, ImageSource categoryIcon)
        {
            this.title = categoryTitle;
            this.icon = categoryIcon;

            this.processes = new List<BaseOperation>();
        }

        /// <summary>
        /// Gets the category title
        /// </summary>
        public string Title
        {
            get { return this.title; }
        }

        /// <summary>
        /// Gets the category icon
        /// </summary>
        public ImageSource Icon
        {
            get { return this.icon; }
        }

        /// <summary>
        /// Gets the operation list
        /// </summary>
        public List<BaseOperation> OperationsList
        {
            get { return this.processes; }
        }

        /// <summary>
        /// Adds an image operation to the list
        /// </summary>
        /// <param name="operation">Image operation</param>
        public void AddOperation(BaseOperation operation)
        {
            this.processes.Add(operation);
        }
    }
}
