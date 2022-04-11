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
using System.Windows.Shell;
// Python Runtime Environment Reference
using Python.Runtime;

// Directories may vary on different machines
// For Setup pythonnet solution must be installed and pointing to python home
// Change Directories to match your machine's configurations
namespace GP_Fantasy_Pred
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public PyObject ScriptReturn, ErrMSE, Predictions;
        public int Budget;
        public Window1(PyObject ScriptReturn, PyObject ErrMSE, PyObject Predictions, int Budget)
        {
            this.Budget = Budget;
            this.ScriptReturn = ScriptReturn;
            InitializeComponent();
            Construct_Team();
            Draw_Players();
        }
        
        // Drawing Players Fit
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
            // CHANGE HERE ALL PATHES TO MATCH YOUR MACHINE'S PATHES
            Player.Source = new BitmapImage(new Uri("E:/Work/FCIS/4th Year/GP/Graduation-Project-FPL-Prediction/GP-Fantasy-Pred/GP-Fantasy-Pred/Background-Images/Player.png", UriKind.Absolute));
            Player.Margin = new Thickness(-835, 460, 0, 0);
            Player.Height = 50;
            Player.Width = 50;
            Sub1Grid.Children.Add(Player);
        }

        // Protoype of a Button Click's Effect
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // Constructing Team Using 0/1 Knapsack Algorithm
        public void Construct_Team()
        {
            // Algorithm Here
            

        }
        // Any Utility Functions

    }
}
