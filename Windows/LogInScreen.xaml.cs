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
using System.Windows.Threading;

namespace NewTillSystem.Windows
{
    public partial class LogInScreen : Window
    {
        private string currentDate;
        private string currentTime;
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

            lblLoginDate.Dispatcher.InvokeAsync(GetDateTime, DispatcherPriority.SystemIdle);
        }

        private void GetDateTime()
        {
            currentDate = DateTime.Now.ToString("dd/mm/yy");
            currentTime = DateTime.Now.ToString("h:mm tt");

            lblLoginDate.Text = currentDate;
            lblLoginTime.Text = currentTime;
            lblLoginDate.Dispatcher.InvokeAsync(GetDateTime, DispatcherPriority.SystemIdle);
        }

        private void btnAdminNumPad_Click(object sender, RoutedEventArgs e)
        {
            Button getButtonClicked = (Button)sender;
            var strButtonClicked = getButtonClicked.Name;
            int numPadBtnAmount = 9;

            for (int i = 0; i <= numPadBtnAmount; i++)
            {
                var strButtonName = "btnAdminNum" + i;
                if (strButtonClicked == strButtonName)
                {
                    strUserNumberInput += i;
                    PrintUserInput();
                }
            }
        }

        private void PrintUserInput()
        {
            if (strUserNumberInput.Length <= 4)
            {
                lblAdminNumAmount.Text = strUserNumberInput;
            }
            if (strUserNumberInput.Length == 4)
            {
                var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\StaffID.xlsx");
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
                        else if (readDataStaffRole == "MANAGER")
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
