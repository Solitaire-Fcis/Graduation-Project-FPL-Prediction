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
using System.IO;
using System.Windows.Shell;
// Python Runtime Environment Reference
using Python.Runtime;


namespace GP_Fantasy_Pred
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PyObject ScriptReturn, ErrMSE, Predictions;
        public int Budget;
        public MainWindow()
        {
            InitializeComponent();
        }
        // Python API for Executing Models and Metrics
        public void PythonAPI()
        {
            Directory.SetCurrentDirectory("../../../");
            //CHANGE HERE PATH OF PYTHON DLL TO MATCH YOUR MACHINE'S PATH
            Runtime.PythonDLL = @"C:/python39/python39.dll";
            PythonEngine.Initialize();
            using (Py.GIL())
            {
                dynamic os = Py.Import("os");
                dynamic sys = Py.Import("sys");
                sys.path.append(os.path.dirname(os.path.expanduser("Script.py")));
                var fromFile = Py.Import(System.IO.Path.GetFileNameWithoutExtension("Script.py"));
                this.ScriptReturn = fromFile;
                this.ErrMSE = fromFile.GetAttr("err");
                this.Predictions = fromFile.GetAttr("prediction");
            }
            PythonEngine.Shutdown();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PythonAPI();
            this.Budget = int.Parse(textbox1.Text);
            Window1 Pred = new Window1(ScriptReturn,ErrMSE,Predictions, Budget);
            this.Hide();
            Pred.Show();
        }

        // Any GUI Interaction Here 
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}

