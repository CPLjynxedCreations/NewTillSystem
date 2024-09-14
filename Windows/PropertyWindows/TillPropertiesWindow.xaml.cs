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
            if (selectedLoginTimeForeground == "Black")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeBlack;
            }
            if (selectedLoginTimeForeground == "#00BFFF")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightBlue;
            }
            if (selectedLoginTimeForeground == "#DEB887")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightBrown;
            }
            if (selectedLoginTimeForeground == "#4CBB17")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightGreen;
            }
            if (selectedLoginTimeForeground == "#DCDCDC")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightGray;
            }
            if (selectedLoginTimeForeground == "#FFC0CB")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightPink;
            }
            if (selectedLoginTimeForeground == "#E0B0FF")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightPurple;
            }
            if (selectedLoginTimeForeground == "#EE4B2B")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightRed;
            }
            if (selectedLoginTimeForeground == "#FFFACD")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeLightYellow;
            }
            if (selectedLoginTimeForeground == "#4682B4")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkBlue;
            }
            if (selectedLoginTimeForeground == "#8B4513")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkBrown;
            }
            if (selectedLoginTimeForeground == "#355E3B")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkGreen;
            }
            if (selectedLoginTimeForeground == "#696969")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkGray;
            }
            if (selectedLoginTimeForeground == "#FF1493")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkPink;
            }
            if (selectedLoginTimeForeground == "#702963")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkPurple;
            }
            if (selectedLoginTimeForeground == "#880808")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkRed;
            }
            if (selectedLoginTimeForeground == "#FFD700")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeDarkYellow;
            }
            if (selectedLoginTimeForeground == "#FF7518")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeOrange;
            }
            if (selectedLoginTimeForeground == "#00FFFF")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeAqua;
            }
            if (selectedLoginTimeForeground == "White")
            {
                boxSelectLoginTimeColor.SelectedItem = boxTimeWhite;
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
                        if (listItem.Content.ToString() == color)
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
    }
}
