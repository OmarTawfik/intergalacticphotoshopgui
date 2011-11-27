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
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using IntergalacticControls.Classes;
    using IntergalacticCore;
    using IntergalacticCore.Data;

    /// <summary>
    /// Interaction logic for SubMenu.xaml
    /// </summary>
    public partial class SubMenu : UserControl
    {
        /// <summary>
        /// Operation to be performed
        /// </summary>
        private BaseOperation operation;

        /// <summary>
        /// Action to perform
        /// </summary>
        private Action action;

        /// <summary>
        /// Fade in/out animations
        /// </summary>
        private DoubleAnimation fadeIn, fadeOut;

        /// <summary>
        /// Initializes a new instance of the SubMenu class
        /// </summary>
        public SubMenu()
        {
            InitializeComponent();
            this.fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.1));
            this.fadeOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.1));
        }

        /// <summary>
        /// Gets the operation to perform
        /// </summary>
        public BaseOperation Operation
        {
            get { return this.operation; }
        }

        /// <summary>
        /// Gets the action of the submenu
        /// </summary>
        public Action Action
        {
            get { return this.action; }
        }

        /// <summary>
        /// Sets the menu operation
        /// </summary>
        /// <param name="operation">The Operation</param>
        public void SetMenuData(BaseOperation operation)
        {
            this.lblMenuTitle.Content = operation.ToString();
            this.operation = operation;
        }

        /// <summary>
        /// Sets the menu action
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="title">The title</param>
        public void SetMenuData(Action action, string title)
        {
            this.lblMenuTitle.Content = title;
            this.action = action;
        }

        /// <summary>
        /// MouseEnter function to add animations
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            rectHover.BeginAnimation(UIElement.OpacityProperty, this.fadeIn);
        }

        /// <summary>
        /// MouseLeave function to add animations
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            rectHover.BeginAnimation(UIElement.OpacityProperty, this.fadeOut);
        }

        /// <summary>
        /// MouseUp function to perform actions
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.Operation != null)
            {
                if (this.Operation.GetInput() != string.Empty)
                {
                    PopupViewManager.CurrentPopupManager.ViewOperationInputView(this.Operation);
                }
                else
                {
                    Manager.Instance.DoOperation(this.operation);
                }
            }
            else if (this.Action != null)
            {
                this.Action();
            }
            else
            {
                throw new InvalidOperationException("Either the operation or the action of the menu must be set.");
            }
        }
    }
}
