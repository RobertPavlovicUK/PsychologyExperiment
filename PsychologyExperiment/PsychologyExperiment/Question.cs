using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyExperiment
{
 

    public class Question
    {
        public string questionID;
        public string question;
  
        public string answer;
        public string line;

        public Question()
        { }
        public Question(string line)
        {
            this.line = line;

        }

        public Question processQuestion()
        {
            Question question = new Question();

            string[] splitWord = line.Split(',');

            question.questionID = splitWord[0];
            question.question = splitWord[1];
       
            question.answer = splitWord[2];

            return question;
        }
        
    }
}
