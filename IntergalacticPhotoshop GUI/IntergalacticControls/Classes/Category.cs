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
    /// Categorizes a group of objects
    /// </summary>
    public abstract class Category
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
        /// Initializes a new instance of the Category class
        /// </summary>
        /// <param name="categoryTitle">Category title</param>
        /// <param name="categoryIcon">Category icon</param>
        public Category(string categoryTitle, ImageSource categoryIcon)
        {
            this.title = categoryTitle;
            this.icon = categoryIcon;
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
        /// Gets the number of items stored in this object
        /// </summary>
        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// Gets the title of the given index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The title</returns>
        public abstract string GetTitleAtIndex(int index);
    }
}
