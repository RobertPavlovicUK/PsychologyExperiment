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

namespace PsychologyExperiment
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private ExperimentGen gen;
        private List<string> settings;

        public Page1()
        {
            InitializeComponent();
        }

        public Page1(ExperimentGen gen, List<string> settings)
        {
            InitializeComponent();
            this.gen = gen;
            this.settings = settings;

            this.label.Text = settings.ElementAt<string>(0) + settings.ElementAt<string>(1);
            this.label.IsEnabled = false;
            this.label.Foreground = new SolidColorBrush(Colors.Black);
            this.label1.Text = settings.ElementAt<string> (2);
            label1.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QuestionPage p = new QuestionPage(gen.rounds);
            App.Current.MainWindow.Content = p;
        }
    }
}
