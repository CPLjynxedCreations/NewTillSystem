﻿using System;
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
using NewTillSystem.Resources.Scripts;

namespace NewTillSystem.Windows
{
    public partial class ProductDetails : Window
    {
        ThemeController themeController;
        TillScreen tillScreen;

        public string strProductName;
        public string strProductPrice;
        public string strButtonTheme;
        public string strButtonForeground;
        public string strEditProductName;
        public string strEditProductPrice;
        public string strEditButtonTheme;
        public string strEditButtonForeground;
        public bool boolIsEditing;

        private string errorBoxThemeProduct;
        private string txtBoxLabelThemeProduct;
        private string txtBoxLabelSelectedThemeProduct;

        public ProductDetails()
        {
            InitializeComponent();

            themeController = new ThemeController();
            tillScreen = (TillScreen)Application.Current.MainWindow;

            errorBoxThemeProduct = tillScreen.errorBoxTheme;
            txtBoxLabelThemeProduct = tillScreen.txtBoxLabelTheme;
            txtBoxLabelSelectedThemeProduct = tillScreen.txtBoxSelectLabelTheme;

            panelKeybooardButtons.IsEnabled = false;
            panelNumpad.IsEnabled = false;
            if (boolIsEditing)
            {
                strButtonTheme = strEditButtonTheme;
                strButtonForeground = strEditButtonForeground;
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            strProductName = string.Empty;
            strProductPrice = string.Empty;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (boolIsEditing)
            {
                if (strButtonTheme == null)
                {
                    strButtonTheme = strEditButtonTheme;
                }
                if (strButtonForeground == null)
                {
                    strButtonForeground = strEditButtonForeground;
                }
            }
            else
            {
                if (strButtonTheme == null)
                {
                    strButtonTheme = "btnDefaultItemTheme";
                }
                if (strButtonForeground == null)
                {
                    strButtonForeground = "Black";
                }
            }
            strProductName = txtEnterProductName.Text.ToUpper();
            strProductPrice = txtEnterProductPrice.Text;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!boolIsEditing)
            {
                strProductName = string.Empty;
                strProductPrice = string.Empty;
            }
            else
            {
                strProductName = strEditProductName.ToUpper();
                strProductPrice = strEditProductPrice;
                strButtonTheme = strEditButtonTheme;
                strButtonForeground = strEditButtonForeground;
            }
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
                                txtBox.Text = txtBox.Text.ToUpper() + strLetterPressed.ToUpper();
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
                            if (txtEnterProductPrice.IsFocused)
                            {
                                txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelSelectedThemeProduct];
                                txtEnterProductName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeProduct];
                                panelNumpad.IsEnabled = true;
                                panelKeybooardButtons.IsEnabled = false;
                                Keyboard.ClearFocus();
                                return;
                            }
                            else if (txtBox.Name == txtEnterProductName.Name)
                            {
                                txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelSelectedThemeProduct];
                                txtEnterProductPrice.Style = (Style)Application.Current.Resources[txtBoxLabelThemeProduct];
                                panelKeybooardButtons.IsEnabled = true;
                                panelNumpad.IsEnabled = false;
                                Keyboard.ClearFocus();
                                return;
                            }
                        }
                        else
                        {
                            txtBox.Style = (Style)Application.Current.Resources[txtBoxLabelThemeProduct];
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
        }

        private void btnItemTheme_Click(object sender, RoutedEventArgs e)
        {
            Button btnTest = (Button)sender;
            if (btnTest.Name.Contains("Theme"))
            {
                btnColorView.Style = (Style)Application.Current.Resources[btnTest.Name];
                strButtonTheme = btnTest.Name;
                txtEnterProductName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeProduct];
                txtEnterProductPrice.Style = (Style)Application.Current.Resources[txtBoxLabelThemeProduct];
                //save string theme
            }
            if (btnTest.Name.Contains("Foreground"))
            {
                string colorTag = Convert.ToString(btnTest.Tag);
                SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(colorTag);
                btnColorView.Foreground = colorBrush;
                strButtonForeground = Convert.ToString(btnTest.Tag);
                txtEnterProductName.Style = (Style)Application.Current.Resources[txtBoxLabelThemeProduct];
                txtEnterProductPrice.Style = (Style)Application.Current.Resources[txtBoxLabelThemeProduct];
            }
            panelKeybooardButtons.IsEnabled = false;
            panelNumpad.IsEnabled = false;
        }
    }
}