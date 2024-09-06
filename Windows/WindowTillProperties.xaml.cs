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

    public partial class WindowTillProperties : Window
    {
        ThemeController themeController;
        public string theme;
        public string selectedLoginBackground;

        public WindowTillProperties()
        {
            InitializeComponent();
            themeController = new ThemeController();
            themeController.ReadTheme();
            theme = themeController.currentThemeName;
            selectedLoginBackground = themeController.imgLoginFileName;
            

            if (theme == "Default")
            {
                boxSelectTheme.SelectedItem = boxThemeSelectDefualt;
            }
            if (theme == "LightBlue")
            {
                boxSelectTheme.SelectedItem = boxThemeSelectLightBlue;
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (boxSelectTheme.Text != string.Empty)
            {
                themeController.currentThemeName = boxSelectTheme.Text;
                themeController.UpdateThemeFile();
            }
            themeController.imgLoginFileName = selectedLoginBackground;
            Debug.WriteLine("theme " + themeController.imgLoginFileName);
            themeController.ReadTheme();
            themeController.SetTheme();
        }

        private void LoginBackground_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            selectedLoginBackground = Convert.ToString(button.Name);
            themeController.imgLoginFileName = selectedLoginBackground;
            themeController.UpdateThemeFile();
        }
    }
}
