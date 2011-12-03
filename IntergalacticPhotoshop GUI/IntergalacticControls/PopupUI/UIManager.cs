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
    using IntergalacticControls.PopupUI.Notifications.Base;
    using IntergalacticCore;
    using IntergalacticCore.Data;

    /// <summary>
    /// Enumerates between different notification transitions
    /// </summary>
    public enum NotificationAnimationType
    {
        /// <summary>
        /// Fade animation
        /// </summary>
        Fade,

        /// <summary>
        /// Slide Animation
        /// </summary>
        Slide,

        /// <summary>
        /// Zoom animation
        /// </summary>
        Zoom
    }

    /// <summary>
    /// Provides the required logic for using the Popup view control
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// Current popup view manager
        /// </summary>
        private static UIManager currentUIManager;

        /// <summary>
        /// Main panel to use
        /// </summary>
        private Panel mainPanel;

        /// <summary>
        /// List of the locked popup views
        /// </summary>
        private List<PopupView> lockedPopupViews;

        /// <summary>
        /// Current popup view
        /// </summary>
        private PopupView currentPopupView;
        
        /// <summary>
        /// Dim rectangle
        /// </summary>
        private Rectangle backRectangle;

        /// <summary>
        /// Operation input view to be used for operations
        /// </summary>
        private OperationInputView operationInputView;

        /// <summary>
        /// Dictionary of the currently shown notification views and its animation types
        /// </summary>
        private Dictionary<NotificationView, NotificationAnimationType> notificationViews;

        /// <summary>
        /// Used in animations
        /// </summary>
        private ScaleTransform operationInputViewTransform;

        /// <summary>
        /// Initializes a new instance of the UIManager class
        /// </summary>
        /// <param name="mainPanel">Main panel to use</param>
        public UIManager(Panel mainPanel)
        {
            this.notificationViews = new Dictionary<NotificationView, NotificationAnimationType>();

            this.lockedPopupViews = new List<PopupView>();
            this.operationInputView = new OperationInputView();
            this.operationInputViewTransform = new ScaleTransform();
            this.mainPanel = mainPanel;
            this.currentPopupView = this.CreatePopupView();
            currentUIManager = this;

            this.backRectangle = new Rectangle();
            this.backRectangle.HorizontalAlignment = HorizontalAlignment.Center;
            this.backRectangle.VerticalAlignment = VerticalAlignment.Center;
            this.backRectangle.Width = 3000;
            this.backRectangle.Height = 2000;
            this.backRectangle.Opacity = 0;
            this.backRectangle.Visibility = Visibility.Hidden;
            this.mainPanel.Children.Add(this.backRectangle);

            RadialGradientBrush gradient = new RadialGradientBrush();
            gradient.Center = new Point(0.5, 0.5);
            gradient.GradientStops.Add(new GradientStop(Color.FromArgb(128, 0, 0, 0), 0));
            gradient.GradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 1));

            this.backRectangle.Fill = gradient;
            this.operationInputView.RenderTransform = this.operationInputViewTransform;
            this.operationInputView.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        /// <summary>
        /// Close notification delegate
        /// </summary>
        /// <param name="view">Notification view</param>
        public delegate void CloseNotificationDelegate(NotificationView view);

        /// <summary>
        /// Gets or sets the current popup manager
        /// </summary>
        public static UIManager CurrentUIManager
        {
            get { return currentUIManager; }
            set { currentUIManager = value; }
        }

        /// <summary>
        /// Views a popup view based on a source
        /// </summary>
        /// <param name="source">The source</param>
        public void ViewPopup(StackButtonBase source)
        {
            for (int i = 0; i < this.lockedPopupViews.Count; i++)
            {
                if (source == this.lockedPopupViews[i].Source)
                {
                    return;
                }
            }

            if (source.SubView == null && source.Category == null)
            {
                throw new InvalidOperationException("At least one of the Subview or the Category of the PanelButton must be set.");
            }

            if (source.IsLockable && source.Category != null && source.SubView == null)
            {
                source.IsLockable = false;
            }

            this.currentPopupView.SetPopupContent(source);
            this.currentPopupView.ShowPopup();
            this.RemoveUnusedPopupViews();
        }

        /// <summary>
        /// Hides the current popup view
        /// </summary>
        public void HideCurrentPopup()
        {
            this.currentPopupView.HidePopup();
        }

        /// <summary>
        /// Shows the operation input panel
        /// </summary>
        /// <param name="operation">The operation</param>
        public void ViewOperationInputView(BaseOperation operation)
        {
            this.operationInputView.IsShown = true;
            this.operationInputView.SetInputTarget(operation);
            this.operationInputView.Visibility = Visibility.Visible;
            UIHelpers.SlideInFronBottomAnimation(this.operationInputView, new Thickness(0, 0, 0, 0), 0.5);
            this.mainPanel.Children.Add(this.operationInputView);
            this.ShowBackCover();
        }

        /// <summary>
        /// Closes the operation input panel
        /// </summary>
        public void CloseOperationInputView()
        {
            this.operationInputView.IsShown = false;
            UIHelpers.SlideOutToBottomAnimation(this.operationInputView, 0.5);
            UIHelpers.RemoveElementFromContainerAfterDelay(this.operationInputView, this.mainPanel, 0.5);
            UIHelpers.HideElementAfterDelay(this.operationInputView, 0.5);
            this.HideBackCover();
        }

        /// <summary>
        /// Displays a notification view
        /// </summary>
        /// <param name="view">The notification view</param>
        /// <param name="animation">Animation type</param>
        /// <param name="timeout">Hiding delay</param>
        internal void ViewNotification(NotificationView view, NotificationAnimationType animation, double? timeout)
        {
            if (this.notificationViews.ContainsKey(view))
            {
                throw new InvalidOperationException("This notification instance is already shown.");
            }

            this.notificationViews.Add(view, animation);
            if (view.BlocksUI)
            {
                this.ShowBackCover();
            }

            this.mainPanel.Children.Add(view);
            switch (animation)
            {
                case NotificationAnimationType.Fade:
                    UIHelpers.FadeInAnimation(view, 0, 0.5);
                    break;
                case NotificationAnimationType.Slide:
                    UIHelpers.SlideInFronBottomAnimation(view, new Thickness(0, 0, 0, 0), 0.5);
                    break;
                case NotificationAnimationType.Zoom:
                    UIHelpers.ZoomInAnimation(view, 0.5);
                    break;
                default:
                    break;
            }

            if (timeout != null)
            {
                Timer timer = new Timer(
                    obj =>
                    {
                        if (this.mainPanel.Dispatcher.Thread == Thread.CurrentThread)
                        {
                            this.CloseNoification(view);
                        }
                        else
                        {
                            this.mainPanel.Dispatcher.BeginInvoke(new CloseNotificationDelegate(CloseNoification), view);
                        }
                    },
                null,
                (int)(timeout * 1000),
                Timeout.Infinite);
            }
        }

        /// <summary>
        /// Closes the given notification view
        /// </summary>
        /// <param name="view">The notification view</param>
        internal void CloseNoification(NotificationView view)
        {
            if (!this.notificationViews.ContainsKey(view))
            {
                throw new InvalidOperationException("This notification instance is not shown.");
            }

            switch (this.notificationViews[view])
            {
                case NotificationAnimationType.Fade:
                    UIHelpers.FadeOutAnimation(view, null, 0.5);
                    break;
                case NotificationAnimationType.Slide:
                    UIHelpers.SlideOutToBottomAnimation(view, 0.5);
                    break;
                case NotificationAnimationType.Zoom:
                    UIHelpers.ZoomOutAnimation(view, 0.5);
                    break;
                default:
                    break;
            }

            UIHelpers.RemoveElementFromContainerAfterDelay(view, this.mainPanel, 0.5);
            this.notificationViews.Remove(view);
            this.HideBackCover();
        }

        /// <summary>
        /// Gets whether this popup view is locked
        /// </summary>
        /// <param name="view">The popup view</param>
        /// <returns>Lock status</returns>
        internal bool GetLockStatus(PopupView view)
        {
            return this.lockedPopupViews.Contains(view);
        }

        /// <summary>
        /// Locks the given popup view
        /// </summary>
        /// <param name="view">The popup view</param>
        internal void LockPopup(PopupView view)
        {
            if (!this.lockedPopupViews.Contains(view))
            {
                this.lockedPopupViews.Add(view);
                this.currentPopupView = this.CreatePopupView();
            }
        }

        /// <summary>
        /// Unocks the given popup view
        /// </summary>
        /// <param name="view">The popup view</param>
        internal void UnlockPopup(PopupView view)
        {
            if (this.lockedPopupViews.Contains(view))
            {
                view.HidePopup();
                this.lockedPopupViews.Remove(view);
            }
        }

        /// <summary>
        /// MouseLeave function for popup views.  It hides the view
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event arguments</param>
        private void HidePopups(object sender, MouseEventArgs e)
        {
            PopupView view = sender as PopupView;
            if (this.GetLockStatus(view))
            {
                return;
            }
            else
            {
                this.currentPopupView.HidePopup();
            }
        }

        /// <summary>
        /// Removes unused popup views from the main panel
        /// </summary>
        private void RemoveUnusedPopupViews()
        {
            for (int i = 0; i < this.mainPanel.Children.Count; i++)
            {
                PopupView view = this.mainPanel.Children[i] as PopupView;
                if (view == null)
                {
                    return;
                }

                if (view.Visibility == Visibility.Hidden && !this.lockedPopupViews.Contains(view) && this.currentPopupView != view)
                {
                    this.mainPanel.Children.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Creates a new popup view
        /// </summary>
        /// <returns>The new popup view</returns>
        private PopupView CreatePopupView()
        {
            PopupView view = new PopupView();
            view.HidePopup();
            this.mainPanel.Children.Add(view);
            view.MouseLeave += new MouseEventHandler(this.HidePopups);

            return view;
        }

        /// <summary>
        /// Shows the back rectangle cover
        /// </summary>
        private void ShowBackCover()
        {
            this.backRectangle.Visibility = Visibility.Visible;
            UIHelpers.FadeInAnimation(this.backRectangle, null, 0.5);
        }

        /// <summary>
        /// Hides the back rectangle cover
        /// </summary>
        private void HideBackCover()
        {
            int count = this.operationInputView.IsShown ? 1 : 0;
            for (int i = 0; i < this.notificationViews.Count; i++)
            {
                count += this.notificationViews.ElementAt(i).Key.BlocksUI ? 1 : 0;
            }

            if (count == 0)
            {
                UIHelpers.FadeOutAnimation(this.backRectangle, null, 0.5);
                UIHelpers.HideElementAfterDelay(this.backRectangle, 0.5);
            }
        }
    }
}
