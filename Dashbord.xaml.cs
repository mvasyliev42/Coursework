using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for Dashbord.xaml
    /// </summary>
    public partial class Dashbord : Window
    {
        public PlotModel PlotModel { get; private set; }

        public Dashbord()
        {
            InitializeComponent();
            DataContext = this;
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };

            // Create two line series (markers are hidden by default)
            var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
            series1.Points.Add(new DataPoint(0, 0));
            series1.Points.Add(new DataPoint(10, 18));
            series1.Points.Add(new DataPoint(20, 12));
            series1.Points.Add(new DataPoint(30, 8));
            series1.Points.Add(new DataPoint(40, 15));

            var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };
            series2.Points.Add(new DataPoint(0, 4));
            series2.Points.Add(new DataPoint(10, 12));
            series2.Points.Add(new DataPoint(20, 16));
            series2.Points.Add(new DataPoint(30, 25));
            series2.Points.Add(new DataPoint(40, 5));


            // Add the series to the plot model
            tmp.Series.Add(series1);
            tmp.Series.Add(series2);
            PlotModel = tmp;
        }

    }
}
