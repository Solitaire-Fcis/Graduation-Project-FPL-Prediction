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
        public PyObject ScriptReturn, Predicted_Points, Team, Substitutions, Captain, Cost;
        public int Budget, Gameweek;
        public MainWindow()
        {
            InitializeComponent();
        }

        // Python API for Executing Models and Metrics
        public void PythonAPI()
        {
            Directory.SetCurrentDirectory("../../../PythonScripts");
            //CHANGE HERE PATH OF PYTHON DLL TO MATCH YOUR MACHINE'S PATH
            Runtime.PythonDLL = @"C:/python39/python39.dll";
            PythonEngine.Initialize();
            using (Py.GIL())
            {

                this.Budget = int.Parse(textbox1.Text);
                this.Gameweek = int.Parse(textbox2.Text);
                PyInt BUD = new PyInt(this.Budget), GW = new PyInt(this.Gameweek);
                PyObject[] para = new PyObject[2];
                para[0] = GW;
                para[1] = BUD;
                PyList arg = new PyList();
                arg.Append(GW);
                arg.Append(BUD);
                dynamic os = Py.Import("os");
                dynamic sys = Py.Import("sys");
                sys.path.append(os.path.dirname(os.path.expanduser("Script.py")));
                sys.argv += arg;
                var fromFile = Py.Import(System.IO.Path.GetFileNameWithoutExtension("Script.py"));
                this.ScriptReturn = fromFile;
                this.Predicted_Points = fromFile.GetAttr("predicted_score");
                this.Team = fromFile.GetAttr("teams_list");
                this.Substitutions = fromFile.GetAttr("subs_list");
                this.Captain = fromFile.GetAttr("captains_list");
                this.Cost = fromFile.GetAttr("total_cost");
            }
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PythonAPI();
            Window1 Pred = new Window1(ScriptReturn, Predicted_Points, Team, Substitutions, Captain, Cost, Budget);
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

