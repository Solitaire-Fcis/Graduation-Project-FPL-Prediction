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
        public static void PythonAPI()
        {
            // Optional Using
            using (Py.GIL())
            {
            }
        }
        // Any GUI Interaction Here 
    }
}

