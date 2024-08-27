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
using DocumentFormat.OpenXml.VariantTypes;

namespace NewTillSystem.Windows
{
    public partial class EnterProductDetails : Window
    {
        public string strProductName;
        public string strProductPrice;

        public EnterProductDetails()
        {
            InitializeComponent();
            //txtEnterProductName.Focus();
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

        private void btnKeyboardClick_Click(object sender, RoutedEventArgs e)
        {
            Button btnClicked = (Button)sender;
            string strLetterPressed = Convert.ToString(btnClicked.Content);
            if (strLetterPressed == btnKeyboard_SPACE.Content)
            {
                strLetterPressed = " ";
            }

            for (int i = 0; i <= grProductPanel.Children.Count; i++)
            {
                foreach (UIElement item in grProductPanel.Children)
                {
                    if (item.GetType() == typeof(TextBox))
                    {
                        TextBox txtBox = (TextBox)item;
                        if (txtBox.IsFocused)
                        {
                            if (strLetterPressed != btnKeyboard_DELETE.Content)
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
            for (int i = 0; i <= grProductPanel.Children.Count; i++)
            {
                foreach (UIElement item in grProductPanel.Children)
                {
                    if (item.GetType() == typeof(TextBox))
                    {
                        TextBox txtBox = (TextBox)item;
                        if (txtBox.IsFocused)
                        {
                            txtBox.BorderBrush = Brushes.Firebrick;
                            if (txtEnterProductPrice.IsFocused)
                            {
                                panelNumpad.IsEnabled = true;
                                panelKeybooardButtons.IsEnabled = false;
                            }
                            else
                            {
                                panelKeybooardButtons.IsEnabled = true;
                                panelNumpad.IsEnabled = false;
                            }
                            Keyboard.ClearFocus();
                        }
                        else
                        {
                            txtBox.BorderBrush = Brushes.DarkOliveGreen;
                        }
                    }
                    else
                    {
                        panelNumpad.IsEnabled = false;
                        panelKeybooardButtons.IsEnabled = false;
                    }
                }
            }

        }
    }
}
