using DocumentFormat.OpenXml.Drawing;
using NewTillSystem.Resources.Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        public string theme = string.Empty;
        public string selectedLoginBackground = string.Empty;
        public string selectedLoginTimeForeground = string.Empty;
        public string unslectedBackgroundImageButton = string.Empty;

        public TillPropertiesWindow()
        {
            InitializeComponent();
            themeController = new ThemeController();
            themeController.ReadTheme();
            theme = themeController.currentThemeName;
            selectedLoginTimeForeground = themeController.currentLabelLoginScreenTimeColor;
            selectedLoginBackground = themeController.imgLoginBackgroundFileName;
            unslectedBackgroundImageButton = themeController.imgLoginBackgroundFileName;

            SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(selectedLoginTimeForeground);
            toggleBoxSelectLoginTimeColor.Background = colorBrush;

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

        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (boxSelectTheme.Text != string.Empty)
            {
                themeController.currentThemeName = boxSelectTheme.Text;
            }
            themeController.currentLabelLoginScreenTimeColor = selectedLoginTimeForeground;
            themeController.imgLoginBackgroundFileName = selectedLoginBackground;
            themeController.UpdateThemeFile();
            themeController.ReadTheme();
            themeController.SetTheme();
        }

        private void LoginBackground_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            foreach (UIElement imgButton in grdLoginBackgroundPanel.Children)
            {
                if (imgButton.GetType() == typeof(Button))
                {
                    Button imageButton = (Button)imgButton;
                    if (Convert.ToString(imageButton.Name) == Convert.ToString(clickedButton.Name))
                    {
                        clickedButton.BorderThickness = new Thickness(15);
                    }
                    else
                    {
                        imageButton.BorderThickness = new Thickness(5);
                    }
                }
            }
            selectedLoginBackground = Convert.ToString(clickedButton.Name);
        }

        private void toggleBoxSelectLoginTimeColor_Checked(object sender, RoutedEventArgs e)
        {
            if (toggleBoxSelectLoginTimeColor.IsChecked == true)
            {
                popBoxSelectLoginTimeColorDrop.IsOpen = true;
            }
            if (toggleBoxSelectLoginTimeColor.IsChecked == false)
            {
                popBoxSelectLoginTimeColorDrop.IsOpen = false;
            }
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            toggleBoxSelectLoginTimeColor.Background = button.Background;
            toggleBoxSelectLoginTimeColor.IsChecked = false;
            selectedLoginTimeForeground = button.Tag.ToString();
        }
    }
}
