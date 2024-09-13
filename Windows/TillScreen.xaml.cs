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
using TextBox = System.Windows.Controls.TextBox;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Windows.Themes;
using System.Drawing;

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


        public string propertyButtonTheme;
        public string propertyButtonSelectedTheme;
        public string tillNavigationButtonTheme;
        public string tillNavigationManageCloseButtonTheme;
        public string tillNumpadTheme;
        public string tillQuickTheme;
        public string keyboardKeyTheme;
        public string keyboardNumpadTheme;
        public string scrViewTheme;
        public string rectangleBackgroundTheme;
        public string loginThemeBackground;
        public string adminButtonTheme;
        public string adminButtonImageTheme;
        public string emptyButtonTheme;
        public string adminButtonSelected;
        public string keyboardBackground;
        public string labelBoxTheme;
        public string labelBoxTimeColor;
        public string txtBoxLabelTheme;
        public string txtBoxSelectLabelTheme;
        public string loginTimeColor;
        public string borderTheme;
        public string comboBoxTheme;
        public string toggleTheme;
        public string errorBoxTheme;
        public string labelTheme;


        public TillScreen()
        {
            themeController = new ThemeController();
            themeController.currentThemeName = "Red";
            InitializeComponent();
            this.DataContext = this;
            SetTill();
        }

        public void SetTill()
        {
            ClearStartStrings();
            SetTillFiles();
            SetProductButtonDetails();
            ChangeTheme();
        }
        public void ChangeTheme()
        {
            themeController.CreateThemeFile();
            themeController.SetThemeWindow();
            themeController.ReadTheme();
            themeController.SetTheme();
            GetTheme();
            TillLogOn();
        }

        public void GetTheme()
        {

            propertyButtonTheme = themeController.currentButtonProperty;
            propertyButtonSelectedTheme = themeController.currentButtonPropertySelected;
            tillNavigationButtonTheme = themeController.currentButtonTillNavigation;
            tillNavigationManageCloseButtonTheme = themeController.currentButtonTillNavigationManageClose;
            tillNumpadTheme = themeController.currentButtonTillNumpad;
            tillQuickTheme = themeController.currentButtonTillQuick;
            keyboardKeyTheme = themeController.currentKeyboardKeys;
            keyboardNumpadTheme = themeController.currentKeyboardNumpad;
            keyboardBackground = themeController.currentKeyboardBackground;
            rectangleBackgroundTheme = themeController.currentRectangleBackgroundTheme;
            scrViewTheme = themeController.currentScrollViewTheme;
            labelBoxTheme = themeController.currentLabelDisplayBackgroundTheme;
            loginThemeBackground = themeController.currentLoginBackgroundFileName;
            adminButtonImageTheme = themeController.currentButtonAdminImageTheme;
            emptyButtonTheme = themeController.currentButtonEmptyProduct;
            txtBoxLabelTheme = themeController.currentTextBoxDisplayTheme;
            loginTimeColor = themeController.currentLabelLoginScreenTimeColor;
            borderTheme = themeController.currentBorderTheme;
            comboBoxTheme = themeController.currentComboBoxToggleTheme;
            errorBoxTheme = themeController.currentTextBoxDisplayThemeError;
            txtBoxSelectLabelTheme = themeController.currentTextBoxDisplayThemeSelected;
            toggleTheme = themeController.currentToggleTheme;
            labelTheme = themeController.currentTextBlockTheme;
            
        }


        private void TillLogOn()
        {
            lblSaleScreenDate.Dispatcher.InvokeAsync(GetDateTime, DispatcherPriority.Normal);
            LogInScreen logInScreen = new LogInScreen();
            foreach (UIElement button in logInScreen.AdminNumbers.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button loginButton = (Button)button;
                    loginButton.Style = (Style)Application.Current.Resources[tillNumpadTheme];
                }
            }
            logInScreen.bgAdminHeader.Style = (Style)Application.Current.Resources[labelBoxTheme];
            var brushConverter = new BrushConverter();
            var colorBrush = (Brush)brushConverter.ConvertFrom(loginTimeColor);
            logInScreen.lblLoginDate.Foreground = colorBrush;
            logInScreen.lblLoginTime.Foreground = colorBrush;
            logInScreen.lblToBe18Date.Foreground = colorBrush;
            
            
            logInScreen.imgTillBackground.Source = new BitmapImage(new Uri(@loginThemeBackground, UriKind.Relative));
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
                ProductDetails productEdit = new ProductDetails();
                foreach (UIElement button in productEdit.panelKeybooardButtons.Children)
                {
                    if (button.GetType() == typeof(Button))
                    {
                        Button keyboardButton = (Button)button;
                        keyboardButton.Style = (Style)Application.Current.Resources[keyboardKeyTheme];
                    }
                }
                foreach (UIElement button in productEdit.panelNumpad.Children)
                {
                    if (button.GetType() == typeof(Button))
                    {
                        Button keyboardButton = (Button)button;
                        keyboardButton.Style = (Style)Application.Current.Resources[keyboardNumpadTheme];
                    }
                }
                productEdit.rctKeyboard.Style = (Style)Application.Current.Resources[keyboardBackground];
                productEdit.rctNumpad.Style = (Style)Application.Current.Resources[keyboardBackground];
                productEdit.rctProductBorder.Style = (Style)Application.Current.Resources[rectangleBackgroundTheme];
                productEdit.scrlButtonColorSelect.Style = (Style)Application.Current.Resources[scrViewTheme];
                productEdit.scrlButtonForegroundColorSelect.Style = (Style)Application.Current.Resources[scrViewTheme];
                productEdit.brdButtonColor.Style = (Style)Application.Current.Resources[borderTheme];
                productEdit.brdButtonForegroundColor.Style = (Style)Application.Current.Resources[borderTheme];
                productEdit.brdPageBreak1.Style = (Style)Application.Current.Resources[borderTheme];
                productEdit.brdButtonForegroundColorSelectBase.Style = (Style)Application.Current.Resources[rectangleBackgroundTheme];
                productEdit.brdButtonColorSelectBase.Style = (Style)Application.Current.Resources[rectangleBackgroundTheme];
                foreach (UIElement button in productEdit.grProductPanel.Children)
                {
                    if (button.GetType() == typeof(Button))
                    {
                        Button UIbutton = (Button)button;
                        if (UIbutton.Name != productEdit.btnColorView.Name)
                        {
                            UIbutton.Style = (Style)Application.Current.Resources[propertyButtonTheme];
                        }
                    }
                }
                foreach (UIElement textBox in productEdit.grProductPanel.Children)
                {
                    if (textBox.GetType() == typeof(TextBox))
                    {
                        TextBox txtBox = (TextBox)textBox;
                        txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelTheme];
                    }
                }
                foreach (UIElement txtBlock in productEdit.grProductPanel.Children)
                {
                    if (txtBlock.GetType() == typeof(TextBlock))
                    {
                        TextBlock manageStaffTextBlock = (TextBlock)txtBlock;
                        manageStaffTextBlock.Style = (Style)Application.Current.Resources[labelTheme];
                    }
                }
                productEdit.Owner = Application.Current.MainWindow;
                productEdit.WindowStartupLocation = WindowStartupLocation.Manual;
                productEdit.Left = 0;
                productEdit.Top = 80;
                if (btnPressedProduct.Content != string.Empty)
                {
                    productEdit.boolIsEditing = true;
                    var readDataName = workSheet.Cell(intXlsxProductRow, strXlsxProductColumn).GetValue<string>();
                    var readDataPrice = workSheet.Cell(intXlsxProductRow, strXlsxPriceColumn).GetValue<string>();
                    var readDataTheme = workSheet.Cell(intXlsxProductRow, strXlsxButtonThemeColumn).GetValue<string>();
                    var readDataFoeground = workSheet.Cell(intXlsxProductRow, strXlsxButtonForegroundColumn).GetValue<string>();
                    productEdit.strEditProductName = readDataName;
                    productEdit.strEditProductPrice = readDataPrice;
                    productEdit.strEditButtonTheme = readDataTheme;
                    productEdit.strEditButtonForeground = readDataFoeground;
                    productEdit.txtEnterProductName.Text = Convert.ToString(btnPressedProduct.Content);
                    productEdit.txtEnterProductPrice.Text = readDataPrice;
                    productEdit.btnColorView.Style = (Style)Application.Current.Resources[readDataTheme];
                    productEdit.btnDelete.IsEnabled = true;
                    SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(readDataFoeground);
                    productEdit.btnColorView.Foreground = colorBrush;
                }
                else
                {
                    productEdit.btnDelete.IsEnabled = false;
                }
                productEdit.btnDelete.Click += (sender, e) => { productEdit.Close(); productEdit.boolIsEditing = false; };
                productEdit.btnOk.Click += (sender, e) => { productEdit.Close(); productEdit.boolIsEditing = false; };
                productEdit.btnCancel.Click += (sender, e) => { productEdit.Close(); productEdit.boolIsEditing = false; };
                productEdit.ShowDialog();
                if (productEdit.strProductName != string.Empty)
                {
                    btnPressedProduct.Content = productEdit.strProductName;
                    btnPressedProduct.Style = (Style)Application.Current.Resources[productEdit.strButtonTheme];
                    SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(productEdit.strButtonForeground);
                    btnPressedProduct.Foreground = colorBrush;
                }
                else
                {
                    btnPressedProduct.Content = string.Empty;
                    btnPressedProduct.Style = (Style)Application.Current.Resources[emptyButtonTheme];
                }
                workSheet.Cell(intXlsxProductRow, strXlsxProductColumn).Value = productEdit.strProductName;
                workSheet.Cell(intXlsxProductRow, strXlsxPriceColumn).Value = productEdit.strProductPrice;
                workSheet.Cell(intXlsxProductRow, strXlsxButtonThemeColumn).Value = productEdit.strButtonTheme;
                workSheet.Cell(intXlsxProductRow, strXlsxButtonForegroundColumn).Value = productEdit.strButtonForeground;
                //workSheet.Cell(intXlsxProductRow, strXlsxButtonTypeColumn).Value = "button type"; //productEdit.strButtonType;
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
                ManagerWindow openManageWindow = new ManagerWindow();
                foreach (UIElement button in openManageWindow.grManageButtons.Children)
                {
                    if (button.GetType() == typeof(Button))
                    {
                        Button manageButton = (Button)button;
                        if (manageButton.Name != "btnManageClose")
                        {
                            manageButton.Style = (Style)Application.Current.Resources[tillNavigationButtonTheme];
                        }
                        else
                        {
                            manageButton.Style = (Style)Application.Current.Resources[tillNavigationManageCloseButtonTheme];
                        }
                    }
                }
                openManageWindow.rctManageWindow.Style = (Style)Application.Current.Resources[keyboardBackground];
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
                openManageWindow.btnManageEditStaff.Click += (sender, e) => { openManageWindow.Close(); ManageEditStaff(); };
                openManageWindow.btnManageAdmin.Click += (sender, e) => { openManageWindow.Close(); EditTillProperties(); };
                openManageWindow.ShowDialog();
            }
            //else show pop up can not

        }
        private void EditTillProperties()
        {
            TillPropertiesWindow TillPropertiesWindow = new TillPropertiesWindow();
            TillPropertiesWindow.Owner = Application.Current.MainWindow;
            TillPropertiesWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            TillPropertiesWindow.Left = 212;
            TillPropertiesWindow.Top = 127;
            TillPropertiesWindow.btnTillPropertiesWindowClose.Click += (sender, e) => { TillPropertiesWindow.Close(); };
            TillPropertiesWindow.btnApply.Click += (sender, e) => { TillPropertiesWindow.Close(); ChangeTheme(); };
            TillPropertiesWindow.btnCancel.Click += (sender, e) => { TillPropertiesWindow.Close(); };

            foreach (UIElement txtBlock in TillPropertiesWindow.grdProperty.Children)
            {
                if (txtBlock.GetType() == typeof(TextBlock))
                {
                    TextBlock propertyTextBlock = (TextBlock)txtBlock;
                    propertyTextBlock.Style = (Style)Application.Current.Resources[labelTheme];
                }
            }
            foreach (UIElement button in TillPropertiesWindow.grdProperty.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button propertyButton = (Button)button;
                    propertyButton.Style = (Style)Application.Current.Resources[propertyButtonTheme];
                }
            }
            foreach (UIElement imgButton in TillPropertiesWindow.panelBackgroundSelect.Children)
            {
                if (imgButton.GetType() == typeof(Button))
                {
                    Button propertyImageButton = (Button)imgButton;
                    propertyImageButton.Style = (Style)Application.Current.Resources[adminButtonImageTheme];
                }
            }
            //change to foreach when more are made
            TillPropertiesWindow.rctPropertyWindow1.Style = (Style)Application.Current.Resources[rectangleBackgroundTheme];
            TillPropertiesWindow.brdPageBreak1.Style = (Style)Application.Current.Resources[borderTheme];
            TillPropertiesWindow.brdPageBreak2.Style = (Style)Application.Current.Resources[borderTheme];
            TillPropertiesWindow.brdBackgroundScroll.Style = (Style)Application.Current.Resources[borderTheme];
            TillPropertiesWindow.scrlThemeSelect.Style = (Style)Application.Current.Resources[scrViewTheme];
            //for each
            TillPropertiesWindow.boxSelectTheme.Style = (Style)Application.Current.Resources[comboBoxTheme];
            TillPropertiesWindow.boxSelectLoginTimeColor.Style = (Style)Application.Current.Resources[comboBoxTheme];
            TillPropertiesWindow.ShowDialog();
        }

        private void ManageEditStaff()
        {
            StaffDetails openEditStaff = new StaffDetails();
            foreach (UIElement button in openEditStaff.panelKeybooardButtons.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button keyboardButton = (Button)button;
                    keyboardButton.Style = (Style)Application.Current.Resources[keyboardKeyTheme];
                }
            }
            foreach (UIElement button in openEditStaff.panelNumpad.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button keyboardButton = (Button)button;
                    keyboardButton.Style = (Style)Application.Current.Resources[keyboardNumpadTheme];
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
            foreach (UIElement txtBlock in openEditStaff.grStaffWindow.Children)
            {
                if (txtBlock.GetType() == typeof(TextBlock))
                {
                    TextBlock manageStaffTextBlock = (TextBlock)txtBlock;
                    manageStaffTextBlock.Style = (Style)Application.Current.Resources[labelTheme];
                }
            }
            foreach (UIElement button in openEditStaff.grStaffWindow.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button staffButton = (Button)button;
                    staffButton.Style = (Style)Application.Current.Resources[propertyButtonTheme];
                }
            }
            openEditStaff.btnFilterStaff1.Style = (Style)Application.Current.Resources[propertyButtonSelectedTheme];
            openEditStaff.rctStaffScreen.Style = (Style)Application.Current.Resources[rectangleBackgroundTheme];
            openEditStaff.rctKeyboard.Style = (Style)Application.Current.Resources[keyboardBackground];
            openEditStaff.rctNumpad.Style = (Style)Application.Current.Resources[keyboardBackground];
            openEditStaff.brdScrollWindow.Style = (Style)Application.Current.Resources[borderTheme];
            openEditStaff.scrlSelectExistingStaff.Style = (Style)Application.Current.Resources[scrViewTheme];
            openEditStaff.boxSelectRole.Style = (Style)Application.Current.Resources[comboBoxTheme];

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
                                //btnName.Style = (Style)Application.Current.Resources["btnDefaultItemTheme"];
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