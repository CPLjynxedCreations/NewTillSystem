using ClosedXML.Excel;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NewTillSystem.Windows
{
    public partial class EnterStaffDetails : Window
    {
        private onScreenKeyboardController kbController;
        private string strXlsxStaffNameColumn = "A";
        private string strXlsxStaffLastNameColumn = "B";
        private string strXlsxStaffIDColumn = "C";
        private string strXlsxStaffRoleColumn = "D";
        private string strAdmin = "ADMIN";
        private string strManager = "MANAGER";
        private string strStaff = "STAFF";
        private string strEmpty = "";
        private string strSetStaffFirstName;
        private string strSetStaffLastName;
        private string strSetStaffID;
        private string strSetStaffRole;
        private string strSetNewStaffFirstName;
        private string strSetNewStaffLastName;
        private string strSetNewStaffID;
        private string strSetNewStaffRole;
        private string strTglStaffName;

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

        public EnterStaffDetails()
        {
            InitializeComponent();
            kbController = new onScreenKeyboardController();
            boolGenerateStaff = true;
            btnFilterStaff1.Foreground = Brushes.White;
            GetCurrentStaffList();
            txtEnterStaffFirstName.Focus();
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
                    tglStaff.Style = (Style)Application.Current.Resources["tglStyleStaff"];
                    tglStaff.Checked += tglStaff_Checked;
                    if (tglStaff.Content != strEmpty)
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
                if (boolAddStaff)
                {
                    if (strSetNewStaffFirstName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName || strSetNewStaffID == readXlsxDataStaffID)
                    {
                        if (strSetNewStaffFirstName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName)
                        {
                            txtEnterStaffFirstName.Background = Brushes.Red;
                            txtEnterStaffLastName.Background = Brushes.Red;
                            boolStaffNameMatch = true;
                        }
                        else
                        {
                            txtEnterStaffFirstName.Background = Brushes.DarkSeaGreen;
                            txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                            boolStaffNameMatch = false;
                        }
                        if (strSetNewStaffID == readXlsxDataStaffID)
                        {
                            txtEnterStaffPin.Background = Brushes.Red;
                            boolStaffIdMatch = true;
                        }
                        else
                        {
                            txtEnterStaffPin.Background = Brushes.DarkSeaGreen;
                            boolStaffIdMatch = false;
                        }
                        break;
                    }
                    if (strSetNewStaffFirstName == strEmpty)
                    {
                        txtEnterStaffFirstName.Background = Brushes.Red;
                        boolStaffNameMatch = true;
                    }
                    else
                    {
                        txtEnterStaffFirstName.Background = Brushes.DarkSeaGreen;
                        //boolStaffNameMatch = false;
                    }
                    if (strSetNewStaffLastName == strEmpty)
                    {
                        txtEnterStaffLastName.Background = Brushes.Red;
                        boolStaffNameMatch = true;
                    }
                    else
                    {
                        txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                    }
                    if(strSetNewStaffID == strEmpty)
                    {
                        txtEnterStaffPin.Background = Brushes.Red;
                        boolStaffIdMatch = true;
                    }
                    else
                    {
                        txtEnterStaffPin.Background = Brushes.DarkSeaGreen;
                    }
                    if (strSetNewStaffFirstName == strEmpty || strSetNewStaffLastName == strEmpty || strSetNewStaffID == strEmpty)
                    {
                        break;
                    }
                        
                    if (i == intStaffAmount)
                    {
                        txtEnterStaffPin.Background = Brushes.DarkSeaGreen;
                        txtEnterStaffFirstName.Background = Brushes.DarkSeaGreen;
                        txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                        boolStaffIdMatch = false;
                        boolStaffNameMatch = false;
                        boolSaveStaff = true;
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
            if (strFilterActive == btnFilterStaff1.Content)
            {
                boolGenerateStaff = true;
                boolFilterActive = false;
                GetCurrentStaffList();
                btnFilterStaff1.Foreground = Brushes.White;
            }
            else
            {
                btnFilterStaff1.Foreground = Brushes.Black;
            }
            if (strFilterActive == btnFilterStaff2.Content)
            {
                boolFilterActive = true;
                boolGenerateStaff = true;
                GetCurrentStaffList();
                btnFilterStaff2.Foreground = Brushes.White;
            }
            else
            {
                btnFilterStaff2.Foreground = Brushes.Black;
            }
            if (strFilterActive == btnFilterStaff3.Content)
            {
                boolFilterActive = true;
                boolGenerateStaff = true;
                GetCurrentStaffList();
                btnFilterStaff3.Foreground = Brushes.White;
            }
            else
            {
                btnFilterStaff3.Foreground = Brushes.Black;
            }
            if (strFilterActive == btnFilterStaff4.Content)
            {
                boolFilterActive = true;
                boolGenerateStaff = true;
                GetCurrentStaffList();
                btnFilterStaff4.Foreground = Brushes.White;
            }
            else
            {
                btnFilterStaff4.Foreground = Brushes.Black;
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

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            kbController.GetKeyPressed(sender);
        }
    }
}
