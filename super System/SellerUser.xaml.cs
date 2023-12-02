using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace super_System
{
    /// <summary>
    /// Interaction logic for SellerUser.xaml
    /// </summary>
    public partial class SellerUser : Page
    {
        StoreManagementSystemEntities db = new StoreManagementSystemEntities();
        public SellerUser()
        {
            InitializeComponent();
            dataGrid2.ItemsSource = db.Products.Select(x=> new {x.Prd_Names , x.Prd_Quantity}).ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            StorePage store = new StorePage();
            this.NavigationService.Navigate(store);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (combo.Text != "")
            {
                var item = combo.SelectedItem.ToString().Split(' ').Last();
                dataGrid2.ItemsSource = db.Products.Where(x=> x.Prd_catg == item).Select(x => new { x.Prd_Names, x.Prd_Quantity }).ToList();
            }
            else
            {
                MessageBox.Show("Choose Category");
            }
        }

        private void btnSearch_Click1(object sender, RoutedEventArgs e)
        {
            combo.Text = " ";
            dataGrid2.ItemsSource = db.Products.Select(x => new { x.Prd_Names, x.Prd_Quantity }).ToList();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDataToTextDocument();
        }
        private void PrintDataToTextDocument()
        {
            try
            {
                // Get the data from the DataGrid
                var data = (dataGrid2.ItemsSource as System.Collections.IEnumerable)
                    .Cast<object>()
                    .Select(row =>
                        string.Join("\t", (dataGrid2.Columns.Cast<DataGridTextColumn>()
                            .Select(column =>
                            {
                                var binding = (column.Binding as System.Windows.Data.Binding);
                                var property = binding?.Path.Path;
                                var cellValue = row.GetType().GetProperty(property)?.GetValue(row, null);
                                return cellValue?.ToString() ?? "";
                            }))))
                    .ToArray();

                // Create a StringBuilder to store the data
                StringBuilder sb = new StringBuilder();
                foreach (var line in data)
                {
                    sb.AppendLine(line);
                }

                // Specify the path for the text document on the desktop
                string desktopPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataOutput.txt");

                // Write the data to the text document
                File.WriteAllText(desktopPath, sb.ToString());

                MessageBox.Show("Data printed to DataOutput.txt on the desktop.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
