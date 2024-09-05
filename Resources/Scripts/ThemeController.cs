using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NewTillSystem.Windows;

namespace NewTillSystem.Resources.Scripts
{
    class ThemeController
    {
        TillScreen tillScreen;
        ManageTillWindow manageTillWindow;

        const string themeFile = "ThemeProperties.dat";

        public string currentThemeName = "LightBlue";

        public string textButtonAdminTheme = "ButtonAdminTheme";
        public string textButtonEmptyTheme = "ButtonEmptyTheme";
        public string textButtonQuickTheme = "ButtonQuickTheme";
        public string textLabelDisplayTheme = "LabelDisplayTheme";
        public string textTextBoxDisplayTheme = "TextBoxDisplayTheme";
        public string textRectangleTheme = "RectangleTheme";
        //public string textTextBoxDisplayThemeSelected = "TextBoxDisplayThemeSelected";
        //public string textTextBoxDisplayThemeError = "TextBoxDisplayThemeError";
        //public string textTextBlockTheme = "TextBlockTheme";
        //public string textScrollViewTheme = "ScrollViewTheme";
        //public string textButtonForegroundSelectTheme = "ButtonForegroundSelectTheme";
        //public string textButtonSelectedtAdminTheme = "ButtonSelectedtAdminTheme";
        //public string textToggleTheme = "ToggleTheme";
        //public string textBorderTheme = "BorderTheme";

        public string currentButtonAdminTheme;
        public string currentButtonEmptyTheme;
        public string currentButtonQuickTheme;
        public string currentLabelDisplayTheme;
        public string currentTextBoxDisplayTheme;
        public string currentRectangleTheme;

        public void CreateThemeFile()
        {
            if (!File.Exists(themeFile))
            {
                using (var stream = File.Open(themeFile, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        writer.Write(currentThemeName + textButtonAdminTheme);
                        writer.Write(currentThemeName + textButtonEmptyTheme);
                        writer.Write(currentThemeName + textButtonQuickTheme);
                        writer.Write(currentThemeName + textLabelDisplayTheme);
                        writer.Write(currentThemeName + textTextBoxDisplayTheme);
                        writer.Write(currentThemeName + textRectangleTheme);
                    }
                }
            }
        }

        public void SetThemeWindow()
        {
            tillScreen = (TillScreen)Application.Current.MainWindow;
        }

        public void ReadTheme()
        {
            if (File.Exists(themeFile))
            {
                using (var stream = File.Open(themeFile, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        currentButtonAdminTheme = reader.ReadString();
                        currentButtonEmptyTheme = reader.ReadString();
                        currentButtonQuickTheme = reader.ReadString();
                        currentLabelDisplayTheme = reader.ReadString();
                        currentTextBoxDisplayTheme = reader.ReadString();
                        currentRectangleTheme = reader.ReadString();
                    }
                }
            }
        }

        public void SetTheme()
        {
            //SET ADMIN BUTTONS AND EMPTY BUTTONS
            foreach (UIElement button in tillScreen.grBtnScreen.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button tillButton = (Button)button;
                    if (tillButton.Name.Contains("btnAdmin"))
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonAdminTheme];
                    }
                    else if (tillButton.Content == string.Empty)
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonEmptyTheme];
                    }
                }
            }

            //SET NUMPAD BUTTONS
            foreach (UIElement button in tillScreen.AdminNumbers.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button tillButton = (Button)button;
                    var tag = Convert.ToString(tillButton.Tag);
                    if (tag == "AdminQuick")
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonQuickTheme];
                    }
                    else if (tag == "AdminNumPad")
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonAdminTheme];
                    }
                }
            }
            tillScreen.bgAdminHeader.Style = (Style)Application.Current.Resources[currentLabelDisplayTheme];
            
            
            tillScreen.adminButtonTheme = currentButtonAdminTheme;
            tillScreen.quickButtonTheme = currentButtonQuickTheme;
            tillScreen.labelBoxTheme = currentLabelDisplayTheme;
            tillScreen.txtBoxLabelTheme = currentTextBoxDisplayTheme;
            tillScreen.rectangleTheme = currentRectangleTheme;
        }
    }
}
