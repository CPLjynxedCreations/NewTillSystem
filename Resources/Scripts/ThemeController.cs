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
using DocumentFormat.OpenXml.Bibliography;
using NewTillSystem.Windows;

namespace NewTillSystem.Resources.Scripts
{
    class ThemeController
    {
        TillScreen tillScreen;
        ManagerWindow ManagerWindow;

        const string themeFile = "ThemeProperties.dat";

        public string currentThemeName;// = "Default";

        public string textButtonAdminTheme = "ButtonAdminTheme";
        public string textButtonAdminImageTheme = "ButtonAdminImageTheme";

        public string textButtonEmptyTheme = "ButtonEmptyTheme";
        public string textButtonQuickTheme = "ButtonQuickTheme";
        public string textLabelDisplayTheme = "LabelDisplayTheme";
        public string textTextBoxDisplayTheme = "TextBoxDisplayTheme";
        public string textRectangleTheme = "RectangleTheme";
        public string textBorderTheme = "BorderTheme";
        public string textScrollViewTheme = "ScrollViewTheme";
        public string textComboBoxToggleTheme = "ComboBoxToggleTheme";
        public string textToggleTheme = "ToggleTheme";
        public string textTextBoxDisplayThemeError = "TextBoxDisplayThemeError";
        public string textTextBoxDisplayThemeSelected = "TextBoxDisplayThemeSelected";
        public string textButtonSelectedtAdminTheme = "ButtonSelectedtAdminTheme";
        public string textTextBlockTheme = "TextBlockTheme";
        public string textButtonForegroundSelectTheme = "ButtonForegroundSelectTheme";


        public string imgLoginSourceLocation = "/Resources/Images/";
        public string imgLoginSourceLocationFileType = ".jpg";
        public string imgLoginFileName;
        public string textLoginFileName;


        public string currentButtonAdminTheme;
        public string currentButtonAdminImageTheme;
        public string currentButtonEmptyTheme;
        public string currentButtonQuickTheme;
        public string currentLabelDisplayTheme;
        public string currentTextBoxDisplayTheme;
        public string currentRectangleTheme;
        public string currentBorderTheme;
        public string currentScrollViewTheme;
        public string currentComboBoxToggleTheme;
        public string currentToggleTheme;
        public string currentTextBoxDisplayThemeError;
        public string currentTextBoxDisplayThemeSelected;
        public string currentButtonSelectedtAdminTheme;
        public string currentTextBlockTheme;
        public string currentButtonForegroundSelectTheme;
        public string currentLoginFileName;

        public void CreateThemeFile()
        {
            if (!File.Exists(themeFile))
            {
                imgLoginFileName = "BarBackground";
                textLoginFileName = imgLoginSourceLocation + imgLoginFileName + imgLoginSourceLocationFileType;
                currentThemeName = "Default";
                using (var stream = File.Open(themeFile, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        writer.Write(currentThemeName);
                        writer.Write(currentThemeName + textButtonAdminTheme);
                        writer.Write(currentThemeName + textButtonAdminImageTheme);
                        writer.Write(currentThemeName + textButtonEmptyTheme);
                        writer.Write(currentThemeName + textButtonQuickTheme);
                        writer.Write(currentThemeName + textLabelDisplayTheme);
                        writer.Write(currentThemeName + textTextBoxDisplayTheme);
                        writer.Write(currentThemeName + textRectangleTheme);
                        writer.Write(currentThemeName + textBorderTheme);
                        writer.Write(currentThemeName + textScrollViewTheme);
                        writer.Write(currentThemeName + textComboBoxToggleTheme);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeError);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeSelected);
                        writer.Write(currentThemeName + textButtonSelectedtAdminTheme);
                        writer.Write(currentThemeName + textToggleTheme);
                        writer.Write(currentThemeName + textTextBlockTheme);
                        writer.Write(currentThemeName + textButtonForegroundSelectTheme);
                        writer.Write(textLoginFileName);
                        writer.Write(imgLoginFileName);
                    }
                }
            }
        }

        public void UpdateThemeFile()
        {
            currentLoginFileName = imgLoginSourceLocation + imgLoginFileName + imgLoginSourceLocationFileType;
            if (File.Exists(themeFile))
            {
                using (var stream = File.Open(themeFile, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        writer.Write(currentThemeName);
                        writer.Write(currentThemeName + textButtonAdminTheme);
                        writer.Write(currentThemeName + currentButtonAdminImageTheme);
                        writer.Write(currentThemeName + textButtonEmptyTheme);
                        writer.Write(currentThemeName + textButtonQuickTheme);
                        writer.Write(currentThemeName + textLabelDisplayTheme);
                        writer.Write(currentThemeName + textTextBoxDisplayTheme);
                        writer.Write(currentThemeName + textRectangleTheme);
                        writer.Write(currentThemeName + textBorderTheme);
                        writer.Write(currentThemeName + textScrollViewTheme);
                        writer.Write(currentThemeName + textComboBoxToggleTheme);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeError);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeSelected);
                        writer.Write(currentThemeName + textButtonSelectedtAdminTheme);
                        writer.Write(currentThemeName + textToggleTheme);
                        writer.Write(currentThemeName + textTextBlockTheme);
                        writer.Write(currentThemeName + textButtonForegroundSelectTheme);
                        writer.Write(currentLoginFileName);
                        writer.Write(imgLoginFileName);
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
                        currentThemeName = reader.ReadString();
                        currentButtonAdminTheme = reader.ReadString();
                        currentButtonAdminImageTheme = reader.ReadString();
                        currentButtonEmptyTheme = reader.ReadString();
                        currentButtonQuickTheme = reader.ReadString();
                        currentLabelDisplayTheme = reader.ReadString();
                        currentTextBoxDisplayTheme = reader.ReadString();
                        currentRectangleTheme = reader.ReadString();
                        currentBorderTheme = reader.ReadString();
                        currentScrollViewTheme = reader.ReadString();
                        currentComboBoxToggleTheme = reader.ReadString();
                        currentTextBoxDisplayThemeError = reader.ReadString();
                        currentTextBoxDisplayThemeSelected = reader.ReadString();
                        currentButtonSelectedtAdminTheme = reader.ReadString();
                        currentToggleTheme = reader.ReadString();
                        currentTextBlockTheme = reader.ReadString();
                        currentButtonForegroundSelectTheme = reader.ReadString();
                        currentLoginFileName = reader.ReadString();
                        imgLoginFileName = reader.ReadString();
                    }
                }
            }
        }

        public void SetTheme()
        {
            tillScreen = (TillScreen)Application.Current.MainWindow;
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
        }
    }
}
