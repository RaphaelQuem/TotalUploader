using Microsoft.Win32;
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
using TotalUploader.Controller;

namespace TotalUploader
{
    public partial class MainWindow : Window
    {
        UploadController controller = new UploadController();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.DefaultExt = ".xml";
            if (ofd.ShowDialog().Equals(true))
            {
                foreach (string file in ofd.FileNames)
                {
                    controller.EnviarNfe(System.IO.Path.GetDirectoryName(file), System.IO.Path.GetFileName(file));
                }
            }
        }
    }
}
