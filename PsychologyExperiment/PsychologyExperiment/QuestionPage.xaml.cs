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
    /// Interaction logic for QuestionPage.xaml
    /// </summary>
    public partial class QuestionPage : Page
    {
       
        private Queue<ExperimentRounds> rounds;

        public QuestionPage()
        {
          
        }

        public QuestionPage(Queue<ExperimentRounds> rounds)
        {
            InitializeComponent();
            this.label.Content = "Please remember the words in their " +
                "\n given order. (Each word will be shown for 2 seconds.)";

            this.rounds = rounds;
        }

     

        private void Positive_Click(object sender, RoutedEventArgs e)
        {
            WordPage p = new WordPage(rounds,0);
            App.Current.MainWindow.Content = p;
        }

        private void Neutral_Click(object sender, RoutedEventArgs e)
        {
            WordPage p = new WordPage(rounds, 1);
            App.Current.MainWindow.Content = p;
        }

        private void Nega_Click(object sender, RoutedEventArgs e)
        {
            WordPage p = new WordPage(rounds, 2);
            App.Current.MainWindow.Content = p;
        }
    }
}
