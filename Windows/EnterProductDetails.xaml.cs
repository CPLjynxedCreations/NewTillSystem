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
        public string strButtonTheme;
        public string strButtonForeground;

        public EnterProductDetails()
        {
            InitializeComponent();
            panelKeybooardButtons.IsEnabled = false;
            panelNumpad.IsEnabled = false;
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

            // IF COLOR PICKED SAVE STR THEME ELSE default COLOR
            // if foregound picked save string else default foreground
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            strProductName = string.Empty;
            strProductPrice = string.Empty;
            //THEME EMPTY
            //foreground empty
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
                            //txtBox.BorderBrush = Brushes.Firebrick;
                            if (txtEnterProductPrice.IsFocused)
                            {
                                txtBox.BorderBrush = Brushes.Firebrick;
                                txtEnterProductName.BorderBrush = Brushes.DarkOliveGreen;
                                panelNumpad.IsEnabled = true;
                                panelKeybooardButtons.IsEnabled = false;
                                Keyboard.ClearFocus();
                                return;
                            }
                            else if (txtBox.Name == txtEnterProductName.Name)
                            {
                                txtBox.BorderBrush = Brushes.Firebrick;
                                txtEnterProductPrice.BorderBrush = Brushes.DarkOliveGreen;
                                panelKeybooardButtons.IsEnabled = true;
                                panelNumpad.IsEnabled = false;
                                Keyboard.ClearFocus();
                                return;
                            }
                            //Keyboard.ClearFocus();
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
                    if (item.GetType() == typeof(Button))
                    {
                        Button button = (Button)item;
                    }
                }
            }
            // check each ui for theme button. then name accordingly
            Button btnTest = (Button)sender;
            if (btnTest.Name.Contains("Theme"))
            {
                btnColorView.Style = (Style)Application.Current.Resources[btnTest.Name];
                strButtonTheme = btnTest.Name;
                //save string theme
            }
            if (btnTest.Name.Contains("Foreground"))
            {
                string colorTag = Convert.ToString(btnTest.Tag);
                SolidColorBrush colorBrush = (SolidColorBrush) new BrushConverter().ConvertFromString(colorTag);
                btnColorView.Foreground = colorBrush;
                strButtonForeground = Convert.ToString(btnTest.Tag);
                //save string foreground color
            }
            //SET TEST BUTTON COLOR
            //SET STR FOR THEME FROM CLICK SENDER.NAME
        }
    }
}
