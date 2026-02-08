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
using Microsoft.Extensions.DependencyInjection;
using Coursework.Services;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for Dashbord.xaml
    /// </summary>
    public partial class Dashbord : Window
    {
        private IServiceProvider _serviceProvider;
        private FlowersService _objFlowersService;
        private Window _parentWindow;

        public Dashbord(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            DataContext = this;
            _serviceProvider = serviceProvider;

            _objFlowersService = _serviceProvider.GetRequiredService<FlowersService>();

            loadFading();
            loadTop5Counts();
        }

        public void setParentWindow(Window parentWindow)
        {
            _parentWindow = parentWindow;

            this.Left = parentWindow.Left;
            this.Top = parentWindow.Top;
            this.Width = parentWindow.Width;
            this.Height = parentWindow.Height;
        }

        public void loadFading()
        {
            datagrid_fading.ItemsSource = _objFlowersService.getTop5FadingFlowers();
        }

        public void loadTop5Counts()
        {
            datagrid_top5.ItemsSource = _objFlowersService.getTop5CountsFlowers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var windows = _serviceProvider.GetRequiredService<MainWindow>();
            windows.setParentWindow(this);
            windows.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var windows = _serviceProvider.GetRequiredService<Form>();
            windows.Show();
        }
    }
}
