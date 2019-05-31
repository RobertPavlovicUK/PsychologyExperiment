using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyExperiment
{
    public class ExpGenManager
    {
        public ExperimentGen gen;
        private static ExpGenManager _instance; 
        public ExpGenManager(ExperimentGen gen)
        {
            this.gen = gen;
            _instance = this;
        }
        public ExpGenManager()
        {
           
        }

        public void start() {

            

        }
        public static ExpGenManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ExpGenManager();
                return _instance;
            }
            return _instance;
                
        }
    }
}
