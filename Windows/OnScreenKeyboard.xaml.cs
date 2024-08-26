using DocumentFormat.OpenXml.Drawing;
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

    public partial class OnScreenKeyboard : Window
    {
        public string letter;

        public OnScreenKeyboard()
        {
            InitializeComponent();
        }

        public void btnClick_Click(object sender, RoutedEventArgs e)
        {
            Button btnPressed = (Button)sender;
            letter = Convert.ToString(btnPressed.Content);

            Debug.WriteLine(letter);
        }
    }
}
