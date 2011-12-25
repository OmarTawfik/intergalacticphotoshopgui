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
    /// Forms the base class for buttons that can be added into the StackController
    /// </summary>
    public class StackButtonBase : UserControl
    {
        /// <summary>
        /// Describes the orientation of the popup views to follow
        /// </summary>
        private bool isVertical;

        /// <summary>
        /// Describes whether the resulting popup view can be lockable or not
        /// </summary>
        private bool isLockable = false;

        /// <summary>
        /// The sub view to show in the popup view
        /// </summary>
        private List<FrameworkElement> subViews;

        /// <summary>
        /// Title of the button
        /// </summary>
        private string title;

        /// <summary>
        /// Initializes a new instance of the StackButtonBase class
        /// </summary>
        public StackButtonBase()
        {
            this.subViews = new List<FrameworkElement>();
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the resulting popup view can be locked
        /// </summary>
        public bool IsLockable
        {
            get { return this.isLockable; }
            set { this.isLockable = value; }
        }
        
        /// <summary>
        /// Gets or sets the sub view of its resulting popup view
        /// </summary>
        public List<FrameworkElement> SubViews
        {
            get { return this.subViews; }
            set { this.subViews = value; }
        }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the resulting popup view is vertical
        /// </summary>
        internal bool IsVertical
        {
            get { return this.isVertical; }
            set { this.isVertical = value; }
        }
    }
}
