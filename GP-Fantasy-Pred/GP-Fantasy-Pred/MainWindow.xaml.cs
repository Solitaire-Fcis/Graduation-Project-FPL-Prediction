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
using System.Windows.Navigation;
using System.Windows.Shapes;
// Python Runtime Environment Reference
using Python.Runtime;

namespace GP_Fantasy_Pred
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
        }
        // Code Execution Here
        public void PythonAPI()
        {
            // Optional Using
            using (Py.GIL())
            {
                PythonEngine.Initialize();
                PyObject ScriptReturn = PythonEngine.Compile("","../Script.py",RunFlagType.File);
            }
        }
        public void Draw_Players()
        {
            // Forward Players at X = -1100, Y = -300
            // Midfielders at X = -1100, Y = 0
            // Defenders at X = -1100, Y = 250
            // Goalkeeper at X = -835, Y = 460
            // Court Margins 
            // X1 = 25, Y1 = 0
            // X2 = 238+25, Y2 = 315
            // Above Measurements are for sure changed for better looks
            Image Player = new Image();
            Player.Source = new BitmapImage(new Uri("E:/Work/FCIS/4th Year/GP/Graduation-Project-FPL-Prediction/GP-Fantasy-Pred/GP-Fantasy-Pred/Background-Images/Player.png", UriKind.Absolute));
            Player.Margin = new Thickness(-835, 460, 0, 0);
            Player.Height = 50;
            Player.Width = 50;
            RootWindow.Children.Add(Player);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Draw_Players();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        // Any GUI Interaction Here 
    }
}

