using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PsychologyExperiment
{
   
    public class FileReader
    {
        FileNotifierListener listener;
      
        private string filePath = "/Files";
        private StreamReader sr;
        public FileReader() {

        }

        public void addListener(FileNotifierListener listener)
        {
            this.listener = listener;
        }

        public Queue<Word> ReadWords()
        {
            sr = new StreamReader(Directory.GetCurrentDirectory() + "/" + filePath + "/word_list.csv");

            string line;
            Queue<Word> fileContent = new Queue<Word>();
            
            listener.OnDataRead("Loading word_list...");
            while ((line = sr.ReadLine()) != null)
            {
                if (!line.StartsWith("["))
                {

                    fileContent.Enqueue(new Word(line).proccessWord());
                }
            }
            listener.OnDataRead("Word_list succesfully loaded");
            sr.Close();
            return fileContent;
        }

        public List<string> ReadSettings()
        {
            sr = new StreamReader(Directory.GetCurrentDirectory() + "/" + filePath + "/settings.txt");
           
            string line;
            List<string> fileContent = new List<string>();
         
            listener.OnDataRead("Loading ExperimentSettings...");
            while ((line = sr.ReadLine()) != null)
            {
                if (!line.StartsWith("["))
                {
                    fileContent.Add(line);
                }
            }
            listener.OnDataRead("ExperimentSettings succesfully loaded");
            sr.Close();
            return fileContent;
        }

        public Queue<Question> ReadMathFile()
        {
            sr = new StreamReader(Directory.GetCurrentDirectory()+"/"+filePath+"/math_file.csv");
            listener.OnDataRead("Loading math_file...");
            string line;
            Queue<Question> fileContent = new Queue<Question>();
         
            while( (line = sr.ReadLine()) != null)
            {
               
                Console.WriteLine(line);
             
                fileContent.Enqueue(new Question(line).processQuestion());
                 
                
            }
            listener.OnDataRead("math_file succesfully loaded");
            sr.Close();
            return fileContent;
        }
    }

}
