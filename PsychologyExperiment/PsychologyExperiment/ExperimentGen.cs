using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyExperiment
{
    public class ExperimentGen
    {
        public Queue<ExperimentRounds> rounds = new Queue<ExperimentRounds>();
        private Queue<Question> questionList;
        private Queue<Word> wordList;

        public ExperimentGen(Queue<Question> questionList, Queue<Word> wordList)
        {
            this.questionList = questionList;
            this.wordList = wordList;
            Queue<Question> questionList1 = new Queue<Question>();
            Queue<Word> wordList1 = new Queue<Word>();



            CreateExperimentRounds();
        }

        public void CreateExperimentRounds()
        {

            for (int i = 0; i < 4; i++)
            {
                rounds.Enqueue(new ExperimentRounds());
            }
            Boolean practice = true;
            while (wordList.Count != 0)
            {

                foreach (ExperimentRounds round in rounds)
                {
                    if (rounds.ElementAt(0) == round)
                    {
                        practice = true;

                    }
                    else
                    {
                        practice = false;
                    }

                    int currentTop = wordList.Peek().wordValue;
                    if (currentTop == 3)
                    {
                        int wordsVal = 5;
                        if (practice)
                        {
                            wordsVal = 1;
                        }
                        for (int i = 1; i < (wordsVal * 3) + 1; i++)
                        {
                            round.wordList.Enqueue(wordList.Dequeue());
                            if (i % 3 == 0)
                            {
                                round.questionList.Enqueue(questionList.Dequeue());
                            }
                        }

                    }
                    if (currentTop == 5)
                    {
                        int wordsVal = 5;
                        if (practice)
                        {
                            wordsVal = 1;
                        }

                        for (int i = 1; i < (wordsVal * 5) + 1; i++)
                        {
                            round.wordList.Enqueue(wordList.Dequeue());
                            if (i % 5 == 0)
                            {
                                round.questionList.Enqueue(questionList.Dequeue());
                            }
                        }
                    }

                    if (currentTop == 7)
                    {
                        int wordsVal = 5;
                        if (practice)
                        {
                            wordsVal = 1;
                        }

                        for (int i = 1; i < (wordsVal * 7) + 1; i++)
                        {
                            round.wordList.Enqueue(wordList.Dequeue());
                            if (i % 7 == 0)
                            {
                                round.questionList.Enqueue(questionList.Dequeue());
                            }
                        }
                    }

                }
            }

        }
    }
}
