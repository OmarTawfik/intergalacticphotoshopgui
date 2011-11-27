namespace IntergalacticControls
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
    using IntergalacticCore;
    using IntergalacticCore.Data;

    /// <summary>
    /// Used in UI to group operations into categories
    /// </summary>
    public class OperationCategory : Category
    {
        /// <summary>
        /// List of processes
        /// </summary>
        private List<BaseOperation> operations;

        /// <summary>
        /// Initializes a new instance of the OperationCategory class
        /// </summary>
        /// <param name="categoryTitle">Category title</param>
        /// <param name="categoryIcon">Category icon</param>
        public OperationCategory(string categoryTitle, ImageSource categoryIcon) : base(categoryTitle, categoryIcon)
        {
            this.operations = new List<BaseOperation>();
        }

        /// <summary>
        /// Gets the number of items stored in this object
        /// </summary>
        public override int Count
        {
            get
            {
                return this.operations.Count;
            }
        }

        /// <summary>
        /// Adds an image operation to the list
        /// </summary>
        /// <param name="operation">Image operation</param>
        public void AddOperation(BaseOperation operation)
        {
            this.operations.Add(operation);
        }

        /// <summary>
        /// Gets the title of the given index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The title</returns>
        public override string GetTitleAtIndex(int index)
        {
            return this.operations[index].ToString();
        }

        /// <summary>
        /// Get the operation at a given index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The operation</returns>
        public BaseOperation GetOperationAtIndex(int index)
        {
            return this.operations[index];
        }
    }
}
