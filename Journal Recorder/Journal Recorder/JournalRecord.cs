using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal_Recorder
{
    public class JournalRecord
    {
        public int journalsCount;
        public string category;
        public string date;
        public string journal;
       

        public string displayText()
        {
            if (date.Length == 9)
                return date + "\t" + journal;
            else
                return date + "\t\t" + journal;
        }

    }
}

