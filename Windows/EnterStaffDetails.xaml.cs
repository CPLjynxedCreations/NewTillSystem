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
        private bool boolIsAddNewStaff;
        private bool boolStaffAdded;
        private bool boolIsNewStaff;
        private bool boolEditDelete;

        public EnterStaffDetails()
        {
            InitializeComponent();
            GetCurrentStaffList();
            txtEnterStaffFirstName.Focus();
        }

        private void GetCurrentStaffList()
        {
            var workBook = new XLWorkbook("C:\\Users\\Cpljy\\source\\repos\\Projects\\NewTillSystem\\Resources\\StaffID.xlsx");
            var workSheet = workBook.Worksheet("Staff");
            int intStaffAmount = workSheet.LastRowUsed().RowNumber();
            var range = workSheet.Range(strXlsxStaffNameColumn + 1, strXlsxStaffRoleColumn + workSheet.RowsUsed().Count());
            for (int i = 1; i <= intStaffAmount; i++)
            {
                var readXlsxDataStaffName = workSheet.Cell(i, strXlsxStaffNameColumn).GetValue<string>();
                var readXlsxDataStaffLastName = workSheet.Cell(i, strXlsxStaffLastNameColumn).GetValue<string>();
                var readXlsxDataStaffID = workSheet.Cell(i, strXlsxStaffIDColumn).GetValue<string>();
                var readXlsxDataStaffRole = workSheet.Cell(i, strXlsxStaffRoleColumn).GetValue<string>();
                var joinNames = readXlsxDataStaffName + " " + readXlsxDataStaffLastName;

                Debug.WriteLine(joinNames);
                if (!boxSelectExistingStaff.Items.Contains(readXlsxDataStaffName + " " + readXlsxDataStaffLastName))
                {
                    boxSelectExistingStaff.Items.Add(readXlsxDataStaffName + " " + readXlsxDataStaffLastName);
                    ToggleButton test = new ToggleButton();
                    test.Name = "test1";
                    test.Content = readXlsxDataStaffName + " " + readXlsxDataStaffLastName;
                    panelExistingStaff.Children.Add(test);
                    range.SetAutoFilter().Sort(2, XLSortOrder.Ascending);
                }
                if (boxSelectExistingStaff.Text == joinNames)
                {
                    string[] strSeperateNames = joinNames.Split(' ');
                    strSetStaffName = strSeperateNames[0];
                    strSetStaffLastName = strSeperateNames[1];
                    strSetStaffID = readXlsxDataStaffID;
                    strSetStaffRole = readXlsxDataStaffRole;
                    intEditRowLine = i;
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
                }

                if (boolIsAddNewStaff)
                {
                    if (strSetNewStaffName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName || strSetNewStaffID == readXlsxDataStaffID)
                    {
                        if (strSetNewStaffName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName)
                        {
                            txtEnterStaffFirstName.Background = Brushes.Red;
                            txtEnterStaffLastName.Background = Brushes.Red;
                        }
                        else
                        {
                            txtEnterStaffFirstName.Background = Brushes.DarkSeaGreen;
                            txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                        }
                        if (strSetNewStaffID == readXlsxDataStaffID)
                        {
                            txtEnterStaffPin.Background = Brushes.Red;
                        }
                        else
                        {
                            txtEnterStaffPin.Background = Brushes.DarkSeaGreen;

                        }
                        boolIsAddNewStaff = false;
                        boolIsNewStaff = false;
                        boolIsToEditStaff = false;
                        boolIsToDeleteStaff = false;
                    }
                    else
                    {
                        txtEnterStaffPin.Background = Brushes.DarkSeaGreen;
                        txtEnterStaffFirstName.Background = Brushes.DarkSeaGreen;
                        txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                        boolIsNewStaff = true;
                    }
                }


                if (boolIsNewStaff)
                {
                    if (!boolStaffAdded)
                    {
                        int intGetNewStaffRow = workSheet.LastRowUsed().RowNumber() + 1;
                        workSheet.Cell(intGetNewStaffRow, strXlsxStaffNameColumn).Value = strSetNewStaffName;
                        workSheet.Cell(intGetNewStaffRow, strXlsxStaffLastNameColumn).Value = strSetNewStaffLastName;
                        workSheet.Cell(intGetNewStaffRow, strXlsxStaffIDColumn).Value = strSetNewStaffID;
                        workSheet.Cell(intGetNewStaffRow, strXlsxStaffRoleColumn).Value = strSetNewStaffRole;
                        boolStaffAdded = true;
                    }
                }
                range.SetAutoFilter().Sort(2, XLSortOrder.Ascending);
                workSheet.ColumnsUsed().AdjustToContents();
                workBook.Save();
            }
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            boolIsToDeleteStaff = true;
            GetCurrentStaffList();
        }

        //if edit ok will override that staff member
        private void btnEditStaff_Click(object sender, RoutedEventArgs e)
        {
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
            //save edit file by getfile current file
        }

        //else exists try again prompt
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!boolIsToEditStaff)
            {
                boolIsAddNewStaff = true;
                strSetNewStaffName = txtEnterStaffFirstName.Text.ToUpper();
                strSetNewStaffLastName = txtEnterStaffLastName.Text.ToUpper();
                strSetNewStaffID = txtEnterStaffPin.Text;
                strSetNewStaffRole = boxSelectRole.Text.ToUpper();

                ToggleButton test = new ToggleButton();
                test.Name = "test1";
                test.Content = "name 1";
                panelExistingStaff.Children.Add(test);


                if (boxRoleSelectNameText.IsSelected)
                {
                    strSetNewStaffRole = strEmpty;
                }
                GetCurrentStaffList();
                if (boolIsNewStaff)
                {
                    boolIsNewStaff = false;
                    this.Close();
                }

                //need to stop close if not new staff
            }
        }
    }
}
