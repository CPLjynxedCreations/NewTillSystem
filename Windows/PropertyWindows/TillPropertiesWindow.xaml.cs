using DocumentFormat.OpenXml.Drawing;
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

            if (selectedLoginTimeForeground == "White")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeWhite;
            }
            if (selectedLoginTimeForeground == "Red")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeRed;
            }
            if (selectedLoginTimeForeground == "Green")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeGreen;
            }
            if (selectedLoginTimeForeground == "Orange")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeOrange;
            }
            if (selectedLoginTimeForeground == "Purple")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimePurple;
            }
            if (selectedLoginTimeForeground == "Aqua")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeAqua;
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (boxSelectTheme.Text != string.Empty)
            {
                themeController.currentThemeName = boxSelectTheme.Text;
            }
            if (boxSelectTheme.Text != string.Empty)
            {
                themeController.currentLabelLoginScreenTimeColor = boxSelectLoginTimeColor.Text;
            }
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
