using System;
using System.Collections.Generic;
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
using System.IO;
using Python.Runtime;

namespace GP_Fantasy_Pred
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public PyObject ScriptReturn;
        public int Budget;
        public Window1(PyObject ScriptReturn, int Budget)
        {
            this.Budget = Budget;
            this.ScriptReturn = ScriptReturn;
            InitializeComponent();
        }
        // Drawing Players on Football Field
        public void Draw_Players()
        {
            // NUMBERS HAVE CHANGED IN ACCORDANCE TO NEW GUI SETTING
            // Forward Players at X = -1100, Y = -300
            // Midfielders at X = -1100, Y = 0
            // Defenders at X = -1100, Y = 250
            // Goalkeeper at X = -835, Y = 460
            // Court Margins 
            // X1 = 25, Y1 = 0
            // X2 = 238+25, Y2 = 315
            // Above Measurements are for sure changed for better looks
            Image Player = new Image();
            Player.Source = new BitmapImage(new Uri("E:/My Study/Graduation Project/Graduation-Project-FPL-Prediction/GP-Fantasy-Pred/GP-Fantasy-Pred/Background-Images/Player.png", UriKind.Absolute));
            Player.Margin = new Thickness(-835, 460, 0, 0);
            Player.Height = 50;
            Player.Width = 50;
            PredWindow.Children.Add(Player);
            return;
        }
        // Constructing Team Using 0/1 Knapsack Algorithm
        public void Construct_Team()
        {
            // Algorithm Here

            // Call For Drawing Players Fit
            Draw_Players();
            return;
        }
        // Any Utility Functions
        
    }
}
