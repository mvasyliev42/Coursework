using Coursework.Data;
using Microsoft.Extensions.DependencyInjection;
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
using Coursework.Services;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IServiceProvider _db;




        public MainWindow(IServiceProvider db)
        {
            InitializeComponent();

            _db = db;

            FlowersService flowersService = new FlowersService(db);


            ProductsGrid.ItemsSource = flowersService.getListFlowers();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var form = _db.GetRequiredService<Form>();
            form.Show();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}