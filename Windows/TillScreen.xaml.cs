﻿using NewTillSystem.Windows;
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
using ClosedXML;

namespace NewTillSystem
{
    public partial class TillScreen : Window
    {
        private string strEmpty = "";
        private string strUserNumberInput;
        private string strUserNumberConvert;
        private int intUserNumberInput;
        private bool boolIsMinus;

        private bool boolCanEditProduct;


        public TillScreen()
        {
            InitializeComponent();
            ClearStartStrings();
            boolCanEditProduct = true;
            TillLogOn();
        }

        private void TillLogOn()
        {
            LogInScreen logInScreen = new LogInScreen();
            logInScreen.ShowDialog();
        }
        #region NUMBER PAD
        private void btnAdminNum0_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "0";
            PrintUserInput();
        }
        private void btnAdminNum1_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "1";
            PrintUserInput();
        }
        private void btnAdminNum2_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "2";
            PrintUserInput();
        }
        private void btnAdminNum3_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "3";
            PrintUserInput();
        }
        private void btnAdminNum4_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "4";
            PrintUserInput();
        }
        private void btnAdminNum5_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "5";
            PrintUserInput();
        }
        private void btnAdminNum6_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "6";
            PrintUserInput();
        }
        private void btnAdminNum7_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "7";
            PrintUserInput();
        }
        private void btnAdminNum8_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "8";
            PrintUserInput();
        }
        private void btnAdminNum9_Click(object sender, RoutedEventArgs e)
        {
            strUserNumberInput += "9";
            PrintUserInput();
        }
        private void btnAdminNumMinus_Click(object sender, RoutedEventArgs e)
        {
            if (!boolIsMinus)
            {
                boolIsMinus = true;
                if (intUserNumberInput > 0)
                {
                    intUserNumberInput = intUserNumberInput * -1;
                    PrintUserInput();
                }
                else
                {
                    lblAdminNumAmount.Text = "-";
                }
            }
            else
            {
                boolIsMinus = false;
                PrintUserInput();
            }
        }
        #endregion

        private void btnProduct_Click (object sender, RoutedEventArgs e)
        {
            Button btnPressedProduct = (Button)sender;
            var strPressedProduct = Convert.ToString(btnPressedProduct.Name);

            if (boolCanEditProduct)
            {
                //get button content
                //if content text box will = content
                EnterProductDetails openPrompt = new EnterProductDetails();
                openPrompt.Owner = Application.Current.MainWindow;
                openPrompt.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                openPrompt.btnOk.Click += (sender, e) => { openPrompt.Close(); };
                openPrompt.btnCancel.Click += (sender, e) => { openPrompt.Close(); };
                openPrompt.txtEnterProductName.Focus();
                openPrompt.ShowDialog();
                //set the name and price of the item from product window
            }
            // create bool for edit
            //if edit bool
            // ADD PRODUCT
            //var name = "btnProduct";
            //int rowNumber;
            //for loop
            //var searchName = name + i;
            //if searchName = strPressedProduct
            //rowNumber = i;
            
            //BTNPressedProduct.Content = "fuck"; // textbox.text
            
            //print to excel

            //do stuff with button clicked
            //add product(); // sell product();
            //Debug.WriteLine(btnPressedProduct);
        }


        private void PrintUserInput()
        {
            intUserNumberInput = Convert.ToInt32(strUserNumberInput);
            if (boolIsMinus)
            {
                intUserNumberInput = intUserNumberInput * -1;
            }
            strUserNumberConvert = Convert.ToString(intUserNumberInput);
            lblAdminNumAmount.Text = strUserNumberConvert;
        }

        private void ClearStartStrings()
        {
            lblAdminNumAmount.Text = strEmpty;

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
    }
}