﻿namespace IntergalacticControls
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
    using IntergalacticCore;
    using IntergalacticCore.Data;

    /// <summary>
    /// Provides the required logic for using the Popup view control
    /// </summary>
    public class UIManager
    {
        /// <summary>
        /// Current popup view manager
        /// </summary>
        private static UIManager currentPopupViewManager;

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
        /// Used in animations
        /// </summary>
        private ScaleTransform operationInputViewTransform;

        /// <summary>
        /// Initializes a new instance of the PopupViewManager class
        /// </summary>
        /// <param name="mainPanel">Main panel to use</param>
        public UIManager(Panel mainPanel)
        {
            this.lockedPopupViews = new List<PopupView>();
            this.operationInputView = new OperationInputView();
            this.operationInputViewTransform = new ScaleTransform();
            this.mainPanel = mainPanel;
            this.currentPopupView = this.CreatePopupView();
            currentPopupViewManager = this;

            this.backRectangle = new Rectangle();
            this.backRectangle.HorizontalAlignment = HorizontalAlignment.Center;
            this.backRectangle.VerticalAlignment = VerticalAlignment.Center;
            this.backRectangle.Width = 3000;
            this.backRectangle.Height = 2000;

            RadialGradientBrush gradient = new RadialGradientBrush();
            gradient.Center = new Point(0.5, 0.5);
            gradient.GradientStops.Add(new GradientStop(Color.FromArgb(128, 0, 0, 0), 0));
            gradient.GradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 1));

            this.backRectangle.Fill = gradient;
            this.operationInputView.RenderTransform = this.operationInputViewTransform;
            this.operationInputView.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        /// <summary>
        /// Gets or sets the current popup manager
        /// </summary>
        public static UIManager CurrentPopupManager
        {
            get { return currentPopupViewManager; }
            set { currentPopupViewManager = value; }
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

            if (source.IsLockable && source.Category != null)
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
            this.operationInputView.SetInputTarget(operation);

            DoubleAnimation fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
            fadeIn.AccelerationRatio = 0.3;
            fadeIn.DecelerationRatio = 0.3;

            ThicknessAnimation slideAnimation = new ThicknessAnimation(new Thickness(0, 1000, 0, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.5));
            slideAnimation.DecelerationRatio = 0.6;

            DoubleAnimation scaleAnimation = new DoubleAnimation(1.7, 1, TimeSpan.FromSeconds(0.5));
            scaleAnimation.DecelerationRatio = 0.3;

            ////this.operationInputView.Opacity = 0;
            ////this.operationInputView.BeginAnimation(UIElement.OpacityProperty, fadeIn);
            this.operationInputView.BeginAnimation(Control.MarginProperty, slideAnimation);
            ////this.opInputViewTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            ////this.opInputViewTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            ////
            this.backRectangle.BeginAnimation(Rectangle.OpacityProperty, fadeIn);

            this.mainPanel.Children.Add(this.backRectangle);
            this.mainPanel.Children.Add(this.operationInputView);
        }

        /// <summary>
        /// Closes the operation input panel
        /// </summary>
        public void CloseOperationInputView()
        {
            DoubleAnimation fadeOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            fadeOut.AccelerationRatio = 0.3;
            fadeOut.DecelerationRatio = 0.3;

            ThicknessAnimation slideAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, 1000, 0, 0), TimeSpan.FromSeconds(0.5));
            slideAnimation.AccelerationRatio = 0.6;

            DoubleAnimation scaleAnimation = new DoubleAnimation(1, 0.8, TimeSpan.FromSeconds(0.5));
            scaleAnimation.AccelerationRatio = 0.3;

            ////this.operationInputView.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            this.operationInputView.BeginAnimation(Control.MarginProperty, slideAnimation);
            ////this.opInputViewTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            ////this.opInputViewTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            ////
            this.backRectangle.BeginAnimation(Rectangle.OpacityProperty, fadeOut);

            Timer timer = new Timer(
                obj =>
                {
                    if (this.mainPanel.Dispatcher.Thread == Thread.CurrentThread)
                    {
                        this.RemoveOperationInputView();
                    }
                    else
                    {
                        this.mainPanel.Dispatcher.BeginInvoke(new Action(this.RemoveOperationInputView));
                    }
                },
            null,
            500,
            Timeout.Infinite);
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
        /// Removes the operation input panel and the dim rectangle from the main panel
        /// </summary>
        private void RemoveOperationInputView()
        {
            this.mainPanel.Children.Remove(this.operationInputView);
            this.mainPanel.Children.Remove(this.backRectangle);
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
    }
}
