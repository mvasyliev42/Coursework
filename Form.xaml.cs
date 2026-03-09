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

            FormCategory.ItemsSource = _categoryService.getList();
            

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
                FormCategory.SelectedValue = _categoryService.getList().FirstOrDefault(c => c.Name == flower.CategoryName)?.Id;
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
                categoryService.name = FormCategory.Text;
                categoryService.save();
                categoryId = categoryService.inserted_id;
            }
            else
            {
                categoryId = (int)FormCategory.SelectedValue;
            }

            bool status = false;



            flowerService.CategoryId = categoryId;
            flowerService.Name = FormName.Text;
            flowerService.Description = FormDescription.Text;
            flowerService.Count = int.Parse(FormCount.Text);
            flowerService.Price = float.Parse(FormPrice.Text);
            flowerService.Type = FormType.Text;
            flowerService.Color = FormColor.Text;

            if (_flower.Id == null)
            {
                status = flowerService.save();
            }
            else
            {
                status = flowerService.update(_flower.Id);
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
