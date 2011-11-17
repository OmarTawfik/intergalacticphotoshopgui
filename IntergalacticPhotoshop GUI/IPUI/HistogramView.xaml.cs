
namespace IPUI
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
    using IntergalacticCore2;
    using IntergalacticCore2.Data;
    using IntergalacticCore2.Operations.HistogramOperations;

    /// <summary>
    /// Interaction logic for IPUIHistogramView.xaml
    /// </summary>
    public partial class HistogramView : UserControl
    {
        private HistogramCalculator histogram;

        public HistogramView()
        {
            InitializeComponent();
            this.histogram = new HistogramCalculator();
            this.Opacity = 0;
        }

        public void UpdateHistogram()
        {
            ////histogram.Execute(///INSERT IMAGE SOURCE HERE///);
            this.UpdateHistogramGraph();
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.UpdateHistogramGraph();
        }

        private void UpdateHistogramGraph()
        {
            this.RedGraph.Points.Clear();
            this.GreenGraph.Points.Clear();
            this.BlueGraph.Points.Clear();
            this.GrayGraph.Points.Clear();

            double graphScaleX = (this.ActualWidth - 20) / 255.0;
            double graphScaleY = this.ActualHeight - 20;

            this.RedGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.histogram.Red.Length; i++)
            {
                this.RedGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.histogram.Red[i])));
            }

            this.RedGraph.Points.Add(new Point(graphScaleX, graphScaleY));

            this.GreenGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.histogram.Green.Length; i++)
            {
                this.GreenGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.histogram.Green[i])));
            }

            this.GreenGraph.Points.Add(new Point(graphScaleX, graphScaleY));

            this.BlueGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.histogram.Blue.Length; i++)
            {
                this.BlueGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.histogram.Blue[i])));
            }

            this.BlueGraph.Points.Add(new Point(graphScaleX, graphScaleY));

            this.GrayGraph.Points.Add(new Point(0, graphScaleY));
            for (int i = 0; i < this.histogram.Gray.Length; i++)
            {
                this.GrayGraph.Points.Add(new Point(graphScaleX * i, graphScaleY * (1.0 - this.histogram.Gray[i])));
            }

            this.GrayGraph.Points.Add(new Point(graphScaleX, graphScaleY));
        }
    }
}
