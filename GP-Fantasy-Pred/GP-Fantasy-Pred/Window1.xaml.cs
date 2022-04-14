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
            TextBlock PlayerName = new TextBlock(), Points = new TextBlock(), TeamName = new TextBlock();
            Image Player = new Image();
            
            // Textblocks Configs
            PlayerName.Background = Brushes.DarkGreen;
            TeamName.Background = Brushes.Green;
            Points.Background = Brushes.LightGreen;
            PlayerName.Foreground = Brushes.White;
            TeamName.Foreground = Brushes.White;
            Points.Foreground = Brushes.White;
            PlayerName.FontSize = 6;
            TeamName.FontSize = 6;
            Points.FontSize = 6;
            PlayerName.TextAlignment = TextAlignment.Center;
            TeamName.TextAlignment = TextAlignment.Center;
            Points.TextAlignment = TextAlignment.Center;
            PlayerName.VerticalAlignment = VerticalAlignment.Center;
            TeamName.VerticalAlignment = VerticalAlignment.Center;
            Points.VerticalAlignment = VerticalAlignment.Center;
            PlayerName.FontFamily = new FontFamily("Calibri");
            TeamName.FontFamily = new FontFamily("Calibri");
            Points.FontFamily = new FontFamily("Calibri");
            PlayerName.Text = "Lakaka";
            TeamName.Text = "Chelsea";
            Points.Text = "0";
            PlayerName.Width = 50;
            TeamName.Width = 50;
            Points.Width = 50;
            PlayerName.Height = 8;
            TeamName.Height = 8;
            Points.Height = 8;
            PlayerName.Margin = new Thickness(790, 420, 0, 0);
            TeamName.Margin = new Thickness(790, 434, 0, 0);
            Points.Margin = new Thickness(790, 448, 0, 0);

            // Players Images Configs
            Player.Source = new BitmapImage(new Uri("/Background-Images/chelsea.png", UriKind.Relative));
            Player.Margin = new Thickness(790, 345, 0, 0);
            Player.Height = 400;
            Player.Width = 400;

            Sub1Grid.Children.Add(PlayerName);
            Sub1Grid.Children.Add(TeamName);
            Sub1Grid.Children.Add(Points);
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
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
