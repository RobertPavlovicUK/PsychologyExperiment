using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyExperiment
{
    public class Word
    {
        public int wordID;
        public int wordValue;
        public string word;
        private string line;
        public Word()
        {
            
        }
        public Word(string line)
        {
            this.line = line;
        }

        public Word proccessWord()
        {
            Word word = new Word();

            string[] splitWord = line.Split(',');

            word.wordID = int.Parse( splitWord[0]);
            word.wordValue= int.Parse(splitWord[1]);
            word.word = splitWord[2];

            return word;
        }
    }

    
}
