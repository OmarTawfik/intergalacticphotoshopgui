namespace IntergalacticControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
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

    /// <summary>
    /// Specifies the direction of the popup view arrow
    /// </summary>
    internal enum Direction
    {
        /// <summary>
        /// Left direction
        /// </summary>
        Left,

        /// <summary>
        /// Right direction
        /// </summary>
        Right,

        /// <summary>
        /// Up direction
        /// </summary>
        Up,

        /// <summary>
        /// Down direction
        /// </summary>
        Down
    }

    /// <summary>
    /// Interaction logic for PopupView.xaml
    /// </summary>
    public partial class PopupView : UserControl
    {
        /// <summary>
        /// Addtitional distance from the popup view to the target
        /// </summary>
        private static int spaceSeperator = 5;

        /// <summary>
        /// Current popup content
        /// </summary>
        private FrameworkElement currentPopupContent = null;

        /// <summary>
        /// Current popup source
        /// </summary>
        private StackButtonBase currentSource = null;

        /// <summary>
        /// Fade in/out animations
        /// </summary>
        private DoubleAnimation fadeIn, fadeOut;

        /// <summary>
        /// Shows whether the popup is shown or not
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// The main submenu container
        /// </summary>
        private SubmenuContainer submenuContainer;

        /// <summary>
        /// Initializes a new instance of the PopupView class
        /// </summary>
        public PopupView()
        {
            InitializeComponent();
            this.fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.3));
            this.fadeOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            this.Opacity = 0;
            this.Visibility = System.Windows.Visibility.Hidden;

            this.submenuContainer = new SubmenuContainer();
        }

        /// <summary>
        /// Gets or sets the source of the popup
        /// </summary>
        public StackButtonBase Source
        {
            get { return this.currentSource; }
            set { this.currentSource = value; }
        }

        /// <summary>
        /// Sets the popup content
        /// </summary>
        /// <param name="source">Content source</param>
        public void SetPopupContent(StackButtonBase source)
        {
            if (this.currentPopupContent != null)
            {
                this.mainGrid.Children.Remove(this.currentPopupContent);
            }

            if (source.IsLockable)
            {
                this.btnLocker.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.btnLocker.Visibility = System.Windows.Visibility.Hidden;
            }

            this.currentSource = source;
            if (source.Category != null)
            {
                this.currentPopupContent = this.submenuContainer;

                OperationCategory tmp = source.Category as OperationCategory;
                if (tmp != null)
                {
                    this.submenuContainer.SetCategory((OperationCategory)source.Category);
                }
                else
                {
                    this.submenuContainer.SetCategory((ActionCategory)source.Category);
                }
            }
            else
            {
                this.currentPopupContent = source.SubView;
            }

            this.currentSource = source;
            this.Width = this.currentPopupContent.Width + 30;
            this.Height = this.currentPopupContent.Height + 30;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            this.currentPopupContent.Margin = new Thickness(15, 15, 15, 15);
            this.currentPopupContent.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            this.currentPopupContent.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            this.mainGrid.Children.Add(this.currentPopupContent);

            Point centerOfSource = source.TranslatePoint(new Point(source.ActualHeight / 2, source.ActualHeight / 2), UIHelpers.GetParentWindow(this.currentSource));
            this.SetPopupPosition(centerOfSource, new Size(source.ActualWidth, source.ActualHeight), this.currentSource.IsVertical);
        }

        /// <summary>
        /// Shows the popup
        /// </summary>
        public void ShowPopup()
        {
            this.Visibility = System.Windows.Visibility.Visible;
            this.isShown = true;
            this.BeginAnimation(UIElement.OpacityProperty, this.fadeIn);
        }

        /// <summary>
        /// Hides the popup
        /// </summary>
        public void HidePopup()
        {
            this.isShown = false;
            this.BeginAnimation(UIElement.OpacityProperty, this.fadeOut);
            Timer timer = new Timer(
                obj =>
            {
                if (this.Dispatcher.Thread == Thread.CurrentThread)
                {
                    this.MakeInvisible();
                }
                else
                {
                    this.Dispatcher.BeginInvoke(new Action(this.MakeInvisible));
                }
            },
            null,
            300,
            Timeout.Infinite);
        }

        /// <summary>
        /// Makes the popup invisible
        /// </summary>
        internal void MakeInvisible()
        {
            if (this.isShown)
            {
                return;
            }

            this.Visibility = System.Windows.Visibility.Hidden;
            if (this.currentPopupContent != null)
            {
                this.mainGrid.Children.Remove(this.currentPopupContent);
            }
        }

        /// <summary>
        /// Shows the target arrow with a certain shift
        /// </summary>
        /// <param name="direction">Arrow direction</param>
        /// <param name="shiftAmount">Shifting amount</param>
        private void SetCurrentArrow(Direction direction, float shiftAmount)
        {
            float tmp = 0;

            if (shiftAmount > this.ActualHeight - 60)
            {
                tmp = (float)(this.ActualHeight - 60);
            }
            else
            {
                tmp = shiftAmount;
            }

            ThicknessAnimation moveAnimation = new ThicknessAnimation(new Thickness(0, 30 + tmp, 0, 0), TimeSpan.FromSeconds(0.3));
            moveAnimation.AccelerationRatio = 0.3;
            moveAnimation.DecelerationRatio = 0.3;

            this.leftArrowPointer.Visibility = System.Windows.Visibility.Hidden;
            this.leftArrowPointer.BeginAnimation(Control.MarginProperty, moveAnimation);
            this.rightArrowPointer.Visibility = System.Windows.Visibility.Hidden;
            this.rightArrowPointer.BeginAnimation(Control.MarginProperty, moveAnimation);

            if (shiftAmount > this.ActualWidth - 60)
            {
                tmp = (float)(this.ActualWidth - 60);
            }
            else
            {
                tmp = shiftAmount;
            }

            moveAnimation.To = new Thickness(30 + tmp, 0, 0, 0);

            this.upArrowPointer.Visibility = System.Windows.Visibility.Hidden;
            this.upArrowPointer.BeginAnimation(Control.MarginProperty, moveAnimation);
            this.downArrowPointer.Visibility = System.Windows.Visibility.Hidden;
            this.downArrowPointer.BeginAnimation(Control.MarginProperty, moveAnimation);

            switch (direction)
            {
                case Direction.Left:
                    this.leftArrowPointer.Visibility = System.Windows.Visibility.Visible;
                    break;
                case Direction.Right:
                    this.rightArrowPointer.Visibility = System.Windows.Visibility.Visible;
                    break;
                case Direction.Up:
                    this.upArrowPointer.Visibility = System.Windows.Visibility.Visible;
                    break;
                case Direction.Down:
                    this.downArrowPointer.Visibility = System.Windows.Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets the position for the popup view
        /// </summary>
        /// <param name="center">The desired position</param>
        /// <param name="size">Size of the target</param>
        /// <param name="isVertical">Popup orientation</param>
        private void SetPopupPosition(Point center, Size size, bool isVertical)
        {
            FrameworkElement parent = (FrameworkElement)this.Parent;
            Direction targetArrowDirection = Direction.Left;
            if (isVertical)
            {
                targetArrowDirection = Direction.Left;
            }
            else
            {
                targetArrowDirection = Direction.Down;
            }

            Thickness newPanelMargin = new Thickness(0, 0, 0, 0);

            switch (targetArrowDirection)
            {
                case Direction.Left:
                    newPanelMargin.Top = center.Y - 30;

                    if ((center.X + (size.Width / 2) + this.Width + spaceSeperator) > parent.ActualWidth)
                    {
                        targetArrowDirection = Direction.Right;
                        newPanelMargin.Left = center.X - ((size.Width / 2) + this.Width + spaceSeperator);
                    }
                    else
                    {
                        newPanelMargin.Left = center.X + (size.Width / 2) + spaceSeperator;
                    }

                    break;
                case Direction.Down:
                    newPanelMargin.Left = center.X - 30;

                    if ((center.Y + (size.Height / 2) + this.Height + spaceSeperator) > parent.ActualHeight)
                    {
                        newPanelMargin.Top = center.Y - ((size.Height / 2) + this.Height + spaceSeperator);
                    }
                    else
                    {
                        targetArrowDirection = Direction.Up;
                        newPanelMargin.Top = center.Y + (size.Height / 2) + spaceSeperator;
                    }

                    break;
            }

            float shiftAmount = 0;

            if (targetArrowDirection == Direction.Up || targetArrowDirection == Direction.Down)
            {
                if (newPanelMargin.Left < spaceSeperator)
                {
                    newPanelMargin.Left = spaceSeperator;
                }

                if ((newPanelMargin.Left + this.Width + spaceSeperator) > parent.ActualWidth)
                {
                    shiftAmount = (float)((newPanelMargin.Left + this.Width + spaceSeperator) - parent.ActualWidth);
                    newPanelMargin.Left -= shiftAmount;
                }
            }
            else if (targetArrowDirection == Direction.Left || targetArrowDirection == Direction.Right)
            {
                if (newPanelMargin.Top < spaceSeperator)
                {
                    newPanelMargin.Top = spaceSeperator;
                }

                if ((newPanelMargin.Top + this.Height + spaceSeperator) > parent.ActualHeight)
                {
                    shiftAmount = (float)((newPanelMargin.Top + this.Height + spaceSeperator) - parent.ActualHeight);
                    newPanelMargin.Top -= shiftAmount;
                }
            }

            this.SetCurrentArrow(targetArrowDirection, shiftAmount);
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            if (this.isShown)
            {
                ThicknessAnimation moveAnimation = new ThicknessAnimation(newPanelMargin, TimeSpan.FromSeconds(0.3));
                moveAnimation.AccelerationRatio = 0.3;
                moveAnimation.DecelerationRatio = 0.3;
                this.BeginAnimation(PopupView.MarginProperty, moveAnimation);
            }
            else
            {
                this.Margin = newPanelMargin;
                this.SetCurrentValue(PopupView.MarginProperty, newPanelMargin);
            }
        }

        /// <summary>
        /// Click function for the lock button
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void BtnLocker_Click(object sender, RoutedEventArgs e)
        {
            if (PopupViewManager.CurrentPopupManager.GetLockStatus(this))
            {
                PopupViewManager.CurrentPopupManager.UnlockPopup(this);
            }
            else
            {
                PopupViewManager.CurrentPopupManager.LockPopup(this);
            }
        }
    }
}
