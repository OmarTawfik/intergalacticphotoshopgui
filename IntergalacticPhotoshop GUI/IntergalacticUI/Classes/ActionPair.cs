namespace IntergalacticUI.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Acts as a data holder for the AddActionCategory() function in MainWindow.
    /// </summary>
    public class ActionPair
    {
        /// <summary>
        /// The action to be activated.
        /// </summary>
        private Action action;

        /// <summary>
        /// The action name.
        /// </summary>
        private string name;

        /// <summary>
        /// Initializes a new instance of the ActionPair class.
        /// </summary>
        /// <param name="action">Action to be activated.</param>
        /// <param name="name">Action bane.</param>
        public ActionPair(Action action, string name)
        {
            this.action = action;
            this.name = name;
        }

        /// <summary>
        /// Gets the action to be activated.
        /// </summary>
        public Action Action
        {
            get { return this.action; }
        }

        /// <summary>
        /// Gets the action name.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }
    }
}
