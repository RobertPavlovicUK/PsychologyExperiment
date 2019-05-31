using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyExperiment
{

    public class ExperimentRounds
    {
        public Queue<Question> questionList;
        public Queue<Word> wordList;

        public ExperimentRounds()
        {
            wordList = new Queue<Word>();
            questionList = new Queue<Question>();
        }
        public ExperimentRounds(Queue<Question> questionList, Queue<Word> wordList)
        {
            this.questionList = questionList;
            this.wordList = wordList;
        }

       

    }
}
