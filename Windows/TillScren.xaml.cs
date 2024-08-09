using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewTillSystem
{
    public partial class TillScren : Window
    {
        private string strEmpty = "";
        private string strSaleSelect;

        public TillScren()
        {
            InitializeComponent();
            ClearStartStrings();
        }

        private void ClearStartStrings()
        {
            lblAdminNumAmount.Text = string.Empty;

            for (int i = 1; i <= 15; i++)
            {
                string btnRow = "btnRow" + i;
                string lbl1Row = "lbl1Row" + i;
                string lbl2Row = "lbl2Row" + i;
                foreach (UIElement item in grSaleScreen.Children)
                {
                    if (item.GetType() == typeof(ToggleButton))
                    {
                        ToggleButton togButton = (ToggleButton)item;
                        if (togButton.Name == btnRow)
                        {
                            togButton.Content = strEmpty;
                        }
                    }
                    if (item.GetType() == typeof(TextBlock))
                    {
                        TextBlock txtBlock = (TextBlock)item;
                        if (txtBlock.Name == lbl1Row)
                        {
                            txtBlock.Text = strEmpty;
                        }
                        if (txtBlock.Name == lbl2Row)
                        {
                            txtBlock.Text = strEmpty;
                        }
                    }
                }
            }
        }
        private void SaleScreenButtonCheck()
        {
            for (int i = 1; i <= 15; i++)
            {
                string btnRow = "btnRow" + i;
                foreach (UIElement item in grSaleScreen.Children)
                {
                    if (item.GetType() == typeof(ToggleButton))
                    {
                        ToggleButton togButton = (ToggleButton)item;
                        if (togButton.Name == strSaleSelect)
                        {
                            togButton.IsChecked = true;
                        }
                        else if (togButton.Name != strSaleSelect)
                        {
                            togButton.IsChecked = false;
                        }
                    }
                }
            }
        }

        private void saleScreenRow1_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow1.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow2_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow2.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow3_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow3.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow4_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow4.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow5_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow5.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow6_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow6.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow7_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow7.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow8_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow8.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow9_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow9.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow10_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow10.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow11_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow11.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow12_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow12.Name);
            SaleScreenButtonCheck();
        }
        private void saleScreenRow13_Click(object sender, RoutedEventArgs e)
        {
            strSaleSelect = Convert.ToString(btnRow13.Name);
            SaleScreenButtonCheck();
        }
    }
}