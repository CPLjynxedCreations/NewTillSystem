using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace NewTillSystem.Windows
{
    public partial class LogInScreen : Window
    {
        private string strUserNumberInput;
        private string strEmpty = "";
        private string strNathan = "1234";
        private string strAlana = "4321";
        public string strStaffLogin;

        private int intStaffAmount = 10;

        public LogInScreen()
        {
            InitializeComponent();
            ClearLabelStrings();
        }

        #region numbers
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
        #endregion

        private void PrintUserInput()
        {
            if (strUserNumberInput.Length <= 4)
            {
                lblAdminNumAmount.Text = strUserNumberInput;
            }
            if (strUserNumberInput.Length == 4)
            {
                for (int i = 1; i <= intStaffAmount; i++)
                {
                    
                    if (strUserNumberInput == strAlana)
                    {
                        strStaffLogin = "Alana";
                        Close();
                    }
                    if (strUserNumberInput == strNathan)
                    {
                        strStaffLogin = "Nathan";
                        Close();
                    }
                    
                    else
                    {
                        lblAdminNumAmount.Text = "Try Again";
                        strUserNumberInput = strEmpty;
                    }
                }
            }
        }

        private void ClearLabelStrings()
        {
            lblAdminNumAmount.Text = string.Empty;
        }
    }
}
