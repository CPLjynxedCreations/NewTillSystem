using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml;
using NewTillSystem.Resources.Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TextBox = System.Windows.Controls.TextBox;

namespace NewTillSystem.Windows
{
    public partial class StaffDetails : Window
    {
        ThemeController themeController;
        TillScreen tillScreen;

        private string strXlsxStaffNameColumn = "A";
        private string strXlsxStaffLastNameColumn = "B";
        private string strXlsxStaffIDColumn = "C";
        private string strXlsxStaffRoleColumn = "D";
        private string strAdmin = "ADMIN";
        private string strManager = "MANAGER";
        private string strStaff = "STAFF";
        private string strEmpty = "";
        private string strSetStaffFirstName = string.Empty;
        private string strSetStaffLastName = string.Empty;
        private string strSetStaffID = string.Empty;
        private string strSetStaffRole = string.Empty;
        private string strSetNewStaffFirstName = string.Empty;
        private string strSetNewStaffLastName = string.Empty;
        private string strSetNewStaffID = string.Empty;
        private string strSetNewStaffRole = string.Empty;
        private string strTglStaffName = string.Empty;

        private string strFilterActive = "OFF";
        private bool boolFilterActive = false;

        private bool boolIsToDeleteStaff;
        private bool boolIsToEditStaff;
        private bool boolStaffNameMatch;
        private bool boolStaffIdMatch;
        private bool boolSaveStaff;
        private bool boolAddStaff;
        private bool boolStaffAdded;
        private bool boolGenerateStaff;


        private string propertyButtonStaff = string.Empty;
        private string propertyButtonSelectdStaff = string.Empty;


        private string errorBoxThemeStaff = string.Empty;
        private string txtBoxLabelThemeStaff = string.Empty;
        private string txtBoxLabelSelectedThemeStaff = string.Empty;
        private string toggleThemeStaff = string.Empty;

        public StaffDetails()
        {
            InitializeComponent();
            themeController = new ThemeController();
            tillScreen = (TillScreen)Application.Current.MainWindow;



            toggleThemeStaff = tillScreen.toggleTheme;
            errorBoxThemeStaff = tillScreen.errorBoxTheme;
            txtBoxLabelThemeStaff = tillScreen.txtBoxLabelTheme;
            txtBoxLabelSelectedThemeStaff = tillScreen.txtBoxSelectLabelTheme;

            propertyButtonStaff = tillScreen.propertyButtonTheme;
            propertyButtonSelectdStaff = tillScreen.propertyButtonSelectedTheme;
            
            boolGenerateStaff = true;
            GetCurrentStaffList();
            btnKeyboard_SPACE.IsEnabled = false;
            panelKeybooardButtons.IsEnabled = false;
            panelNumpad.IsEnabled = false;
        }


        private void GetCurrentStaffList()
        {
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\StaffID.xlsx");
            var workSheet = workBook.Worksheet("Staff");
            int intStaffAmount = workSheet.LastRowUsed().RowNumber();


            var range = workSheet.Range(strXlsxStaffNameColumn + 1, strXlsxStaffRoleColumn + workSheet.RowsUsed().Count());

            for (int i = 2; i <= intStaffAmount; i++)
            {
                var readXlsxDataStaffName = workSheet.Cell(i, strXlsxStaffNameColumn).GetValue<string>();
                var readXlsxDataStaffLastName = workSheet.Cell(i, strXlsxStaffLastNameColumn).GetValue<string>();
                var readXlsxDataStaffID = workSheet.Cell(i, strXlsxStaffIDColumn).GetValue<string>();
                var readXlsxDataStaffRole = workSheet.Cell(i, strXlsxStaffRoleColumn).GetValue<string>();
                var joinNames = readXlsxDataStaffName + " " + readXlsxDataStaffLastName;

                if (boolGenerateStaff)
                {
                    ToggleButton tglStaff = new ToggleButton();
                    tglStaff.Name = readXlsxDataStaffName + readXlsxDataStaffLastName;
                    tglStaff.Content = readXlsxDataStaffName + " " + readXlsxDataStaffLastName;
                    tglStaff.Style = (Style)Application.Current.Resources[toggleThemeStaff];
                    tglStaff.Checked += tglStaff_Checked;
                    if (tglStaff.Content.ToString() != strEmpty)
                    {
                        if (boolFilterActive)
                        {
                            for (int j = 0; j <= intStaffAmount; j++)
                            {
                                foreach (UIElement tglRemove in panelExistingStaff.Children)
                                {
                                    if (tglRemove.GetType() == typeof(ToggleButton))
                                    {
                                        ToggleButton strTglStaffName = (ToggleButton)tglRemove;
                                        if (strTglStaffName.Name == readXlsxDataStaffName + readXlsxDataStaffLastName)
                                        {
                                            panelExistingStaff.Children.Remove(strTglStaffName);
                                            break;
                                        }
                                    }
                                }
                            }
                            if (strFilterActive == readXlsxDataStaffRole)
                            {
                                panelExistingStaff.Children.Add(tglStaff);
                            }
                        }
                        if (strFilterActive == "OFF")
                        {
                            for (int j = 0; j <= intStaffAmount; j++)
                            {
                                foreach (UIElement tglRemove in panelExistingStaff.Children)
                                {
                                    if (tglRemove.GetType() == typeof(ToggleButton))
                                    {
                                        ToggleButton strTglStaffName = (ToggleButton)tglRemove;
                                        if (strTglStaffName.Name == readXlsxDataStaffName + readXlsxDataStaffLastName)
                                        {
                                            panelExistingStaff.Children.Remove(strTglStaffName);
                                            break;
                                        }
                                    }
                                }
                            }
                            panelExistingStaff.Children.Add(tglStaff);
                        }
                    }
                    if (i == intStaffAmount)
                    {
                        boolGenerateStaff = false;
                    }
                }
                if (boolIsToEditStaff)
                {
                    if (strTglStaffName == joinNames)
                    {
                        string[] strSeperateNames = joinNames.Split(' ');
                        strSetStaffFirstName = strSeperateNames[0];
                        strSetStaffLastName = strSeperateNames[1];
                        strSetStaffID = readXlsxDataStaffID;
                        strSetStaffRole = readXlsxDataStaffRole;
                        if (boolIsToDeleteStaff)
                        {
                            workSheet.Row(i).Delete();
                            workBook.Save();
                            for (int j = 0; j <= intStaffAmount; j++)
                            {
                                foreach (UIElement tglDelete in panelExistingStaff.Children)
                                {
                                    if (tglDelete.GetType() == typeof(ToggleButton))
                                    {
                                        ToggleButton strTglStaffName = (ToggleButton)tglDelete;
                                        string strNameCheck = strSetStaffFirstName + strSetStaffLastName;
                                        if (strTglStaffName.Name == strNameCheck)
                                        {
                                            panelExistingStaff.Children.Remove(strTglStaffName);
                                            intStaffAmount = intStaffAmount - 1;
                                            boolIsToEditStaff = false;
                                            boolIsToDeleteStaff = false;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (boolAddStaff)
            {
                NameCheck();
                IDCheck();
                if (strSetNewStaffFirstName == strEmpty)
                {
                    txtEnterStaffFirstName.Style = (Style)Application.Current.Resources[errorBoxThemeStaff];
                    boolStaffNameMatch = true;
                }
                if (strSetNewStaffLastName == strEmpty)
                {
                    txtEnterStaffLastName.Style = (Style)Application.Current.Resources[errorBoxThemeStaff];
                    boolStaffNameMatch = true;
                }
                if (strSetNewStaffID == strEmpty)
                {
                    txtEnterStaffPin.Style = (Style)Application.Current.Resources[errorBoxThemeStaff];
                    boolStaffIdMatch = true;
                }
                if (strSetNewStaffFirstName == strEmpty || strSetNewStaffLastName == strEmpty || strSetNewStaffID == strEmpty)
                {
                    boolSaveStaff = false;
                    this.Close();
                }
                if (boolStaffNameMatch == false && boolStaffIdMatch == false)
                {
                    boolSaveStaff = true;
                }
            }
            if (boolSaveStaff)
            {
                int intGetNewStaffRow = workSheet.LastRowUsed().RowNumber() + 1;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffNameColumn).Value = strSetNewStaffFirstName;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffLastNameColumn).Value = strSetNewStaffLastName;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffIDColumn).Value = strSetNewStaffID;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffRoleColumn).Value = strSetNewStaffRole;
                boolStaffAdded = true;
                boolSaveStaff = false;
            }

            range = workSheet.Range(strXlsxStaffNameColumn + 1, strXlsxStaffRoleColumn + workSheet.RowsUsed().Count());
            range.SetAutoFilter().Sort(2, XLSortOrder.Ascending);

            workSheet.ColumnsUsed().AdjustToContents();
            workBook.Save();
        }

        private void NameCheck()
        {
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\StaffID.xlsx");
            var workSheet = workBook.Worksheet("Staff");
            int intStaffAmount = workSheet.LastRowUsed().RowNumber();


            var range = workSheet.Range(strXlsxStaffNameColumn + 1, strXlsxStaffRoleColumn + workSheet.RowsUsed().Count());

            for (int i = 2; i <= intStaffAmount; i++)
            {
                var readXlsxDataStaffName = workSheet.Cell(i, strXlsxStaffNameColumn).GetValue<string>();
                var readXlsxDataStaffLastName = workSheet.Cell(i, strXlsxStaffLastNameColumn).GetValue<string>();
                if (strSetNewStaffFirstName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName)
                {
                    txtEnterStaffFirstName.Style = (Style)Application.Current.Resources[errorBoxThemeStaff];
                    txtEnterStaffLastName.Style = (Style)Application.Current.Resources[errorBoxThemeStaff];
                    boolStaffNameMatch = true;
                    return;
                }
                else
                {
                    txtEnterStaffFirstName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                    txtEnterStaffLastName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                    boolStaffNameMatch = false;
                }
            }
        }
        private void IDCheck()
        {
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\XLSX\\StaffID.xlsx");
            var workSheet = workBook.Worksheet("Staff");
            int intStaffAmount = workSheet.LastRowUsed().RowNumber();
            var range = workSheet.Range(strXlsxStaffNameColumn + 1, strXlsxStaffRoleColumn + workSheet.RowsUsed().Count());

            for (int i = 2; i <= intStaffAmount; i++)
            {
                var readXlsxDataStaffID = workSheet.Cell(i, strXlsxStaffIDColumn).GetValue<string>();
                if (strSetNewStaffID == readXlsxDataStaffID)
                {
                    txtEnterStaffPin.Style = (Style)Application.Current.Resources[errorBoxThemeStaff];
                    boolStaffIdMatch = true;
                    return;
                }
                else
                {
                    txtEnterStaffPin.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                    boolStaffNameMatch = false;
                }
            }
        }

        private void tglStaff_Checked(object sender, EventArgs e)
        {
            ToggleButton tglStaff = (ToggleButton)sender;
            strTglStaffName = Convert.ToString(tglStaff.Content);
            string strSelectedStaff = tglStaff.Name;
            int intStaffCount = panelExistingStaff.Children.Count;
            foreach (UIElement tglStaffName in panelExistingStaff.Children)
            {
                if (tglStaffName.GetType() == typeof(ToggleButton))
                {
                    ToggleButton tglSelectedStaff = (ToggleButton)tglStaffName;
                    if (tglSelectedStaff.Name == strSelectedStaff)
                    {
                        tglSelectedStaff.IsChecked = true;
                    }
                    else
                    {
                        tglSelectedStaff.IsChecked = false;
                    }
                }
            }
        }

        private void btnFilterStaff_Click(object sender, EventArgs e)
        {
            Button btnFilter = (Button)sender;
            strFilterActive = Convert.ToString(btnFilter.Content);
            if (btnFilterStaff1.Content.ToString() == strFilterActive)
            {
                boolGenerateStaff = true;
                boolFilterActive = false;
                GetCurrentStaffList();
                btnFilterStaff1.Style = (Style)Application.Current.Resources[propertyButtonSelectdStaff];
            }
            else
            {
                btnFilterStaff1.Style = (Style)Application.Current.Resources[propertyButtonStaff];
            }
            if (btnFilterStaff2.Content.ToString() == strFilterActive)
            {
                boolFilterActive = true;
                boolGenerateStaff = true;
                GetCurrentStaffList();
                btnFilterStaff2.Style = (Style)Application.Current.Resources[propertyButtonSelectdStaff];
            }
            else
            {
                btnFilterStaff2.Style = (Style)Application.Current.Resources[propertyButtonStaff];
            }
            if (btnFilterStaff3.Content.ToString() == strFilterActive)
            {
                boolFilterActive = true;
                boolGenerateStaff = true;
                GetCurrentStaffList();
                btnFilterStaff3.Style = (Style)Application.Current.Resources[propertyButtonSelectdStaff];
            }
            else
            {
                btnFilterStaff3.Style = (Style)Application.Current.Resources[propertyButtonStaff];
            }
            if (btnFilterStaff4.Content.ToString() == strFilterActive)
            {
                boolFilterActive = true;
                boolGenerateStaff = true;
                GetCurrentStaffList();
                btnFilterStaff4.Style = (Style)Application.Current.Resources[propertyButtonSelectdStaff];
            }
            else
            {
                btnFilterStaff4.Style = (Style)Application.Current.Resources[propertyButtonStaff];
            }
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            boolIsToDeleteStaff = true;
            boolIsToEditStaff = true;
            GetCurrentStaffList();
        }

        private void btnEditStaff_Click(object sender, RoutedEventArgs e)
        {
            boolIsToEditStaff = true;
            boolIsToDeleteStaff = true;
            GetCurrentStaffList();

            txtEnterStaffFirstName.Text = strSetStaffFirstName;
            txtEnterStaffLastName.Text = strSetStaffLastName;
            txtEnterStaffPin.Text = strSetStaffID;
            if (strSetStaffRole == strAdmin)
            {
                boxSelectRole.SelectedItem = boxRoleSelectAdmin;
            }
            else if (strSetStaffRole == strManager)
            {
                boxSelectRole.SelectedItem = boxRoleSelectManager;
            }
            else if (strSetStaffRole == strStaff)
            {
                boxSelectRole.SelectedItem = boxRoleSelectStaff;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!boolIsToEditStaff)
            {
                boolAddStaff = true;
                strSetNewStaffFirstName = txtEnterStaffFirstName.Text.ToUpper();
                strSetNewStaffLastName = txtEnterStaffLastName.Text.ToUpper();
                strSetNewStaffID = txtEnterStaffPin.Text;
                strSetNewStaffRole = boxSelectRole.Text.ToUpper();

                if (boxRoleSelectNameText.IsSelected)
                {
                    strSetNewStaffRole = strStaff;
                }
                GetCurrentStaffList();
                if (boolStaffAdded)
                {
                    boolStaffAdded = false;
                    this.Close();
                }
            }
            if (txtEnterStaffFirstName.Text == strEmpty && txtEnterStaffLastName.Text == strEmpty && txtEnterStaffPin.Text == strEmpty)
            {
                this.Close();
            }
        }

        private void btnKeyboardClick_Click(object sender, RoutedEventArgs e)
        {
            Button btnClicked = (Button)sender;
            string strLetterPressed = Convert.ToString(btnClicked.Content);
            for (int i = 0; i <= grStaffWindow.Children.Count; i++)
            {
                foreach (UIElement item in grStaffWindow.Children)
                {
                    if (item.GetType() == typeof(TextBox))
                    {
                        TextBox txtBox = (TextBox)item;
                        if (txtBox.IsFocused)
                        {
                            if (btnKeyboard_DELETE.Content.ToString() != strLetterPressed)
                            {
                                txtBox.Text = txtBox.Text + strLetterPressed;
                                txtBox.CaretIndex = txtBox.Text.Length;
                                return;
                            }
                            else
                            {
                                strLetterPressed = txtBox.Text;
                                strLetterPressed = strLetterPressed.Remove(strLetterPressed.Length - 1);
                                txtBox.Text = strLetterPressed;
                                txtBox.CaretIndex = txtBox.Text.Length;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void CheckTxtBoxFocus(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i <= grStaffWindow.Children.Count; i++)
            {
                foreach (UIElement item in grStaffWindow.Children)
                {
                    if (item.GetType() == typeof(TextBox))
                    {
                        TextBox txtBox = (TextBox)item;
                        if (txtBox.IsFocused)
                        {

                            // GET RESOURCE FROM DICTIONARY TO FIX
                            //txtBox.BorderBrush = Brushes.Firebrick;
                            if (txtBox.Name == txtEnterStaffPin.Name)
                            {
                                txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelSelectedThemeStaff];
                                txtEnterStaffFirstName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                                txtEnterStaffLastName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                                panelNumpad.IsEnabled = true;
                                panelKeybooardButtons.IsEnabled = false;
                                Keyboard.ClearFocus();
                                return;

                            }
                            else if (txtBox.Name == txtEnterStaffFirstName.Name)
                            {
                                txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelSelectedThemeStaff];
                                txtEnterStaffPin.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                                txtEnterStaffLastName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                                panelKeybooardButtons.IsEnabled = true;
                                panelNumpad.IsEnabled = false;
                                Keyboard.ClearFocus();
                                return;
                            }
                            else if (txtBox.Name == txtEnterStaffLastName.Name)
                            {
                                txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelSelectedThemeStaff];
                                txtEnterStaffPin.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                                txtEnterStaffFirstName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                                panelKeybooardButtons.IsEnabled = true;
                                panelNumpad.IsEnabled = false;
                                Keyboard.ClearFocus();
                                return;
                            }


                            for (int j = 0; j <= panelExistingStaff.Children.Count; j++)
                            {
                                foreach (UIElement btnStack in panelExistingStaff.Children)
                                {
                                    if (btnStack.GetType() == typeof(ToggleButton))
                                    {
                                        ToggleButton tglButton = (ToggleButton)btnStack;
                                        tglButton.IsChecked = false;
                                        strTglStaffName = strEmpty;
                                    }
                                }
                            }
                        }
                        else
                        {
                            txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelThemeStaff];
                            panelNumpad.IsEnabled = false;
                            panelKeybooardButtons.IsEnabled = false;
                        }
                    }
                }
            }

        }
    }
}
