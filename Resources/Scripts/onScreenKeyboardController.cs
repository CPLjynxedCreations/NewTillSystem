using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NewTillSystem.Resources.Scripts
{
    internal class onScreenKeyboardController
    {
        //public string wordstring;
        public void GetKeyPressed(object sender)
        {
            Button btnClicked = (Button)sender;
            Debug.WriteLine(btnClicked);
            Debug.WriteLine(Convert.ToString(btnClicked.Content + " cont"));

            //add to string in window

        }
    }
}
