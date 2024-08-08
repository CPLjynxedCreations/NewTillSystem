using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewTillSystem
{
    public partial class TillScren : Window
    {
        private string strEmpty = "";
        private int saleScreeenLine;

        public TillScren()
        {
            InitializeComponent();
            ClearStartStrings();
        }

        private void ClearStartStrings()
        {
            var builder = new StringBuilder();
            for (int i = 1; i <= 13; i++)
            {
                string btnChange = "btnRow";
                
                builder.Append(btnChange + i);
                Debug.WriteLine(btnChange + i);
            }
            
            btnRow1.Content = strEmpty;
            btnRow2.Content = strEmpty;
            btnRow3.Content = strEmpty;
            btnRow4.Content = strEmpty;
            btnRow5.Content = strEmpty;
            btnRow6.Content = strEmpty;
            btnRow7.Content = strEmpty;
            btnRow8.Content = strEmpty;
            btnRow9.Content = strEmpty;
            btnRow10.Content = strEmpty;
            btnRow11.Content = strEmpty;
            btnRow12.Content = strEmpty;
            btnRow13.Content = strEmpty;
            btnRow13.Content = strEmpty;
            lbl1Row1.Text = strEmpty;
            lbl2Row1.Text = strEmpty;
            lbl1Row2.Text = strEmpty;
            lbl2Row2.Text = strEmpty;
            lbl1Row3.Text = strEmpty;
            lbl2Row3.Text = strEmpty;
            lbl1Row4.Text = strEmpty;
            lbl2Row4.Text = strEmpty;
            lbl1Row5.Text = strEmpty;
            lbl2Row5.Text = strEmpty;
            lbl1Row6.Text = strEmpty;
            lbl2Row6.Text = strEmpty;
            lbl1Row7.Text = strEmpty;
            lbl2Row7.Text = strEmpty;
            lbl1Row8.Text = strEmpty;
            lbl2Row8.Text = strEmpty;
            lbl1Row9.Text = strEmpty;
            lbl2Row9.Text = strEmpty;
            lbl1Row10.Text = strEmpty;
            lbl2Row10.Text = strEmpty;
            lbl1Row11.Text = strEmpty;
            lbl2Row11.Text = strEmpty;
            lbl1Row12.Text = strEmpty;
            lbl2Row12.Text = strEmpty;
            lbl1Row13.Text = strEmpty;
            lbl2Row13.Text = strEmpty;
            lblSaleScreenTotalAmount.Text = strEmpty;
        }
    }
}