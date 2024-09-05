using NewTillSystem.Windows;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;
using NewTillSystem.Resources.Scripts;
using Microsoft.VisualBasic;

namespace NewTillSystem
{
    public partial class TillScreen : Window
    {
        ThemeController themeController;
        private string strEmpty = "";
        private string strUserNumberInput;
        private string strUserNumberConvert;
        private int intUserNumberInput;
        private int intProductButtonCount = 51;
        private bool boolIsMinus;


        private int intXlsxProductRow;
        private string strXlsxProductColumn = "A";
        private string strXlsxPriceColumn = "B";
        private string strXlsxButtonThemeColumn = "C";
        private string strXlsxButtonForegroundColumn = "D";
        private string strXlsxButtonTypeColumn = "E";

        private string strXlsxStaffNameColumn = "A";
        private string strXlsxStaffLastNameColumn = "B";
        private string strXlsxStaffIDColumn = "C";
        private string strXlsxStaffRoleColumn = "D";
        private string strXlsxStaffInitColumn = "E";
        private string strXlsxLoggedInStaffRole;
        private string strXlsxLoggedInStaffName;

        private bool boolIsAdmin;
        private bool boolIsManager;
        private bool boolIsStaff;
        private bool boolCanEditProduct;
        private bool boolEditStaff;



        public string adminButtonTheme;
        public string quickButtonTheme;
        public string labelBoxTheme;
        public string txtBoxLabelTheme;
        public string rectangleTheme;



        public TillScreen()
        {
            themeController = new ThemeController();
            InitializeComponent();
            this.DataContext = this;
            ClearStartStrings();
            SetTillFiles();
            SetProductButtonDetails();

            themeController.CreateThemeFile();
            themeController.SetThemeWindow();
            themeController.ReadTheme();
            themeController.SetTheme();
            TillLogOn();
        }

        private void TillLogOn()
        {
            lblSaleScreenDate.Dispatcher.InvokeAsync(GetDateTime, DispatcherPriority.Normal);
            LogInScreen logInScreen = new LogInScreen();
            foreach(UIElement button in logInScreen.AdminNumbers.Children)
            {
                if(button.GetType() == typeof(Button))
                {
                    Button loginButton = (Button)button;
                    loginButton.Style = (Style)Application.Current.Resources[quickButtonTheme];
                }
            }
            logInScreen.bgAdminHeader.Style = (Style)Application.Current.Resources[labelBoxTheme];
            logInScreen.ShowDialog();
            if (!logInScreen.boolStaffLogin)
            {
                Close();
            }

            strXlsxLoggedInStaffName = logInScreen.strStaffLoginName;
            strXlsxLoggedInStaffRole = logInScreen.strStaffRole;
            boolIsAdmin = logInScreen.boolLoggedInAdmin;
            boolIsManager = logInScreen.boolLoggedInManager;
            boolIsStaff = logInScreen.boolLoggedInStaff;
            lblSaleScreenStaff.Text = strXlsxLoggedInStaffName;
        }

        private void GetDateTime()
        {
            string currentDate = DateTime.Now.ToString("dd/MM/yy");
            string currentTime = DateTime.Now.ToString("h:mm tt");

            lblSaleScreenDate.Text = currentDate;
            lblSaleScreenTime.Text = currentTime;
        }

        #region NUMBER PAD

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
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\ProductDataBase.xlsx");
            var workSheet = workBook.Worksheet("Product Sheet");
            if (boolCanEditProduct)
            {
                for (int i = 1; i <= intProductButtonCount; i++)
                {
                    var strBtnName = "btnProduct" + i;
                    if (strBtnName == strPressedProduct)
                    {
                        intXlsxProductRow = i + 1;
                    }
                }
                EnterProductDetails openPrompt = new EnterProductDetails();
                foreach (UIElement button in openPrompt.panelKeybooardButtons.Children)
                {
                    if (button.GetType() == typeof(Button))
                    {
                        Button keyboardButton = (Button)button;
                        keyboardButton.Style = (Style)Application.Current.Resources[adminButtonTheme];
                    }
                }
                foreach (UIElement button in openPrompt.panelNumpad.Children)
                {
                    if (button.GetType() == typeof(Button))
                    {
                        Button keyboardButton = (Button)button;
                        keyboardButton.Style = (Style)Application.Current.Resources[quickButtonTheme];
                    }
                }
                openPrompt.rctKeyboard.Style = (Style)Application.Current.Resources[labelBoxTheme];
                openPrompt.rctNumpad.Style = (Style)Application.Current.Resources[labelBoxTheme];
                openPrompt.Owner = Application.Current.MainWindow;
                openPrompt.WindowStartupLocation = WindowStartupLocation.Manual;
                openPrompt.Left = 0;
                openPrompt.Top = 80;
                if (btnPressedProduct.Content != string.Empty)
                {
                    openPrompt.boolIsEditing = true;
                    var readDataName = workSheet.Cell(intXlsxProductRow, strXlsxProductColumn).GetValue<string>();
                    var readDataPrice = workSheet.Cell(intXlsxProductRow, strXlsxPriceColumn).GetValue<string>();
                    var readDataTheme = workSheet.Cell(intXlsxProductRow, strXlsxButtonThemeColumn).GetValue<string>();
                    var readDataFoeground = workSheet.Cell(intXlsxProductRow, strXlsxButtonForegroundColumn).GetValue<string>();
                    openPrompt.strEditProductName = readDataName;
                    openPrompt.strEditProductPrice = readDataPrice;
                    openPrompt.strEditButtonTheme = readDataTheme;
                    openPrompt.strEditButtonForeground = readDataFoeground;
                    openPrompt.txtEnterProductName.Text = Convert.ToString(btnPressedProduct.Content);
                    openPrompt.txtEnterProductPrice.Text = readDataPrice;
                    openPrompt.btnColorView.Style = (Style)Application.Current.Resources[readDataTheme];
                    openPrompt.btnDelete.IsEnabled = true;
                    SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(readDataFoeground);
                    openPrompt.btnColorView.Foreground = colorBrush;
                }
                else
                {
                    openPrompt.btnDelete.IsEnabled = false;
                }
                openPrompt.btnDelete.Click += (sender, e) => { openPrompt.Close(); openPrompt.boolIsEditing = false; };
                openPrompt.btnOk.Click += (sender, e) => { openPrompt.Close(); openPrompt.boolIsEditing = false; };
                openPrompt.btnCancel.Click += (sender, e) => { openPrompt.Close(); openPrompt.boolIsEditing = false; };
                openPrompt.ShowDialog();
                if (openPrompt.strProductName != string.Empty)
                {
                    btnPressedProduct.Content = openPrompt.strProductName;
                    btnPressedProduct.Style = (Style)Application.Current.Resources[openPrompt.strButtonTheme];
                    SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(openPrompt.strButtonForeground);
                    btnPressedProduct.Foreground = colorBrush;
                }
                else
                {
                    btnPressedProduct.Content = string.Empty;
                    btnPressedProduct.Style = (Style)Application.Current.Resources["DefaultButtonEmptyTheme"];
                }
                workSheet.Cell(intXlsxProductRow, strXlsxProductColumn).Value = openPrompt.strProductName;
                workSheet.Cell(intXlsxProductRow, strXlsxPriceColumn).Value = openPrompt.strProductPrice;
                workSheet.Cell(intXlsxProductRow, strXlsxButtonThemeColumn).Value = openPrompt.strButtonTheme;
                workSheet.Cell(intXlsxProductRow, strXlsxButtonForegroundColumn).Value = openPrompt.strButtonForeground;
                //workSheet.Cell(intXlsxProductRow, strXlsxButtonTypeColumn).Value = "button type"; //openPrompt.strButtonType;
                workSheet.ColumnsUsed().AdjustToContents();
                workBook.Save();
            }
            //ADD TO SALE SCREEN WHEN MADE
        }

        private void btnAdminLogOut_Click(object sender, EventArgs e)
        {
            boolIsAdmin = false;
            boolIsManager = false;
            boolIsStaff = false;
            TillLogOn();
        }

        private void btnAdminManage_Click(object sender, RoutedEventArgs e)
        {
            if (boolIsManager || boolIsAdmin)
            {
                ManageTillWindow openManageWindow = new ManageTillWindow();
                foreach (UIElement button in openManageWindow.grManageButtons.Children)
                {
                    if (button.GetType() == typeof(Button))
                    {
                        Button manageButton = (Button)button;
                        manageButton.Style = (Style)Application.Current.Resources[adminButtonTheme];
                    }
                }
                openManageWindow.rctManageWindow.Style = (Style)Application.Current.Resources[labelBoxTheme];
                openManageWindow.Owner = Application.Current.MainWindow;
                openManageWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (boolCanEditProduct)
                {
                    openManageWindow.btnManageEditProducts.Content = "STOP PRODUCT EDIT";
                }
                openManageWindow.btnManageClose.Click += (sender, e) => { openManageWindow.Close(); };
                openManageWindow.btnManageEditProducts.Click += (sender, e) =>
                {
                    if (!boolCanEditProduct)
                    {
                        boolCanEditProduct = true;
                    }
                    else
                    {
                        boolCanEditProduct = false;
                    }
                    openManageWindow.Close();
                };
                //MAKE BUTTON TO STOP EDIT POP UP
                openManageWindow.btnManageEditStaff.Click += (sender, e) => { openManageWindow.Close(); ManageEditStaff(); };
                openManageWindow.btnManageAdmin.Click += (sender, e) => { openManageWindow.Close(); EditTillProperties(); };
                openManageWindow.ShowDialog();
            }
            //else show pop up can not

        }
        private void EditTillProperties()
        {
            WindowTillProperties customButtonColor = new WindowTillProperties();
            customButtonColor.Owner = Application.Current.MainWindow;
            customButtonColor.WindowStartupLocation = WindowStartupLocation.Manual;
            customButtonColor.Left = 212;
            customButtonColor.Top = 127;
            //customButtonColor.Topmost = true;
            customButtonColor.btnWindowTillPropertiesClose.Click += (sender, e) => { customButtonColor.Close(); };
            customButtonColor.ShowDialog();
        }
        private void ManageEditStaff()
        {
            EnterStaffDetails openEditStaff = new EnterStaffDetails();


            foreach (UIElement button in openEditStaff.panelKeybooardButtons.Children)
            {
                if(button.GetType() == typeof(Button))
                {
                    Button keyboardButton = (Button)button;
                    keyboardButton.Style = (Style)Application.Current.Resources[adminButtonTheme];
                }
            }
            foreach (UIElement button in openEditStaff.panelNumpad.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button keyboardButton = (Button)button;
                    keyboardButton.Style = (Style)Application.Current.Resources[quickButtonTheme];
                }
            }
            foreach (UIElement txtBox in openEditStaff.grStaffWindow.Children)
            {
                if (txtBox.GetType() == typeof(TextBox))
                {
                    TextBox manageStaffTextBox = (TextBox)txtBox;
                    manageStaffTextBox.Style = (Style)Application.Current.Resources[txtBoxLabelTheme];
                }
            }
            openEditStaff.rctStaffScreen.Style = (Style)Application.Current.Resources[rectangleTheme];
            openEditStaff.rctKeyboard.Style = (Style)Application.Current.Resources[labelBoxTheme];
            openEditStaff.rctNumpad.Style = (Style)Application.Current.Resources[labelBoxTheme];

            openEditStaff.Owner = Application.Current.MainWindow;
            openEditStaff.WindowStartupLocation = WindowStartupLocation.Manual;
            openEditStaff.Left = 0;
            openEditStaff.Top = 80;
            openEditStaff.btnCancel.Click += (sender, e) => { openEditStaff.Close(); };
            openEditStaff.ShowDialog();

        }

        private void btnAdminNumClear_Click(object sender, RoutedEventArgs e)
        {
            lblAdminNumAmount.Text = strEmpty;
            strUserNumberInput = strEmpty;
            intUserNumberInput = 0;
            Debug.WriteLine(intUserNumberInput);
        }

        private void SetProductButtonDetails()
        {
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\ProductDataBase.xlsx");
            var workSheet = workBook.Worksheet("Product Sheet");
            for (int i = 2; i <= intProductButtonCount; i++)
            {
                var readDataName = workSheet.Cell(i, strXlsxProductColumn).GetValue<string>();
                var readDataTheme = workSheet.Cell(i, strXlsxButtonThemeColumn).GetValue<string>();
                var readDataFoeground = workSheet.Cell(i, strXlsxButtonForegroundColumn).GetValue<string>();
                int intButtonNumber = i - 1;
                var strBtnName = "btnProduct" + intButtonNumber;
                foreach (UIElement item in grBtnScreen.Children)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btnName = (Button)item;
                        if (btnName.Name == strBtnName)
                        {
                            btnName.Content = readDataName;
                            if (btnName.Content != string.Empty)
                            {
                                btnName.Style = (Style)Application.Current.Resources["btnDefaultItemTheme"];
                                btnName.Style = (Style)Application.Current.Resources[readDataTheme];
                                SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(readDataFoeground);
                                btnName.Foreground = colorBrush;
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
            if (!File.Exists("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\StaffID.xlsx"))
            {
                var workBook = new XLWorkbook();
                var workSheet = workBook.Worksheets.Add("Staff");
                string setAdminName = "ADMIN";
                string setAdminLastName = "ADMIN";
                string setAdminID = "1111";
                string setAdminRole = "ADMIN";
                string setFirstNameHeader = "FIRST NAME";
                string setLastNameHeader = "LAST NAME";
                string setCodeHeader = "CODE";
                string setRoleHeader = "ROLE";
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cell(1, strXlsxStaffNameColumn).Value = setFirstNameHeader;
                workSheet.Cell(1, strXlsxStaffLastNameColumn).Value = setLastNameHeader;
                workSheet.Cell(1, strXlsxStaffIDColumn).Value = setCodeHeader;
                workSheet.Cell(1, strXlsxStaffRoleColumn).Value = setRoleHeader;
                workSheet.Cell(2, strXlsxStaffNameColumn).Value = setAdminName;
                workSheet.Cell(2, strXlsxStaffLastNameColumn).Value = setAdminLastName;
                workSheet.Cell(2, strXlsxStaffIDColumn).Value = setAdminID;
                workSheet.Cell(2, strXlsxStaffRoleColumn).Value = setAdminRole;
                workSheet.ColumnsUsed().AdjustToContents();

                var range = workSheet.Range(strXlsxStaffNameColumn + 1, strXlsxStaffRoleColumn + workSheet.RowsUsed().Count());
                range.SetAutoFilter().Sort(2, XLSortOrder.Ascending);

                workBook.SaveAs("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\StaffID.xlsx");
            }
            if (!File.Exists("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\ProductDataBase.xlsx"))
            {
                var workBook = new XLWorkbook();
                var workSheet = workBook.Worksheets.Add("Product Sheet");
                string setProductHeader = "PRODUCT";
                string setPriceHeader = "PRICE";
                string setThemeHeader = "THEME";
                string setThemeForeground = "FOREGROUND";
                string setButtonType = "BUTTON TYPE";
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cell(1, strXlsxProductColumn).Value = setProductHeader;
                workSheet.Cell(1, strXlsxPriceColumn).Value = setPriceHeader;
                workSheet.Cell(1, strXlsxButtonThemeColumn).Value = setThemeHeader;
                workSheet.Cell(1, strXlsxButtonForegroundColumn).Value = setThemeForeground;
                workSheet.Cell(1, strXlsxButtonTypeColumn).Value = setButtonType;
                workSheet.ColumnsUsed().AdjustToContents();
                workBook.SaveAs("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\ProductDataBase.xlsx");
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