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
    /// Interaction logic for sellersPage.xaml
    /// </summary>
    public partial class sellersPage : Page
    {
        StoreManagementSystemEntities db = new StoreManagementSystemEntities();
        public sellersPage()
        {
            InitializeComponent();
            dataGrid.ItemsSource = db.sellers.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            seller seller = new seller();
            try
            {
                if (txtId.Text != "" && txtName.Text != "" && txtAge.Text != "" && txtMobileNo.Text != "" && txtPassword.Text != "")
                {
                    if (txtMobileNo.Text.Length >= 11 && txtPassword.Text.Length >= 8)
                    {
                        seller.Sell_ID = txtId.Text;
                        seller.Sell_Name = txtName.Text;
                        seller.Sell_no = txtMobileNo.Text;
                        seller.Sell_Age = txtAge.Text;
                        seller.Passwords = txtPassword.Text;
                        db.sellers.Add(seller);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.sellers.ToList();
                    }
                    else
                    {
                        MessageBox.Show("The Password isnt more than 8, OR the mobileno isn't more than 11");
                    }
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            seller seller = new seller();
            try
            {
                if (txtId.Text != "" && (txtName.Text != "" || txtAge.Text != "" || txtMobileNo.Text != "" || txtPassword.Text != ""))
                {
                    if (txtMobileNo.Text.Length >= 11 && txtPassword.Text.Length >= 8)
                    {
                        seller = db.sellers.First(x => x.Sell_ID == txtId.Text);
                        seller.Sell_Name = txtName.Text;
                        seller.Sell_no = txtMobileNo.Text;
                        seller.Sell_Age = txtAge.Text;
                        seller.Passwords = txtPassword.Text;
                        db.sellers.AddOrUpdate(seller);
                        db.SaveChanges();
                        dataGrid.ItemsSource = db.sellers.ToList();
                    }
                    else
                    {
                        MessageBox.Show("The Password isnt more than 8, OR the mobileno isn't more than 11");
                    }
                }
                else
                {
                    MessageBox.Show("Please, Enter Data");
                }
            }
            catch
            {
                MessageBox.Show("this ID ISn't Exist ");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            seller seller = new seller();
            try
            {
                if (txtId.Text != "" && (txtName.Text != "" || txtAge.Text != "" || txtMobileNo.Text != "" || txtPassword.Text != ""))
                {
                    seller = db.sellers.First(x => x.Sell_ID == txtId.Text);
                    db.sellers.Remove(seller);
                    db.SaveChanges();
                    dataGrid.ItemsSource = db.sellers.ToList();
                }
                else
                {
                    MessageBox.Show("Please, Enter The ID");
                }
            }
            catch
            {
                MessageBox.Show("This ID ISn't Exist ");
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            StorePage store = new StorePage();
            this.NavigationService.Navigate(store);
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductsPage products = new ProductsPage();
            this.NavigationService.Navigate(products);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
