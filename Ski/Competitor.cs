using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ski
{
    public abstract class Competitor
    {
        #region Data
        //All of the competitor variables that will need taking. 
        protected string ID;       
        protected string Name;
        protected string Number;
        protected string Address;
        protected string Age;
        protected string Score;
        protected string Sponsor;
        protected string Blood;
        protected string Kin;
        #endregion

        #region Competitor Constructor
        //The main constructor for all of the competitors. They will all use this and then go to children classes for their specific variables. 
        public Competitor(string inID, string inName, string inNumber, string inAddress, string inAge, string inScore)
        {
            ID = inID;
            Name = inName;
            Number = inNumber;
            Address = inAddress;
            Age = inAge;
            Score = inScore;
        }
        #endregion

        #region Save Competitor
        public virtual void Save(StreamWriter outStream)  //Saves the inforamtion about each competitor. 
        {
            outStream.WriteLine(Name);    //Saves the name.
            outStream.WriteLine(Number);  //Saves the number.
            outStream.WriteLine(Address); //Saves the address.
            outStream.WriteLine(Age);     //Saves the age.
            outStream.WriteLine(Score);   //Saves the score. 
        }

        public void Save(string filename) //Saves the information to a file which I name in the Main Window class. 
        {
            StreamWriter textOut = null;               //Makes a new StreamWriter. 
            try
            {
                textOut = new StreamWriter(filename);  //Makes textOut = the inforamtion to write to the file. 
                Save(textOut);                         //Gets the information and saves it to the file. 
            }
            catch (Exception e)                        //If something goes wrong we throw an exception. 
            {
                throw e;
            }
            finally                                    //The finally clause runs no matter what the outcome of the try-catch is. If textOut = null then it'll close. 
            {
                if (textOut != null) textOut.Close();
            }
        }
        #endregion

        #region Load Competitor
        public Competitor(StreamReader inStream)  //Loads in all of the inforamtion about each competitor. 
        {
            Name = inStream.ReadLine();    //Loads in the name. 
            Number = inStream.ReadLine();  //Loads in the number.
            Address = inStream.ReadLine(); //Loads in the address.
            Age = inStream.ReadLine();     //Loads in the age.
            Score = inStream.ReadLine();   //Loads in the score. 
        }    
        #endregion

        #region "Get" data
        public string GetID()
        {
            return ID;
        }        //Gets competitor ID. (Not used but kept for later use if needed). 
        public string GetNumber()
        {
            return Number;
        }    //Gets competitor number.
        public string GetAddress()
        {
            return Address;
        }   //Gets competitor address.
        public string GetName()
        {
            return Name;
        }      //Gets competitor name.
        public string GetAge()
        {
            return Age;
        }       //Gets competitor age.
        public string GetScore()
        {
            return Score;
        }     //Gets competitor score.
        public string GetSponsor()
        {
            return Sponsor;
        }   //Gets competitor sponsor.   
        public string GetBlood()
        {
            return Blood;
        }     //Gets competitor blood type.
        public string GetNoK()
        {
            return Kin;
        }       //Gets competitor next of kin. 
        #endregion
    }

    #region Child Constructors
    public class Amateur : Competitor       //Makes a child class of the main "Competitor" class specifically for amateur competitors. 
    {
        //The constructor for an amateur competitor. 
        public Amateur(string AmaID, string AmaName, string AmaNumber, string AmaAddress, string AmaAge, string AmaScore)
            : base(AmaID, AmaName, AmaNumber, AmaAddress, AmaAge, AmaScore)
        {
            //We don't need any extras becasue the main constructor is exactly the same as parent constructors. 
        }
        public Amateur(StreamReader textIn)
            : base(textIn)
        {
            //We do not need to load anything extra becasue the child class is exactly the same as the parent class. 
        }
    }

    public class Professional : Competitor  //Makes a child class of the main "Competitor" class specifically for professional competitors. 
    {
        //The constructor for a professional competitor. 
        public Professional(string ProID, string ProName, string ProNumber, string ProAddress, string ProAge, string ProScore, string ProSponsor)
            : base(ProID, ProName, ProNumber, ProAddress, ProAge, ProScore)
        {

            Sponsor = ProSponsor;
        }
        public override void Save(StreamWriter outStream)  //Override save method because we need to save a sponsor as well as everything else in the parent class. This is so we dont have to use many different save methods. 
        {
            base.Save(outStream);          //Saves all of the universal data. 
            outStream.WriteLine(Sponsor);  //Saves a sponsor too.
        }
        public Professional(StreamReader textIn)  //The unique load for the professional. 
            : base(textIn)
        {
            Sponsor = textIn.ReadLine();   //Loads back in the sponsor when loading a professional. 
        }
    }

    public class Celebrity : Competitor    //Makes a child class of the main "Competitor" class specifically for celebrity competitors. 
    {
        //The constructior for a celebrity competitor. 
        public Celebrity(string CelID, string CelName, string CelNumber, string CelAddress, string CelAge, string CelScore, string CelBlood, string CelNoK)
            : base(CelID, CelName, CelNumber, CelAddress, CelAge, CelScore)
        {
            Blood = CelBlood;
            Kin = CelNoK;
        }
        public override void Save(StreamWriter outStream)  //Override save method because we need the blood type and next of kin as well as everything in the parent class. 
        {
            base.Save(outStream);          //Saves all of the universal data.
            outStream.WriteLine(Blood);    //Saves the blood type.
            outStream.WriteLine(Kin);      //Saves the next of kin. 
        }
        public Celebrity(StreamReader textIn)  //The unique load for the celebrity. 
            : base(textIn)
        {
            Blood = textIn.ReadLine();  //Loads back in the blood type when loading a celebrity. 
            Kin = textIn.ReadLine();    //Loads back in the next of kin when loading a celebrity. 
        }
    }
    #endregion
}
