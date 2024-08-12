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
        public string strStaffLoginName;
        public string strStaffLastName;
        public bool boolStaffLogin;
        public bool boolLoggedInManager;
        public bool boolLoggedInAdmin;
        public bool boolLoggedInStaff;
        public string strStaffRole;
        private string strUserNumberInput;
        private string strEmpty = "";

        private string strXlsxStaffName = "A";
        private string strXlsxStaffLastName = "B";
        private string strXlsxStaffID = "C";
        private string strXlsxStaffRole = "D";
        private int intStaffAmount;

        public LogInScreen()
        {
            InitializeComponent();
            ClearLabelStrings();
            boolStaffLogin = false;
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
                var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\StaffID.xlsx");
                var workSheet = workBook.Worksheet("Staff");
                intStaffAmount = workSheet.LastRowUsed().RowNumber();
                for (int i = 1; i <= intStaffAmount; i++)
                {
                    var readDataStaffName = workSheet.Cell(i, strXlsxStaffName).GetValue<string>();
                    var readDataStaffLastName = workSheet.Cell(i, strXlsxStaffLastName).GetValue<string>();
                    var readDataStaffID = workSheet.Cell(i, strXlsxStaffID).GetValue<string>();
                    var readDataStaffRole = workSheet.Cell(i, strXlsxStaffRole).GetValue<string>();
                    if (strUserNumberInput == readDataStaffID)
                    {
                        strStaffLoginName = readDataStaffName;
                        strStaffLastName = readDataStaffLastName;
                        strStaffRole = readDataStaffRole;
                        boolStaffLogin = true;
                        if (readDataStaffRole == "ADMIN")
                        {
                            boolLoggedInAdmin = true;
                        }
                        else if(readDataStaffRole == "MANAGER")
                        {
                            boolLoggedInManager = true;
                        }
                        else if (readDataStaffRole == "STAFF")
                        {
                            boolLoggedInStaff = true;
                        }
                        break;
                    }
                }
                workBook.Save();
                if (boolStaffLogin)
                {
                    Close();
                }
                else
                {
                    boolStaffLogin = false;
                    boolLoggedInAdmin = false;
                    boolLoggedInManager = false;
                    boolLoggedInStaff = false;
                    lblAdminNumAmount.Text = "Try Again";
                    strUserNumberInput = strEmpty;
                }
            }
        }


        private void ClearLabelStrings()
        {
            lblAdminNumAmount.Text = string.Empty;
        }
    }
}
