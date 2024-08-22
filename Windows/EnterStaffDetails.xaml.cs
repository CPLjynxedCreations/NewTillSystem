using ClosedXML.Excel;
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
        private int intEditRowLine;
        private string strSetStaffName;
        private string strSetNewStaffName;
        private string strXlsxStaffNameColumn = "A";
        private string strSetStaffLastName;
        private string strSetNewStaffLastName;
        private string strXlsxStaffLastNameColumn = "B";
        private string strSetStaffID;
        private string strSetNewStaffID;
        private string strXlsxStaffIDColumn = "C";
        private string strSetStaffRole;
        private string strSetNewStaffRole;
        private string strXlsxStaffRoleColumn = "D";
        private string strAdmin = "ADMIN";
        private string strManager = "MANAGER";
        private string strStaff = "STAFF";

        private string strEmpty = "";
        private bool boolIsToDeleteStaff;


        private bool boolIsToEditStaff;

        private string strTglStaffName;
        private bool boolStaffNameMatch;
        private bool boolStaffIdMatch;
        private bool boolSaveStaff;
        private bool boolAddStaff;
        private bool boolStaffAdded;
        private bool boolGenerateStaff;

        private bool boolEditDelete;

        private string strExistingStaff;

        public EnterStaffDetails()
        {
            InitializeComponent();
            boolGenerateStaff = true;
            GetCurrentStaffList();
            txtEnterStaffFirstName.Focus();
        }

        private void GetCurrentStaffList()
        {
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\StaffID.xlsx");
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

                //Debug.WriteLine(joinNames);

                if (boolGenerateStaff)
                {
                    ToggleButton tglStaff = new ToggleButton();
                    tglStaff.Name = readXlsxDataStaffName + readXlsxDataStaffLastName;
                    tglStaff.Content = readXlsxDataStaffName + " " + readXlsxDataStaffLastName;
                    tglStaff.Checked += tglStaff_Checked;
                    if (tglStaff.Content != strEmpty)
                    {
                        panelExistingStaff.Children.Add(tglStaff);
                    }
                    if (i == intStaffAmount)
                    {
                        boolGenerateStaff = false;
                    }
                }
                if (boolAddStaff)
                {
                    if (strSetNewStaffName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName || strSetNewStaffID == readXlsxDataStaffID)
                    {
                        if (strSetNewStaffName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName)
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
                        strSetStaffName = strSeperateNames[0];
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
                                        string strNameCheck = strSetStaffName + strSetStaffLastName;
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
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffNameColumn).Value = strSetNewStaffName;
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

        /*
        if (boxSelectExistingStaff.Text == joinNames)
        {

            if (boolIsToDeleteStaff)
            {
                Debug.WriteLine("join " + joinNames);
                if (boxSelectExistingStaff.Text != joinNames || boxSelectExistingStaff.Text != "ADMIN ADMIN")
                {
                    Debug.WriteLine("Were in line " + joinNames);
                    //now display cannot delete because logged in
                    //didnt remove from excel
                    Debug.WriteLine("delete row = " + i);
                    workSheet.Row(i).Delete();
                    workBook.Save();
                    int intDeleteStaffNumber = boxSelectExistingStaff.SelectedIndex;
                    boxSelectExistingStaff.Items.RemoveAt(intDeleteStaffNumber);
                    Debug.WriteLine("delete staff  " + intDeleteStaffNumber);
                    boxSelectExistingStaff.SelectedItem = boxSelectExistingStaffText;
                    boxSelectExistingStaff.Items.Refresh();
                    boolIsToDeleteStaff = false;
                    workBook.Save();
                    GetCurrentStaffList();
                    //return;
                }
            }
        }*/

        private void tglStaff_Checked(object sender, EventArgs e)
        {
            ToggleButton tglStaff = (ToggleButton)sender;
            strTglStaffName = Convert.ToString(tglStaff.Content);
            Debug.WriteLine(strTglStaffName);

            /*
            boolIsToDeleteStaff = true;
            boolIsToEditStaff = true;
            GetCurrentStaffList();
            txtEnterStaffFirstName.Text = strSetStaffName;
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
            */
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
            txtEnterStaffFirstName.Text = strSetStaffName;
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
                strSetNewStaffName = txtEnterStaffFirstName.Text.ToUpper();
                strSetNewStaffLastName = txtEnterStaffLastName.Text.ToUpper();
                strSetNewStaffID = txtEnterStaffPin.Text;
                strSetNewStaffRole = boxSelectRole.Text.ToUpper();

                if (boxRoleSelectNameText.IsSelected)
                {
                    strSetNewStaffRole = strEmpty;
                }
                GetCurrentStaffList();
                if (boolStaffAdded)
                {
                    boolStaffAdded = false;
                    this.Close();
                }
            }
        }
    }
}
