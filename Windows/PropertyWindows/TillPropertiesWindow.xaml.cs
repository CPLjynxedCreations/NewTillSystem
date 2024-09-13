﻿using DocumentFormat.OpenXml.Drawing;
using NewTillSystem.Resources.Scripts;
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

    public partial class TillPropertiesWindow : Window
    {
        ThemeController themeController;
        public string theme;
        public string selectedLoginBackground;
        public string selectedLoginTimeForeground;

        public TillPropertiesWindow()
        {
            InitializeComponent();
            themeController = new ThemeController();
            themeController.ReadTheme();
            theme = themeController.currentThemeName;
            selectedLoginTimeForeground = themeController.currentLabelLoginScreenTimeColor;
            selectedLoginBackground = themeController.imgLoginBackgroundFileName;

            #region THEME SELECTED
            if (theme == "Default")
            {
                boxSelectTheme.SelectedItem = boxThemeSelectDefualt;
            }
            if (theme == "LightGreen")
            {
                boxSelectTheme.SelectedItem = boxThemeSelectLightGreen;
            }
            if (theme == "LightBlue")
            {
                boxSelectTheme.SelectedItem = boxThemeSelectLightBlue;
            }
            if (theme == "DarkRed")
            {
                boxSelectTheme.SelectedItem = boxThemeSelectDarkRed;
            }
            #endregion

            #region LOGIN LABEL COLOR SELECTED
            if (selectedLoginTimeForeground == "White")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeWhite;
            }
            if (selectedLoginTimeForeground == "#EE4B2B")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightRed;
            }
            if (selectedLoginTimeForeground == "#4CBB17")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightGreen;
            }
            if (selectedLoginTimeForeground == "#E0B0FF")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightPurple;
            }
            if (selectedLoginTimeForeground == "#880808")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkRed;
            }
            if (selectedLoginTimeForeground == "#355E3B")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkGreen;
            }
            if (selectedLoginTimeForeground == "#702963")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkPurple;
            }
            if (selectedLoginTimeForeground == "#FF7518")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeOrange;
            }
            if (selectedLoginTimeForeground == "#00FFFF")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeAqua;
            }
            #endregion
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (boxSelectTheme.Text != string.Empty)
            {
                themeController.currentThemeName = boxSelectTheme.Text;
            }
            if (boxSelectTheme.Text != string.Empty)
            {
                var color = boxSelectLoginTimeColor.SelectionBoxItem.ToString();
                foreach (UIElement list in boxSelectLoginTimeColor.Items)
                {
                    if (list.GetType() == typeof(ComboBoxItem))
                    {
                        ComboBoxItem listItem = (ComboBoxItem)list;
                        var hexCode = Convert.ToString(listItem.Tag);
                        if (color == listItem.Content)
                        {
                            selectedLoginTimeForeground = hexCode;
                        }
                    }
                }
            }
            themeController.currentLabelLoginScreenTimeColor = selectedLoginTimeForeground;
            themeController.imgLoginBackgroundFileName = selectedLoginBackground;
            themeController.UpdateThemeFile();
            themeController.ReadTheme();
            themeController.SetTheme();
        }

        private void LoginBackground_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            selectedLoginBackground = Convert.ToString(button.Name);
            themeController.imgLoginBackgroundFileName = selectedLoginBackground;
            themeController.UpdateThemeFile();
        }
    }
}
