using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for FinalPage.xaml
    /// </summary>
    public partial class FinalPage : Page
    {
        string ageS;
        string gender;
        string language;
        string currentFolder;
        public FinalPage(string currentFolder)
        {
            InitializeComponent();
            this.currentFolder = currentFolder;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            gender = radioButton.Content.ToString();
         
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
          
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sr = File.AppendText(Directory.GetCurrentDirectory() + "\\Files\\Experiments\\" + currentFolder + "\\" + "Feedback" + ".txt");
            sr.Write("-------------------------------------------------------------");
            sr.WriteLine("Gender: " + gender);
            sr.WriteLine("Age: " + age.Text.ToString());
            sr.WriteLine("Native speaker?: " + language);
            sr.Close();
            App.Current.MainWindow.Close();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var b = sender as TextBox;
            language = b.Text;

        }
    }
}
