namespace IntergalacticControls.Classes
{
    using System;
    using System.Collections.Generic;
    using IntergalacticCore;
    using IntergalacticCore.Data;

    /// <summary>
    /// Acts a manager to all GUI based operations.
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static Manager instance = new Manager();

        /// <summary>
        /// Collection of active tabs in the project.
        /// </summary>
        private Dictionary<string, Tab> tabs;

        /// <summary>
        /// Current tab shown to GUI.
        /// </summary>
        private Tab currentTab;

        /// <summary>
        /// Prevents a default instance of the Manager class from being created.
        /// </summary>
        private Manager()
        {
            this.tabs = new Dictionary<string, Tab>();
        }

        /// <summary>
        /// Manager event delegate
        /// </summary>
        /// <param name="mng">The manager</param>
        public delegate void ManagerEvent(Manager mng);

        /// <summary>
        /// Tab event delegate
        /// </summary>
        /// <param name="mng">The manager</param>
        /// <param name="tab">The tab</param>
        public delegate void TabEvent(Manager mng, Tab tab);

        /// <summary>
        /// Gets triggered when a new tab is added.
        /// </summary>
        public event TabEvent OnNewTabAdded;

        /// <summary>
        /// Gets triggered when a tab is closed.
        /// </summary>
        public event TabEvent OnTabClosed;

        /// <summary>
        /// Gets triggered when an operation finishes.
        /// </summary>
        public event TabEvent OnTabChanged;

        /// <summary>
        /// Gets triggered when an operation finishes.
        /// </summary>
        public event ManagerEvent OnOperationFinshed;

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static Manager Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets the current tab.
        /// </summary>
        public Tab CurrentTab
        {
            get { return this.currentTab; }
        }

        /// <summary>
        /// Adds a new tab to active tabs.
        /// </summary>
        /// <param name="image">Image of the new tab.</param>
        /// <param name="name">Name of the new tab.</param>
        /// <param name="canBeDeleted">Indicates if this tab can be deleted.</param>
        public void AddTab(ImageBase image, string name, bool canBeDeleted = true)
        {
            Tab newTab = new Tab(image, name, canBeDeleted);
            this.currentTab = newTab;
            this.tabs.Add(name, newTab);
            this.SwitchImage(name);

            this.OnNewTabAdded(this, newTab);
        }

        /// <summary>
        /// Deletes a tab from the list.
        /// </summary>
        /// <param name="name">Name of the tab.</param>
        public void DeleteTab(string name)
        {
            Tab tab = this.tabs[name];
            tab.DeActivate();
            this.tabs.Remove(name);
            this.OnTabClosed(this, tab);
        }

        /// <summary>
        /// Switches to a new tab.
        /// </summary>
        /// <param name="name">Name of this tab.</param>
        public void SwitchImage(string name)
        {
            if (this.tabs.ContainsKey(name))
            {
                this.currentTab.DeActivate();
                this.currentTab = this.tabs[name];
                this.currentTab.Activate();
                this.OnTabChanged(this, this.CurrentTab);
            }
            else
            {
                this.SwitchImage("Default");
            }
        }

        /// <summary>
        /// Executes an operation on the tab.
        /// </summary>
        /// <param name="operation">Operation to be executed.</param>
        public void DoOperation(BaseOperation operation)
        {
            this.currentTab.DoOperation(operation);
            this.OnOperationFinshed(this);
        }

        /// <summary>
        /// Undos a previous operation.
        /// </summary>
        public void UndoOperation()
        {
            this.currentTab.UndoOperation();
        }

        /// <summary>
        /// Redoes a previous operation.
        /// </summary>
        public void RedoOperation()
        {
            this.currentTab.RedoOperation();
        }

        /// <summary>
        /// Gets a list of all tab names.
        /// </summary>
        /// <returns>Constructed list of tab names.</returns>
        public List<string> GetTabNames()
        {
            List<string> names = new List<string>();
            
            foreach (string name in this.tabs.Keys)
            {
                names.Add(name);
            }

            return names;
        }

        /// <summary>
        /// Gets the tab object by its name
        /// </summary>
        /// <param name="name">The name</param>
        public Tab GetTab(string name)
        {
            return this.tabs[name];
        }
    }
}
