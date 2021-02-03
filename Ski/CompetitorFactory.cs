using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski
{
    class CompetitorFactory  //The CompetitorFactory class (from the yellow book) makes the competitors from the loaded data. 
    {
        public static Competitor MakeCompetitor(string ID, StreamReader textIn)  //A single class takes in the competitor ID and the information so we know who to create. 
        {
            switch (ID)
            {
                case "Amateur":
                    return new Amateur(textIn);       //Returns an amateur competitor. 

                case "Professional":
                    return new Professional(textIn);  //Returns a professional competitor.

                case "Celebrity":
                    return new Celebrity(textIn);     //Returns a celebrity competitor. 

                default:
                    return null;                      //Returns null if something goes wrong. 
            }
        }
    }
}
