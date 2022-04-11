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
        public PyObject ScriptReturn;
        public int Budget;
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
                ScriptReturn = PythonEngine.Compile("","../Script.py",RunFlagType.File);
                PythonEngine.Shutdown();
            }
        }
        public void Draw_Players()
        {
            return;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PythonAPI();
            this.Budget = int.Parse(textbox1.Text);
            Window1 Pred = new Window1(ScriptReturn, Budget);
            this.Hide();
            Pred.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        // Any GUI Interaction Here 
    }
}

