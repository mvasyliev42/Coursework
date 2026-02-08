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
using System.Diagnostics;
using Coursework.Models;

namespace Coursework
{
    /// <summary>
    /// Interaction logic for Form.xaml
    /// </summary>
    public partial class Form : Window
    {
        private IServiceProvider _db;
        private CategoriesService _categoryService;
        private FlowerDto _flower;
        private Window _parentWindow;

        public Form(IServiceProvider db)
        {
            InitializeComponent();
            _db = db;

            _categoryService = new CategoriesService(this._db);

            FormCategory.ItemsSource = _categoryService.getListCategories();


        }

        public void setParentWindow(Window parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public void setFlower(FlowerDto flower)
        {

            if (flower != null)
            {
                _flower = flower;
                FormCategory.SelectedValue = _categoryService.getListCategories().FirstOrDefault(c => c.Name == flower.CategoryName)?.Id;
                FormName.Text = flower.Name;
                FormDescription.Text = flower.Description;
                FormCount.Text = flower.Count.ToString();
                FormPrice.Text = flower.Price.ToString();
                FormType.Text = flower.Type;
                FormColor.Text = flower.Color;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var flowerService = new FlowersService(this._db);

            Debug.WriteLine(FormCategory.SelectedValue);

            int categoryId = 1;

            if (FormCategory.SelectedItem == null)
            {
                CategoriesService categoryService = new CategoriesService(this._db);
                var category = categoryService.addCategory(FormCategory.Text);
                categoryId = category.Id;
            }
            else
            {
                categoryId = (int)FormCategory.SelectedValue;
            }

            bool status = false;


            if (_flower.Id == null)
            {
                status = flowerService.addFlower(categoryId, FormName.Text, FormDescription.Text, int.Parse(FormCount.Text), float.Parse(FormPrice.Text), FormType.Text, FormColor.Text);

            }
            else
            {
                status = flowerService.UpdateFlower(_flower.Id, categoryId, FormName.Text, FormDescription.Text, int.Parse(FormCount.Text), float.Parse(FormPrice.Text), FormType.Text, FormColor.Text);
            }


            if (status)
            {
                MessageBox.Show(
                    "Запис успішно збережено",
                    "Успіх",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                clearForm();


                if (_parentWindow is MainWindow main)
                {
                    main.loadFlowers();
                    Debug.WriteLine("reload");
                }
                Debug.WriteLine(_parentWindow);


            }
            else
            {

                MessageBox.Show(
                    "Ми не змогли зберегти запис",
                    "Невдача",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void clearForm()
        {
            FormCategory.SelectedIndex = -1;
            FormName.Text = "";
            FormDescription.Text = "";
            FormCount.Text = "";
            FormPrice.Text = "";
            FormType.Text = "";
            FormColor.Text = "";
        }
    }
}
