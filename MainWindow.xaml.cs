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
using System.Diagnostics;
using Coursework.Models;
using System.Collections.ObjectModel;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IServiceProvider _db;
        private FlowersService _flowersService;
        private Window _parentWindow;



        public MainWindow(IServiceProvider db)
        {
            InitializeComponent();

            _db = db;

            _flowersService = new FlowersService(db);


            loadFlowers();
        }

        public void loadFlowers()
        {
            ProductsGrid.ItemsSource = new List<FlowerDto>();
            ProductsGrid.ItemsSource = _flowersService.getListFlowers();
        }

        public void setParentWindow(Window parentWindow)
        {
            _parentWindow = parentWindow;

            this.Left = parentWindow.Left + this.Width;
            this.Top = parentWindow.Top;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var form = _db.GetRequiredService<Form>();
            form.Show();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FlowerDto flowers = (FlowerDto)ProductsGrid.SelectedItem;

            if (flowers.Id == null)
            {
                MessageBox.Show(
                    "Оберіть квітку",
                    "Невдача",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                var form = _db.GetRequiredService<Form>();
                form.setFlower(flowers);
                form.setParentWindow(this);
                form.Show();
            }
        }

        private void ProductsGrid_Selected(object sender, RoutedEventArgs e)
        {
            //FlowerDto flowers = (FlowerDto)ProductsGrid.SelectedItem;
            if (ProductsGrid.SelectedItem is FlowerDto flowers)
            {
                label_name.Content = flowers.Name;
                label_category.Content = flowers.CategoryName;
                label_color.Content = flowers.Color;
                label_price.Content = flowers.Price;
                label_count.Content = flowers.Count;
                label_type.Content = flowers.Type;
                label_desc.Content = flowers.Description;
            }
        }
    }
}