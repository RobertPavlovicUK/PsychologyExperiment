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

namespace PsychologyExperiment
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window,FileNotifierListener
    {
        public ExperimentGen gen;
        public List<string> settings;

        public LoadingWindow()
        {
            InitializeComponent();
         
            this.Loaded += LoadingWindow_Loaded;
           
        }

        private void LoadingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FileReader reader = new FileReader();
            reader.addListener(this);

            gen = new ExperimentGen(reader.ReadMathFile(), reader.ReadWords());
            settings = reader.ReadSettings();
            this.Close();
        }

        public void OnDataRead(string line)
        {
            this.Listbox.Items.Add(line);
        }

       

    }

}
