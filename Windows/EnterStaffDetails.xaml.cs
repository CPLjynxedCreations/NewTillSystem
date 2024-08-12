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
        private string strXlsxStaffNameColumn = "A";
        private string strSetStaffLastName;
        private string strXlsxStaffLastNameColumn = "B";
        private string strSetStaffID;
        private string strXlsxStaffIDColumn = "C";
        private string strSetStaffRole;
        private string strXlsxStaffRollColumn = "D";
        private string strAdmin = "ADMIN";
        private string strManager = "MANAGER";
        private string strStaff = "STAFF";

        private string strEmpty = "";
        private bool boolIsToDeleteStaff;
        private bool boolIsToEditStaff;

        public EnterStaffDetails()
        {
            InitializeComponent();
            GetCurrentStaffList();
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
                            workSheet.Row(i).Delete();
                            int intDeleteStaffNumber = boxSelectExistingStaff.SelectedIndex;
                            boxSelectExistingStaff.Items.RemoveAt(intDeleteStaffNumber);
                            boxSelectExistingStaff.SelectedItem = boxSelectExistingStaffText;
                            boxSelectExistingStaff.Items.Refresh();
                            workBook.Save();
                            boolIsToDeleteStaff = false;
                            return;
                            //now display cannot delete logged in
                        }
                    }
                    //if is edit staff save file with new details
                }
            }
            workSheet.ColumnsUsed().AdjustToContents();
            workBook.Save();
        }

        private void btnDeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            boolIsToDeleteStaff = true;
            GetCurrentStaffList();
        }

        //edit move to left side to enter datails();
        //bool edit staff
        //save staff name to string edit details code names roll
        //dispay in left in all boxes
        //if edit ok will override that staff member
        private void btnEditStaff_Click(object sender, RoutedEventArgs e)
        {
            boxSelectRole.Items.Remove(boxSelectRole);
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
            //save edit file by getfile
        }

        //on ok check file if doesnt exist close();
        // see if this saves details without close in main
        //else exists try again prompt
        private void AddStaff()
        {

        }
    }
}
