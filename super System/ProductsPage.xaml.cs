using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace super_System
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        StoreManagementSystemEntities db = new StoreManagementSystemEntities();
        public ProductsPage()
        {
            InitializeComponent();
            dataGrid.ItemsSource= db.Products.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Product products = new Product();
            try
            {
                if (txtId.Text != "" && txtName.Text != "" && txtPrice.Text != "" && txtQuantity.Text != "" && combo.Text != "")
                {
                    var item = combo.SelectedItem.ToString().Split(':').Last().Trim();
                    products.Prd_Id = txtId.Text;
                    products.Prd_Price = txtPrice.Text;
                    products.Prd_Quantity = txtQuantity.Text;
                    products.Prd_Names = txtName.Text;
                    products.Prd_catg = item;
                    db.Products.Add(products);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.Products.ToList();
                }
                else
                {
                    MessageBox.Show("Please, Enter Data");
                }
            }
            catch
            {
                MessageBox.Show("this ID IS Generated");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Product products = new Product();
            try
            {
                if (txtId.Text != "" && (txtName.Text != "" || txtPrice.Text != "" || txtQuantity.Text != "" || combo.Text != ""))
                {
                    var item = combo.SelectedItem.ToString().Split(':').Last().Trim();
                    products = db.Products.First(x=> x.Prd_Id == txtId.Text);
                    products.Prd_Price = txtPrice.Text;
                    products.Prd_Quantity = txtQuantity.Text;
                    products.Prd_Names = txtName.Text;
                    products.Prd_catg = item;
                    db.Products.AddOrUpdate(products);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.Products.ToList();
                }
                else
                {
                    MessageBox.Show("Please, Enter Data");
                }
            }
            catch
            {
                MessageBox.Show("This ID is not Exist");
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Product products = new Product();
            try
            {
                if (txtId.Text != "")
                {
                    products = db.Products.First(x => x.Prd_Id == txtId.Text);
                    db.Products.Remove(products);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.Products.ToList();
                }
                else
                {
                    MessageBox.Show("Please, Enter The ID");
                }
            }
            catch
            {
                MessageBox.Show("This ID is not Exist");
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            StorePage store = new StorePage();
            this.NavigationService.Navigate(store);
        }

        private void btnSellers_Click(object sender, RoutedEventArgs e)
        {
            sellersPage sellers = new sellersPage();
            this.NavigationService.Navigate(sellers);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = " ";
            txtId.Text = " ";
            txtPrice.Text = " ";
            txtQuantity.Text = " ";
            combo.Text = " ";
            combo2.Text = " ";
            dataGrid.ItemsSource = db.Products.ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (combo2.Text != "")
            {
                var item = combo2.SelectedItem.ToString().Split(' ').Last();
                dataGrid.ItemsSource = db.Products.Where(x => x.Prd_catg == item).ToList();
            }
            else
            {
                MessageBox.Show("Choose Category");
            }
        }
    }
}
