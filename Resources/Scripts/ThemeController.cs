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

        const string themeFile = "ThemeProperties.dat";

        public string currentThemeName = string.Empty;
        public string textButtonProperty = "PropertytButton";
        public string textButtonPropertySelected = "PropertytButtonSelected";
        public string textButtonTillNavigation = "TillNagivationButton";
        public string textButtonTillNavigationManageClose = "ManageWindowCloseButton";
        public string textButtonTillNumpad = "TillNumpad";
        public string textButtonTillQuick = "TillQuick";
        public string textButtonEmptyProduct = "EmptyProductButton";
        public string textKeyboardKeys = "KeyboardKeys";
        public string textKeyboardNumpad = "KeyboardNumpad";
        public string textKeyboardBackground= "RectangleKeyboardBackground";
        public string textScrollViewTheme = "ScrollViewTheme";
        public string textToggleTheme = "ToggleTheme";
        public string textComboBoxToggleTheme = "ComboBoxToggleTheme";
        public string textRectangleBackgroundTheme = "RectangleBackgroundTheme";
        public string textBorderTheme = "BorderTheme";
        public string textLabelDisplayBackgroundTheme = "LabelumpadTopBackground";
        public string textTextBlockTheme = "TextBlockTheme";
        public string textTextBoxDisplayTheme = "TextBoxDisplayTheme";
        public string textTextBoxDisplayThemeError = "TextBoxDisplayThemeError";
        public string textTextBoxDisplayThemeSelected = "TextBoxDisplayThemeSelected";
        public string textLabelLoginScreenTimeColor = "=LoginScreenTimeColor";
        public string imgLoginSourceLocation = "/Resources/Images/";
        public string imgLoginSourceLocationFileType = ".jpg";
        public string imgLoginBackgroundFileName = string.Empty;
        public string textLoginFileName = string.Empty;
        public string textButtonAdminImageTheme = "ButtonAdminImageTheme";//??

        public string currentButtonProperty = string.Empty;
        public string currentButtonPropertySelected = string.Empty;
        public string currentButtonTillNavigation = string.Empty;
        public string currentButtonTillNavigationManageClose = string.Empty;
        public string currentButtonTillNumpad = string.Empty;
        public string currentButtonTillQuick = string.Empty;
        public string currentButtonEmptyProduct = string.Empty;
        public string currentKeyboardNumpad = string.Empty;
        public string currentKeyboardKeys = string.Empty;
        public string currentKeyboardBackground = string.Empty;
        public string currentScrollViewTheme = string.Empty;
        public string currentRectangleBackgroundTheme = string.Empty;
        public string currentComboBoxToggleTheme = string.Empty;
        public string currentToggleTheme = string.Empty;
        public string currentLabelDisplayBackgroundTheme = string.Empty;
        public string currentTextBlockTheme = string.Empty;
        public string currentLabelLoginScreenTimeColor = string.Empty;
        public string currentButtonAdminImageTheme = string.Empty;//??
        public string currentTextBoxDisplayTheme = string.Empty;
        public string currentTextBoxDisplayThemeError = string.Empty;
        public string currentTextBoxDisplayThemeSelected = string.Empty;
        public string currentLoginBackgroundFileName = string.Empty;
        public string currentBorderTheme = string.Empty;
 

        public void CreateThemeFile()
        {
            if (!File.Exists(themeFile))
            {
                imgLoginBackgroundFileName = "BarBackground";
                textLoginFileName = imgLoginSourceLocation + imgLoginBackgroundFileName + imgLoginSourceLocationFileType;
                currentThemeName = "Default";
                currentLabelLoginScreenTimeColor = "White";
                using (var stream = File.Open(themeFile, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        writer.Write(currentThemeName);
                        writer.Write(currentThemeName + textButtonProperty);
                        writer.Write(currentThemeName + textButtonPropertySelected);
                        writer.Write(currentThemeName + textButtonTillNavigation);
                        writer.Write(currentThemeName + textButtonTillNavigationManageClose);
                        writer.Write(currentThemeName + textButtonTillNumpad);
                        writer.Write(currentThemeName + textButtonTillQuick);
                        writer.Write(currentThemeName + textButtonEmptyProduct);
                        writer.Write(currentThemeName + textKeyboardKeys);
                        writer.Write(currentThemeName + textKeyboardNumpad);
                        writer.Write(currentThemeName + textKeyboardBackground);
                        writer.Write(currentThemeName + textScrollViewTheme);
                        writer.Write(currentThemeName + textToggleTheme);
                        writer.Write(currentThemeName + textComboBoxToggleTheme);
                        writer.Write(currentThemeName + textRectangleBackgroundTheme);
                        writer.Write(currentThemeName + textLabelDisplayBackgroundTheme);
                        writer.Write(currentThemeName + textTextBlockTheme);
                        writer.Write(currentLabelLoginScreenTimeColor + textLabelLoginScreenTimeColor);
                        writer.Write(currentThemeName + textTextBoxDisplayTheme);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeError);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeSelected);
                        writer.Write(currentThemeName + textButtonAdminImageTheme);//??
                        writer.Write(textLoginFileName);
                        writer.Write(imgLoginBackgroundFileName);
                        writer.Write(textBorderTheme);
                    }
                }
            }
        }

        public void UpdateThemeFile()
        {
            currentLoginBackgroundFileName = imgLoginSourceLocation + imgLoginBackgroundFileName + imgLoginSourceLocationFileType;
            if (File.Exists(themeFile))
            {
                using (var stream = File.Open(themeFile, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        writer.Write(currentThemeName);
                        writer.Write(currentThemeName + textButtonProperty);
                        writer.Write(currentThemeName + textButtonPropertySelected);
                        writer.Write(currentThemeName + textButtonTillNavigation);
                        writer.Write(currentThemeName + textButtonTillNavigationManageClose);
                        writer.Write(currentThemeName + textButtonTillNumpad);
                        writer.Write(currentThemeName + textButtonTillQuick);
                        writer.Write(currentThemeName + textButtonEmptyProduct);
                        writer.Write(currentThemeName + textKeyboardKeys);
                        writer.Write(currentThemeName + textKeyboardNumpad);
                        writer.Write(currentThemeName + textKeyboardBackground);
                        writer.Write(currentThemeName + textScrollViewTheme);
                        writer.Write(currentThemeName + textToggleTheme);
                        writer.Write(currentThemeName + textComboBoxToggleTheme);
                        writer.Write(currentThemeName + textRectangleBackgroundTheme);
                        writer.Write(currentThemeName + textLabelDisplayBackgroundTheme);
                        writer.Write(currentThemeName + textTextBlockTheme);
                        writer.Write(currentLabelLoginScreenTimeColor + textLabelLoginScreenTimeColor);
                        writer.Write(currentThemeName + textTextBoxDisplayTheme);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeError);
                        writer.Write(currentThemeName + textTextBoxDisplayThemeSelected);
                        writer.Write(currentThemeName + currentButtonAdminImageTheme);//??
                        writer.Write(currentLoginBackgroundFileName);
                        writer.Write(imgLoginBackgroundFileName);
                        writer.Write(textBorderTheme);
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
                        currentButtonProperty = reader.ReadString();
                        currentButtonPropertySelected = reader.ReadString();
                        currentButtonTillNavigation = reader.ReadString();
                        currentButtonTillNavigationManageClose = reader.ReadString();
                        currentButtonTillNumpad = reader.ReadString();
                        currentButtonTillQuick = reader.ReadString();
                        currentButtonEmptyProduct = reader.ReadString();
                        currentKeyboardKeys = reader.ReadString();
                        currentKeyboardNumpad = reader.ReadString();
                        currentKeyboardBackground = reader.ReadString();
                        currentScrollViewTheme = reader.ReadString();
                        currentToggleTheme = reader.ReadString();
                        currentComboBoxToggleTheme = reader.ReadString();
                        currentRectangleBackgroundTheme = reader.ReadString();
                        currentLabelDisplayBackgroundTheme = reader.ReadString();
                        currentTextBlockTheme = reader.ReadString();
                        currentLabelLoginScreenTimeColor = reader.ReadString();
                        currentTextBoxDisplayTheme = reader.ReadString();
                        currentTextBoxDisplayThemeError = reader.ReadString();
                        currentTextBoxDisplayThemeSelected = reader.ReadString();
                        currentButtonAdminImageTheme = reader.ReadString();
                        currentLoginBackgroundFileName = reader.ReadString();
                        imgLoginBackgroundFileName = reader.ReadString();
                        currentBorderTheme = reader.ReadString();
                    }
                    string[] getLoginColor = currentLabelLoginScreenTimeColor.Split('=');
                    currentLabelLoginScreenTimeColor = getLoginColor[0];
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
                    var navTag = Convert.ToString(tillButton.Tag);
                    if (navTag == "TillNavigation")
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonTillNavigation];
                    }
                    else if (tillButton.Content.ToString() == string.Empty)
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonEmptyProduct];
                    }
                }
            }
            foreach (UIElement button in tillScreen.AdminNumbers.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    Button tillButton = (Button)button;
                    var tag = Convert.ToString(tillButton.Tag);
                    if (tag == "QuickPick")
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonTillQuick];
                    }
                    else if (tag == "TillNumpad")
                    {
                        tillButton.Style = (Style)Application.Current.Resources[currentButtonTillNumpad];
                    }
                }
            }
            tillScreen.bgAdminHeader.Style = (Style)Application.Current.Resources[currentLabelDisplayBackgroundTheme];
        }
    }
}
