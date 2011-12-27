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
    using IntergalacticCore.Operations.HistogramOperations;

    /// <summary>
    /// Interaction logic for IPUIHistogramView.xaml
    /// </summary>
    public partial class HistogramView : UserControl
    {
        /// <summary>
        /// Histogram calculator used in to get histogram data
        /// </summary>
        private HistogramCalculator histogram;

        /// <summary>
        /// Holds the normalized components values
        /// </summary>
        private float[] red, green, blue, gray;

        /// <summary>
        /// Initializes a new instance of the HistogramView class
        /// </summary>
        public HistogramView()
        {
            InitializeComponent();
            this.histogram = new HistogramCalculator();

            Manager.Instance.OnOperationFinshed += this.UpdateHistogram;
            Manager.Instance.OnTabChanged += this.UpdateHistogramWhenTabChanges;
        }

        /// <summary>
        /// Updates histogram data and graph
        /// </summary>
        /// <param name="mng">The manager instance</param>
        /// <param name="operation">The operation</param>
        public void UpdateHistogram(Manager mng, BaseOperation operation)
        {
            this.histogram.Execute(mng.CurrentTab.Image);
            this.red = this.NormalizeArray(this.histogram.Red);
            this.green = this.NormalizeArray(this.histogram.Green);
            this.blue = this.NormalizeArray(this.histogram.Blue);
            this.gray = this.NormalizeArray(this.histogram.Gray);

            this.UpdateHistogramGraph();
        }

        /// <summary>
        /// Updates histogram data and graph
        /// </summary>
        /// <param name="mng">The manager instance</param>
        /// <param name="tab">The tab</param>
        public void UpdateHistogramWhenTabChanges(Manager mng, Tab tab)
        {
            if (tab == null)
            {
                return;
            }

            this.UpdateHistogram(mng, null);
        }

        /// <summary>
        /// Update histogram graph on mouse enter
        /// </summary>
        /// <param name="e">Mouse event argumentss</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.UpdateHistogramGraph();
        }

        /// <summary>
        /// To update the histogram graph
        /// </summary>
        /// <param name="oldParent">Old parent</param>
        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            UIHelpers.CallFunctionAfterDelay(1, this.Dispatcher, new Action(this.UpdateHistogramGraph));
        }

        /// <summary>
        /// Updates the histogram graph
        /// </summary>
        private void UpdateHistogramGraph()
        {
            if (this.gray == null)
            {
                return;
            }

            this.RedGraph.Points.Clear();
            this.GreenGraph.Points.Clear();
            this.BlueGraph.Points.Clear();
            this.GrayGraph.Points.Clear();

            double graphScaleX = (this.ActualWidth - 20) / 255.0;
            double graphScaleY = this.ActualHeight - 20;

            this.RedGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.red.Length; i++)
            {
                this.RedGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.red[i])));
            }

            this.RedGraph.Points.Add(new Point(graphScaleX * 255, graphScaleY));

            this.GreenGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.green.Length; i++)
            {
                this.GreenGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.green[i])));
            }

            this.GreenGraph.Points.Add(new Point(graphScaleX * 255, graphScaleY));

            this.BlueGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.blue.Length; i++)
            {
                this.BlueGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.blue[i])));
            }

            this.BlueGraph.Points.Add(new Point(graphScaleX * 255, graphScaleY));

            this.GrayGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.gray.Length; i++)
            {
                this.GrayGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.gray[i])));
            }

            this.GrayGraph.Points.Add(new Point(graphScaleX * 255, graphScaleY));
        }

        /// <summary>
        /// Normalized the sent array
        /// </summary>
        /// <param name="array">The array</param>
        /// <returns>The normalized array</returns>
        private float[] NormalizeArray(int[] array)
        {
            int max = array.Max();
            float[] result = new float[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = (float)array[i] / (float)max;
            }

            return result;
        }
    }
}
