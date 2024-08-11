using NewTillSystem.Windows;
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
using ClosedXML.Excel;
using System.IO;

namespace NewTillSystem
{
    public partial class TillScreen : Window
    {

        private string strEmpty = "";
        private string strUserNumberInput;
        private string strUserNumberConvert;
        private int intUserNumberInput;
        private int intProductButtonCount = 51;
        private bool boolIsMinus;

        private bool boolCanEditProduct;
        private bool boolEditStaff;

        private int intXlsxProductRow;
        private string strXlsxProductColumn = "A";
        private string strXlsxPriceColumn = "B";
        private string strXlsxStaffNameColumn = "A";
        private string strXlsxStaffLastNameColumn = "B";
        private string strXlsxStaffIDColumn = "C";
        private string strXlsxStaffRoleColumn = "D";
        private string strXlsxLoggedInStaffRole;
        private string strXlxsLoggedInStaffName;


        public TillScreen()
        {
            InitializeComponent();
            this.DataContext = this;
            ClearStartStrings();
            SetTillFiles();
            SetProductButtonDetails();
            TillLogOn();
        }

        private void TillLogOn()
        {
            LogInScreen logInScreen = new LogInScreen();
            logInScreen.ShowDialog();
            if (!logInScreen.boolStaffLogin)
            {
                Close();
            }
            strXlxsLoggedInStaffName = logInScreen.strStaffLoginName;
            strXlsxLoggedInStaffRole = logInScreen.strStaffRole;
            lblSaleScreenStaff.Text = strXlxsLoggedInStaffName;
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

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            Button btnPressedProduct = (Button)sender;
            var strPressedProduct = Convert.ToString(btnPressedProduct.Name);
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\ProductDataBase.xlsx");
            var workSheet = workBook.Worksheet("Product Sheet");
            if (boolCanEditProduct)
            {
                for (int i = 1; i <= intProductButtonCount; i++)
                {
                    var strBtnName = "btnProduct" + i;
                    if (strBtnName == strPressedProduct)
                    {
                        intXlsxProductRow = i;
                    }
                }
                EnterProductDetails openPrompt = new EnterProductDetails();
                openPrompt.Owner = Application.Current.MainWindow;
                openPrompt.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                openPrompt.btnOk.Click += (sender, e) => { openPrompt.Close(); };
                openPrompt.btnCancel.Click += (sender, e) => { openPrompt.Close(); };
                openPrompt.txtEnterProductName.Focus();
                openPrompt.ShowDialog();
                if (openPrompt.strProductName != string.Empty)
                {
                    btnPressedProduct.Content = openPrompt.strProductName;
                    btnPressedProduct.Style = (Style)Application.Current.Resources["btnItemStyle"];
                }
                else
                {
                    btnPressedProduct.Content = string.Empty;
                    btnPressedProduct.Style = (Style)Application.Current.Resources["btnEmptyStyle"];
                }
                workSheet.Cell(intXlsxProductRow, strXlsxProductColumn).Value = openPrompt.strProductName;
                workSheet.Cell(intXlsxProductRow, strXlsxPriceColumn).Value = openPrompt.strProductPrice;
                workBook.Save();
            }
        }

        private void btnAdminManage_Click(object sender, RoutedEventArgs e)
        {
            //if manager role can do
            //else show pop up can not
            ManageTillWindow openManageWindow = new ManageTillWindow();
            openManageWindow.Owner = Application.Current.MainWindow;
            openManageWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            openManageWindow.btnManageClose.Click += (sender, e) => { openManageWindow.Close(); };
            openManageWindow.btnManageEditProducts.Click += (sender, e) => { openManageWindow.Close(); boolCanEditProduct = true; };
            openManageWindow.btnManageEditStaff.Click += (sender, e) => { openManageWindow.Close(); ManageEditStaff(); };
            openManageWindow.ShowDialog();
            
        }
        private void ManageEditStaff()
        {
            EnterStaffDetails openEditStaff = new EnterStaffDetails();
            openEditStaff.Owner = Application.Current.MainWindow;
            openEditStaff.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //check details
            //openEditStaff.btnOk.Click += (sender, e) => { openEditStaff.Close(); }; // save entered details
                //if newstaff then close and save details
                //if edit and pressed. save new details
            openEditStaff.btnCancel.Click += (sender, e) => { openEditStaff.Close(); }; // save nothing
            openEditStaff.ShowDialog();

        }

        private void SetProductButtonDetails()
        {
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\ProductDataBase.xlsx");
            var workSheet = workBook.Worksheet("Product Sheet");
            for (int i = 1; i <= intProductButtonCount; i++)
            {
                var readData = workSheet.Cell(i, strXlsxProductColumn).GetValue<string>();
                var strBtnName = "btnProduct" + i;
                foreach (UIElement item in grBtnScreen.Children)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btnName = (Button)item;
                        if (btnName.Name == strBtnName)
                        {
                            btnName.Content = readData;
                            if (btnName.Content != string.Empty)
                            {
                                btnName.Style = (Style)Application.Current.Resources["btnItemStyle"];
                            }
                        }
                    }
                }
            }
            workBook.Save();
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

        private void SetTillFiles()
        {
            if (!File.Exists("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\StaffID.xlsx"))
            {
                var workBook = new XLWorkbook();
                var workSheet = workBook.Worksheets.Add("Staff");
                string setAdminName = "ADMIN";
                string setAdminLastName = "ADMIN";
                string setAdminID = "1111";
                string setAdminRole = "ADMIN";
                workSheet.Cell(1, strXlsxStaffNameColumn).Value = setAdminName;
                workSheet.Cell(1, strXlsxStaffLastNameColumn).Value = setAdminLastName;
                workSheet.Cell(1, strXlsxStaffIDColumn).Value = setAdminID;
                workSheet.Cell(1, strXlsxStaffRoleColumn).Value = setAdminRole;
                workBook.SaveAs("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\StaffID.xlsx");
            }
            if (!File.Exists("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\ProductDataBase.xlsx"))
            {
                var workBook = new XLWorkbook();
                var workSheet = workBook.Worksheets.Add("Product Sheet");
                workBook.SaveAs("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\ProductDataBase.xlsx");
            }
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