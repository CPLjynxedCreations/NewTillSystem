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
        private string strXlsxStaffRollColumn = "D";
        private string strAdmin = "ADMIN";
        private string strManager = "MANAGER";
        private string strStaff = "STAFF";

        private string strEmpty = "";
        private bool boolIsToDeleteStaff;
        private bool boolIsToEditStaff;
        private bool boolIsAddNewStaff;
        private bool boolIsNewStaff;

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
            for (int i = 1; i <= intStaffAmount; i++)
            {
                var readXlsxDataStaffName = workSheet.Cell(i, strXlsxStaffNameColumn).GetValue<string>();
                var readXlsxDataStaffLastName = workSheet.Cell(i, strXlsxStaffLastNameColumn).GetValue<string>();
                var readXlsxDataStaffID = workSheet.Cell(i, strXlsxStaffIDColumn).GetValue<string>();
                var readXlsxDataStaffRole = workSheet.Cell(i, strXlsxStaffRollColumn).GetValue<string>();
                var joinNames = readXlsxDataStaffName + " " + readXlsxDataStaffLastName;
                if (!boxSelectExistingStaff.Items.Contains(readXlsxDataStaffName + " " + readXlsxDataStaffLastName))
                {
                    boxSelectExistingStaff.Items.Add(readXlsxDataStaffName + " " + readXlsxDataStaffLastName);
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
                        if (boxSelectExistingStaff.Text != joinNames || boxSelectExistingStaff.Text != "ADMIN ADMIN")
                        {
                            //now display cannot delete because logged in
                            //didnt remove from excel
                            workSheet.Row(i).Delete();
                            int intDeleteStaffNumber = boxSelectExistingStaff.SelectedIndex;
                            boxSelectExistingStaff.Items.RemoveAt(intDeleteStaffNumber);
                            boxSelectExistingStaff.SelectedItem = boxSelectExistingStaffText;
                            boxSelectExistingStaff.Items.Refresh();
                            workBook.Save();
                            boolIsToDeleteStaff = false;
                            return;
                        }
                    }
                    if (boolIsToEditStaff)
                    {
                        //if is edit staff save file with new details
                        boolIsToEditStaff = false;
                    }
                }

                // CHECK HERE FOR THE SAVING ERROR OF EXISTING STAFF
                //still having errors with detecting coppy
                if (boolIsAddNewStaff)
                {
                    if (strSetNewStaffName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName || strSetNewStaffID == readXlsxDataStaffID)
                    {
                        if (strSetNewStaffName == readXlsxDataStaffName && strSetNewStaffLastName == readXlsxDataStaffLastName)
                        {
                            Debug.WriteLine("name match");
                            txtEnterStaffFirstName.Background = Brushes.Red;
                            txtEnterStaffLastName.Background = Brushes.Red;
                            //turn red
                            //in first name text user exists
                            //return;
                        }
                        else
                        {
                            txtEnterStaffFirstName.Background = Brushes.DarkSeaGreen;
                            txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                        }
                        /*if (strSetNewStaffLastName == readXlsxDataStaffLastName)
                        {
                            txtEnterStaffLastName.Background = Brushes.Red;
                        }
                        else if (strSetNewStaffLastName != readXlsxDataStaffLastName)
                        {
                            txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                        }*/
                        if (strSetNewStaffID == readXlsxDataStaffID)
                        {
                            Debug.WriteLine("id match");
                            txtEnterStaffPin.Background = Brushes.Red;
                            //turn red
                            //set text ID used
                            //return;
                        }
                        else if (strSetNewStaffID != readXlsxDataStaffID)
                        {
                            txtEnterStaffPin.Background = Brushes.DarkSeaGreen;
                        }
                        boolIsAddNewStaff = false;
                        boolIsNewStaff = false;
                    }
                    else
                    {
                        txtEnterStaffPin.Background = Brushes.DarkSeaGreen;
                        txtEnterStaffFirstName.Background = Brushes.DarkSeaGreen;
                        txtEnterStaffLastName.Background = Brushes.DarkSeaGreen;
                        boolIsNewStaff = true;
                        Debug.WriteLine("no match");
                    }
                }
            }
            if (boolIsNewStaff)
            {

                int intGetNewStaffRow = workSheet.LastRowUsed().RowNumber() + 1;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffNameColumn).Value = strSetNewStaffName;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffLastNameColumn).Value = strSetNewStaffLastName;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffIDColumn).Value = strSetNewStaffID;
                workSheet.Cell(intGetNewStaffRow, strXlsxStaffRollColumn).Value = strSetNewStaffRole;
            }
            workSheet.ColumnsUsed().AdjustToContents();
            workBook.Save();
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            boolIsToDeleteStaff = true;
            GetCurrentStaffList();
        }

        //if edit ok will override that staff member
        private void btnEditStaff_Click(object sender, RoutedEventArgs e)
        {
            //boxSelectRole.Items.Remove(boxSelectRole);
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
            else if (strSetStaffRole == strAdmin)
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
                strSetNewStaffID = txtEnterStaffPin.Text.ToUpper();
                strSetNewStaffRole = boxSelectRole.Text.ToUpper();
                GetCurrentStaffList();
                if (boolIsNewStaff)
                {
                    boolIsNewStaff = false;
                    this.Close();
                }
                //need to stop close if not new staff
            }
            if (boolIsToEditStaff)
            {
                //check if it still matches and just cancel
                //otherwise save over row with new details
                //  or store them delete row and save to new one
            }
        }
    }
}
