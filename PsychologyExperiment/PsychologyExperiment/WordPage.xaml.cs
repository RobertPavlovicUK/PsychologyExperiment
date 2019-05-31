using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PsychologyExperiment
{
    /// <summary>
    /// Interaction logic for WordPage.xaml
    /// </summary>
    public partial class WordPage : Page
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        MediaPlayer player = new MediaPlayer();
        MediaPlayer beepPlayer;
        string result;
        private Queue<ExperimentRounds> rounds;
        private ExperimentRounds currentRound;
        private Question currentQuestion;
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private string currentWord;
        List<Scoretracker> scores = new List<Scoretracker>();
        Questionaire q1;
        Questionaire q2;
        Questionaire q3;
        Questionaire q4;
        Scoretracker score;
        private Queue<string> PositiveLocation = new Queue<string>();
        private Queue<string> NeutralLocation = new Queue<string>();
        private Queue<string> NegativeLocation = new Queue<string>();
        RadioButton r1;
        RadioButton r2;
        RadioButton r3;
        RadioButton r4;
        string beepLocation;
        private int MoodSelected;
        Boolean notPracitice = false;
        private string current;
        string fileName;
        public WordPage()
        {



        }

        public WordPage(Queue<ExperimentRounds> rounds, int MoodSelected)
        {
            this.MoodSelected = MoodSelected;
            beepLocation = Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\beep-07.mp3";
            NegativeLocation.Enqueue( Directory.GetCurrentDirectory() +"\\Files\\audoFiles\\Negative1-baby crying.m4a");
            NegativeLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\Negative2-man crying.m4a");
            NegativeLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\Negative3-women crying.m4a");
           

            NeutralLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\Neutral1-white noise.m4a");
            NeutralLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\Neutral2-pink noise.m4a");
            NeutralLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\Neutral3-brown noise.m4a");
           

            PositiveLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\positive1-children laughing.m4a");
            PositiveLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\Positive2-men laughing.m4a");
            PositiveLocation.Enqueue(Directory.GetCurrentDirectory() + "\\Files\\audoFiles\\Positive3-women laughing.m4a");
          

            InitializeComponent();
           box.Visibility = Visibility.Hidden;
            title.Content = "";
            @true.Visibility = Visibility.Hidden;
            @false.Visibility = Visibility.Hidden;
            record.Visibility = Visibility.Hidden;
            recordstop.Visibility = Visibility.Hidden;
            this.rounds = rounds;
            currentRound = rounds.Dequeue();
            
            current = DateTime.Now.Day.ToString()+"-"+DateTime.Now.Month.ToString()+";"+DateTime.Now.Hour+"-"+DateTime.Now.Minute;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.WorkerReportsProgress = true;

            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;

            Directory.CreateDirectory(Directory.GetCurrentDirectory()+"\\Files\\Experiments\\"+current);
           
                backgroundWorker.DoWork += (s, e) =>
                {

                    
                    currentWord = "";
                    int t = currentRound.wordList.Peek().wordValue;
                    for (int i = 0; i < t; i++)
                    {
                        Word word = currentRound.wordList.Dequeue();
                        string dequeword = word.word;
                        string itemID = word.wordID.ToString();
                        string itemValue = word.wordValue.ToString();
                        fileName = itemID + "-" + itemValue;
                        int s1 = currentRound.wordList.Count;
                        backgroundWorker.ReportProgress(0, dequeword);
                        Thread.Sleep(2000);

                    }

                };

            backgroundWorker1.DoWork += (s, e) =>
            {
              
             
                beepPlayer = new MediaPlayer();
                Thread.Sleep(500);
                beepPlayer.Open(new Uri(beepLocation));
                beepPlayer.Play();

            };
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;

            backgroundWorker.RunWorkerCompleted += (s, e) =>
            {
               
                displayQuestion();
            };



            runThread();

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            recordstop.Visibility = Visibility.Visible;
            RecordVoice();
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        private void displayQuestion()
        {
            currentQuestion = currentRound.questionList.Dequeue();
            if (notPracitice)
            {
               
                score = new Scoretracker();
                score.q = currentQuestion;
                scores.Add(score);
            }
            @true.Visibility = Visibility.Visible;
            @false.Visibility = Visibility.Visible;
            this.label.Content = currentQuestion.question;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.label.Content = e.UserState.ToString();
            title.Content = "";
        }

        private void False_Click(object sender, RoutedEventArgs e)
        {
            if (notPracitice)
            {
                score.userResponse = "False";
            }
            @true.Visibility = Visibility.Hidden;
            @false.Visibility = Visibility.Hidden;
            
            recordstop.Visibility = Visibility.Hidden;
            record.Visibility = Visibility.Visible;
            this.title.Content = "Please recall the words you remember in " +
               " \n the given order. \n (Speak out the words press Stop when you finish) ";
              
            this.label.Content = "";
            recordstop.Visibility = Visibility.Visible;
            record.Visibility = Visibility.Hidden;
            backgroundWorker1.RunWorkerAsync();
        }

        private void True_Click(object sender, RoutedEventArgs e)
        {
            if(notPracitice)
            {
                score.userResponse = "True";
            }
            @true.Visibility = Visibility.Hidden;
            @false.Visibility = Visibility.Hidden;
            
            recordstop.Visibility = Visibility.Hidden;
            record.Visibility = Visibility.Visible;
            this.title.Content = "Please recall the words you remember in " +
               " \n the given order. \n (Speak out the words press Stop when you finish) ";

            this.label.Content = "";
            recordstop.Visibility = Visibility.Visible;
            record.Visibility = Visibility.Hidden;
            backgroundWorker1.RunWorkerAsync();
        }

       
        private void RecordVoice()
        {


           
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
          

          
        
    }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            record.Visibility = Visibility.Hidden;
            runThread();

        }

        private void RecordStop_Click(object sender, RoutedEventArgs e)
        {
           
            string ss = Directory.GetCurrentDirectory()+ "\\Files\\Experiments\\"+current+"\\" + fileName + ".wav";
           
                int res = mciSendString("save recsound " + ss, "", 0, 0);
         
            mciSendString("close recsound ", "", 0, 0);
            record.Visibility = Visibility.Hidden;
            recordstop.Visibility = Visibility.Hidden;
            runThread();
           


        }
     

        public void runThread()
        {
            if (currentRound.wordList.Count != 0)
            {
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                if (notPracitice)
                {
                    title.Content = "";
                    label.Content = "";
                    box.Visibility = Visibility.Visible;
                }
                else {
                    PlaySound();
                }
            }
        }

        public void PlaySound()
        {
            if (rounds.Count != 0)
            {
                notPracitice = true;
                this.label.Content = "Listen to the Audio";
                this.title.Content = "Let’s step up to experimental section";
                player.MediaEnded += Player_MediaEnded;
                player.MediaOpened += Player_MediaOpened;
                player.MediaFailed += Player_MediaFailed;
                player.BufferingStarted += Player_BufferingStarted;
                player.BufferingEnded += Player_BufferingEnded;
             
               
                if (MoodSelected == 0)
                {

                    player.Open(new Uri(PositiveLocation.Dequeue()));

                    player.Play();
                }
                if (MoodSelected == 1)
                {

                    player.Open(new Uri(NeutralLocation.Dequeue()));
                    player.Play();
                }
                if (MoodSelected == 2)
                {
                    player.Open(new Uri(NegativeLocation.Dequeue()));
                    player.Play();

                }
             
            }
            else
            {
                App.Current.MainWindow.Content = new FinalPage(current);
            }
        }
        private void Player_BufferingEnded(object sender, EventArgs e)
        {
            int t;
        }

        private void Player_BufferingStarted(object sender, EventArgs e)
        {
            int tt;
        }

        private void Player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
         
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            currentRound = rounds.Dequeue();
            
               label.Content = "another round";
            
            title.Content = "After listening to the audio" +
               " \n Please remember the following" +
               "\n words in the given order";
            record.Visibility = Visibility.Visible;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            r1=  sender as RadioButton;
            if (r1 == null)
                return;
            int intIndex = Convert.ToInt32(r1.Content.ToString());
           q1 = new Questionaire();
            q1.question = Q1.Content.ToString();
            q1.answer = intIndex;
         
          

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
             r2 = sender as RadioButton;
            if (r2 == null)
                return;
            int intIndex = Convert.ToInt32(r2.Content.ToString());
          q2 = new Questionaire();
            q2.question = Q2.Content.ToString();
            q2.answer = intIndex;
         
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            r3 = sender as RadioButton;
            if (r3 == null)
                return;
            int intIndex = Convert.ToInt32(r3.Content.ToString());
           q3 = new Questionaire();
            q3.question = Q3.Content.ToString();
            q3.answer = intIndex;
          
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
             r4 = sender as RadioButton;
            if (r4 == null)
                return;
            int intIndex = Convert.ToInt32(r4.Content.ToString());
            q4 = new Questionaire();
            q4.question = Q4.Content.ToString();
            q4.answer = intIndex;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (r1 != null && r2 != null && r3 != null && r4 != null)
            {
                r1.IsChecked = false;
                r2.IsChecked = false;
                r3.IsChecked = false;
                r4.IsChecked = false;
            }
            box.Visibility = Visibility.Hidden;
            PlaySound();

            StreamWriter sr = File.AppendText(Directory.GetCurrentDirectory() + "\\Files\\Experiments\\" + current + "\\" +"Feedback" + ".txt");

            foreach (Scoretracker item in scores)
            {
                sr.WriteLine("Question: " + item.q.question + " Correct: " + item.q.answer + " User: " + item.userResponse);
            }
            scores.Clear();
            sr.WriteLine(q1.question + " : " + q1.answer);
            sr.WriteLine(q2.question + " : " + q2.answer);
            sr.WriteLine(q3.question + " : " + q3.answer);
            sr.WriteLine(q4.question + " : " + q4.answer);
            sr.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            sr.Close();
        }
    }


}
