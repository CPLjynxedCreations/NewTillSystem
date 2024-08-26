using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
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
    public partial class EnterProductDetails : Window
    {
        public string strProductName;
        public string strProductPrice;

        public EnterProductDetails()
        {
            InitializeComponent();
            //OpenOnScreenKeyboard();
        }
        /* DISPLAYS WINDOWS ONSCREEN KEYBOARD
        private void OpenOnScreenKeyboard()
        {   
            var onScreenKeyboardProcess = new ProcessStartInfo("osk.exe")
            {
                UseShellExecute = true
            };
            Process.Start(onScreenKeyboardProcess);
        }
        */

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            strProductName = txtEnterProductName.Text;
            strProductPrice = txtEnterProductPrice.Text;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            strProductName = string.Empty;
            strProductPrice = string.Empty;
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
