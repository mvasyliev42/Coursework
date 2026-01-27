using Coursework.Data;
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
using Coursework.Services;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for Form.xaml
    /// </summary>
    public partial class Form : Window
    {
        private IServiceProvider _db;

        public Form(IServiceProvider db)
        {
            InitializeComponent();
            _db = db;

            var categoryService = new CategoriesService(this._db);

            FormCategory.ItemsSource = categoryService.getListCategories();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var flowerService = new FlowersService(this._db);

         
            flowerService.addFlower((int)FormCategory.SelectedValue, FormName.Text, FormDescription.Text, int.Parse(FormCount.Text), float.Parse(FormPrice.Text), FormType.Text, FormColor.Text);
        }
    }
}
