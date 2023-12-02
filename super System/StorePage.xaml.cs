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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace super_System
{
    /// <summary>
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {
        StoreManagementSystemEntities db = new StoreManagementSystemEntities();
        public StorePage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            seller seller1 = new seller();  
            Admin admin = new Admin();
            try
            {
                if (combo.Text != "")
                {
                    
                    if (txtPassword.Text!="" && txtUserName.Text != "")
                    {
                        var item = combo.SelectedItem.ToString().Split(' ').Last();
                        if (item == "User")
                        {
                            seller1 = db.sellers.First(x=> x.Sell_Name == txtUserName.Text && x.Passwords == txtPassword.Text);
                            SellerUser seller = new SellerUser();
                            this.NavigationService.Navigate(seller);
                        }
                        else
                        {
                            admin = db.Admins.First(x => x.UserNames == txtUserName.Text && x.Passwords == txtPassword.Text);
                            ProductsPage products = new ProductsPage();
                            this.NavigationService.Navigate(products);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please, Enter Data");
                    }
                }
                else
                {
                    MessageBox.Show("Please, Select a Role");
                }
            }
            catch
            {
                MessageBox.Show("This User Name, or ID doesn't Exist" );
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            combo.Text = " ";
            txtPassword.Text = " ";
            txtUserName.Text = " ";
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
