using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychologyExperiment
{
   public interface FileNotifierListener
    {
     
        void OnDataRead(string line);
    }
}
