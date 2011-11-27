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

    /// <summary>
    /// Used to store actions with titles
    /// </summary>
    public class ActionCategory : Category
    {
        /// <summary>
        /// The actions dictionary
        /// </summary>
        private Dictionary<string, Action> actionsDictionary;

        /// <summary>
        /// Initializes a new instance of the ActionCategory class
        /// </summary>
        /// <param name="categoryTitle">Category title</param>
        /// <param name="categoryIcon">Category icon</param>
        public ActionCategory(string categoryTitle, ImageSource categoryIcon) : base(categoryTitle, categoryIcon)
        {
            this.actionsDictionary = new Dictionary<string, Action>();
        }

        /// <summary>
        /// Gets the number of items stored in this object
        /// </summary>
        public override int Count
        {
            get { return this.actionsDictionary.Count; }
        }

        /// <summary>
        /// Adds an action to the category
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="title">The title</param>
        public void AddAction(Action action, string title)
        {
            this.actionsDictionary.Add(title, action);
        }

        /// <summary>
        /// Gets the title of the given index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The title</returns>
        public override string GetTitleAtIndex(int index)
        {
            return this.actionsDictionary.Keys.ElementAt(index);
        }

        /// <summary>
        /// Gets the action of the given index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The action</returns>
        public Action GetActionAtIndex(int index)
        {
            return this.actionsDictionary[this.actionsDictionary.Keys.ElementAt(index)];
        }
    }
}
