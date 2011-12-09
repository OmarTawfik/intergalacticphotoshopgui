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
    using System.Windows.Threading;

    /// <summary>
    /// Used to pass parameters to a thread start, to remove elements from a panel.
    /// </summary>
    private struct RemoveElementParameters
    {
        /// <summary>
        /// The element to remove
        /// </summary>
        private FrameworkElement element;

        /// <summary>
        /// The container of the element
        /// </summary>
        private Panel panel;

        /// <summary>
        /// Delay before element removal
        /// </summary>
        private double delayInSeconds;

        /// <summary>
        /// Initializes a new instance of the RemoveElementParameters struct
        /// </summary>
        /// <param name="element">The element to remove</param>
        /// <param name="panel">The parent panel</param>
        /// <param name="delayInSeconds">Time before removal</param>
        public RemoveElementParameters(FrameworkElement element, Panel panel, double delayInSeconds)
        {
            this.element = element;
            this.panel = panel;
            this.delayInSeconds = delayInSeconds;
        }

        /// <summary>
        /// Gets the element to remove
        /// </summary>
        public FrameworkElement Element
        {
            get { return this.element; }
        }

        /// <summary>
        /// Gets the parent of the element
        /// </summary>
        public Panel Panel
        {
            get { return this.panel; }
        }

        /// <summary>
        /// Gets the delay, in seconds
        /// </summary>
        public double DelayInSeconds
        {
            get { return this.delayInSeconds; }
        }
    }

    /// <summary>
    /// Provides helper funcitons relative to the UI framwwork
    /// </summary>
    internal class UIHelpers
    {
        /// <summary>
        /// Delegate for element removal function
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="panel">The Panel</param>
        private delegate void RemoveElementDelegate(FrameworkElement element, Panel panel);

        /// <summary>
        /// Delegate for element hiding function
        /// </summary>
        /// <param name="element">The element</param>
        private delegate void HideElementDelegate(FrameworkElement element);

        /// <summary>
        /// Gets the parent window of a control
        /// </summary>
        /// <param name="child">The control</param>
        /// <returns>The parent window</returns>
        public static Window GetParentWindow(DependencyObject child)
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
            {
                return null;
            }

            Window parent = parentObject as Window;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return GetParentWindow(parentObject);
            }
        }

        /// <summary>
        /// Applies fade in animation to the given element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="from">Optional from value</param>
        /// <param name="timeInSeconds">Time of the animation</param>
        public static void FadeInAnimation(FrameworkElement element, double? from, double timeInSeconds)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = TimeSpan.FromSeconds(timeInSeconds);
            animation.AccelerationRatio = 0.3;
            animation.DecelerationRatio = 0.3;
            animation.To = 1;
            animation.From = (from != null) ? from : element.Opacity;
            
            element.BeginAnimation(FrameworkElement.OpacityProperty, animation);
        }

        /// <summary>
        /// Applies fade out animation to the given element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="to">Optional to value</param>
        /// <param name="timeInSeconds">Time of the animation</param>
        public static void FadeOutAnimation(FrameworkElement element, double? to, double timeInSeconds)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = TimeSpan.FromSeconds(timeInSeconds);
            animation.AccelerationRatio = 0.3;
            animation.DecelerationRatio = 0.3;
            animation.From = element.Opacity;
            animation.To = (to != null) ? to : 0;

            element.BeginAnimation(FrameworkElement.OpacityProperty, animation);
        }

        /// <summary>
        /// Applies slide in animation to the given element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="to">The target margin</param>
        /// <param name="timeInSeconds">Time of the animation</param>
        public static void SlideInFronBottomAnimation(FrameworkElement element, Thickness to, double timeInSeconds)
        {
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromSeconds(timeInSeconds);
            animation.DecelerationRatio = 0.6;
            animation.To = to;
            animation.From = new Thickness(element.Margin.Left, element.Margin.Top + 1000, element.Margin.Right, element.Margin.Bottom);

            element.BeginAnimation(FrameworkElement.MarginProperty, animation);
        }

        /// <summary>
        /// Applies slide out animation to the given element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="timeInSeconds">Time of the animation</param>
        public static void SlideOutToBottomAnimation(FrameworkElement element, double timeInSeconds)
        {
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromSeconds(timeInSeconds);
            animation.AccelerationRatio = 0.6;
            animation.From = element.Margin;
            animation.To = new Thickness(element.Margin.Left, element.Margin.Top + 1000, element.Margin.Right, element.Margin.Bottom);

            element.BeginAnimation(FrameworkElement.MarginProperty, animation);
        }

        /// <summary>
        /// Applies zoom in animation to the given element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="timeInSeconds">Time of the animation</param>
        public static void ZoomInAnimation(FrameworkElement element, double timeInSeconds)
        {
            element.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform trasform = element.RenderTransform as ScaleTransform;

            if (trasform == null)
            {
                element.RenderTransform = trasform = new ScaleTransform();
            }

            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = TimeSpan.FromSeconds(timeInSeconds);
            animation.DecelerationRatio = 0.6;
            animation.From = 1.2;
            animation.To = 1;

            trasform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            trasform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

            FadeInAnimation(element, null, timeInSeconds);
        }

        /// <summary>
        /// Applies zoom out animation to the given element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="timeInSeconds">Time of the animation</param>
        public static void ZoomOutAnimation(FrameworkElement element, double timeInSeconds)
        {
            element.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform trasform = element.RenderTransform as ScaleTransform;

            if (trasform == null)
            {
                element.RenderTransform = trasform = new ScaleTransform();
            }

            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = TimeSpan.FromSeconds(timeInSeconds);
            animation.AccelerationRatio = 0.6;
            animation.From = 1;
            animation.To = 0.8;

            trasform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            trasform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

            FadeOutAnimation(element, null, timeInSeconds);
        }

        /// <summary>
        /// Removes element from it's container after delay
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="panel">The container</param>
        /// <param name="delayInSeconds">Delay time in seconds</param>
        public static void RemoveElementFromContainerAfterDelay(FrameworkElement element, Panel panel, double delayInSeconds)
        {
            RemoveElementParameters parameters = new RemoveElementParameters(element, panel, delayInSeconds);
            Thread thread = new Thread(new ParameterizedThreadStart(new ParameterizedThreadStart(RemoveElementThreadStart)));
        }

        /// <summary>
        /// Hides element after delay
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="delayInSeconds">Delay time in seconds</param>
        public static void HideElementAfterDelay(FrameworkElement element, double delayInSeconds)
        {
            Timer timer = new Timer(
                obj =>
                {
                    if (element.Dispatcher.Thread == Thread.CurrentThread)
                    {
                        HideElementHelper(element);
                    }
                    else
                    {
                        element.Dispatcher.BeginInvoke(new HideElementDelegate(HideElementHelper), element).Wait();
                    }
                },
            null,
            (int)(delayInSeconds * 1000),
            Timeout.Infinite);
        }

        /// <summary>
        /// Call a function by using the given dispatcher object after delay
        /// </summary>
        /// <param name="delayInSeconds">Delay time in seconds</param>
        /// <param name="dispatcher">The dispatcher object</param>
        /// <param name="function">The function to call</param>
        /// <param name="parameters">Function parameters</param>
        public static void CallFunctionAfterDelay(double delayInSeconds, Dispatcher dispatcher, Delegate function, params object[] parameters)
        {
            Timer timer = new Timer(
                obj =>
                {
                    if (dispatcher.Thread == Thread.CurrentThread)
                    {
                        switch (parameters.Length)
                        {
                            case 0:
                                function.DynamicInvoke();
                                break;
                            case 1:
                                function.DynamicInvoke(parameters[0]);
                                break;
                            case 2:
                                function.DynamicInvoke(parameters[0], parameters[1]);
                                break;
                            case 3:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2]);
                                break;
                            case 4:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2], parameters[3]);
                                break;
                            case 5:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
                                break;
                            case 6:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5]);
                                break;
                            case 7:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6]);
                                break;
                            case 8:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[1]);
                                break;
                            case 9:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);
                                break;
                            case 10:
                                function.DynamicInvoke(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9]);
                                break;
                            default:
                                throw new InvalidOperationException("Parameters are too much.");
                        }
                    }
                    else
                    {
                        switch (parameters.Length)
                        {
                            case 0:
                                dispatcher.BeginInvoke(function).Wait();
                                break;
                            case 1:
                                dispatcher.BeginInvoke(function, parameters[0]).Wait();
                                break;
                            case 2:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1]).Wait();
                                break;
                            case 3:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2]).Wait();
                                break;
                            case 4:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2], parameters[3]).Wait();
                                break;
                            case 5:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]).Wait();
                                break;
                            case 6:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5]).Wait();
                                break;
                            case 7:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6]).Wait();
                                break;
                            case 8:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[1]).Wait();
                                break;
                            case 9:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]).Wait();
                                break;
                            case 10:
                                dispatcher.BeginInvoke(function, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9]).Wait();
                                break;
                            default:
                                throw new InvalidOperationException("Parameters are too much.");
                        }
                    }
                },
            null,
            (int)(delayInSeconds * 1000),
            Timeout.Infinite);
        }

        /// <summary>
        /// Thread start for control removal
        /// </summary>
        /// <param name="parameters">Removal parameters</param>
        private static void RemoveElementThreadStart(object parameters)
        {
            RemoveElementParameters param = (RemoveElementParameters)parameters;
            Thread.Sleep((int)(param.DelayInSeconds * 1000));
            if (param.Panel.Dispatcher.Thread == Thread.CurrentThread)
            {
                RemoveElementHelper(param.Element, param.Panel);
            }
            else
            {
                param.Panel.Dispatcher.BeginInvoke(new RemoveElementDelegate(RemoveElementHelper), param.Element, param.Panel).Wait();
            }
        }

        /// <summary>
        /// Helper function for removing the given element from its panel
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="panel">The panel</param>
        private static void RemoveElementHelper(FrameworkElement element, Panel panel)
        {
            if (panel.Children.Contains(element))
            {
                panel.Children.Remove(element);
            }
        }

        /// <summary>
        /// Helper function for hiding the given element
        /// </summary>
        /// <param name="element">The element</param>
        private static void HideElementHelper(FrameworkElement element)
        {
            element.Visibility = Visibility.Hidden;
        }
    }
}
