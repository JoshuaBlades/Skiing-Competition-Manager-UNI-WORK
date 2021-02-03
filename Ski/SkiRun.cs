using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ski
{
    public class SkiRun
    {
        private string skiName;

        private List<Tuple<int, string>> topAmateurScoresList = new List<Tuple<int, string>>(3);      //List containing the top amateur scores.
        private List<Tuple<int, string>> topProfessionalScoresList = new List<Tuple<int, string>>(3); //List containing the top professional socres.
        private List<Tuple<int, string>> topCelebrityScoresList = new List<Tuple<int, string>>(3);    //List containing the top celebrity scores.
        public List<string> SearchByName = new List<string>();                                        //List containing the names matching the user search. 

        private static Dictionary<string, Competitor> skiCompetitors = new Dictionary<string, Competitor>();  //The main competitor dictionary. 

        public SkiRun(string newSkiName)  //The constrctor for the SkiRunLodge. 
        {
            skiName = newSkiName;  //Sets the name. 
            skiCompetitors = new Dictionary<string, Competitor>();  //Creates a dictionary.
        }

        #region Competitor Number/Income/Scores Data
        static int newSkiNumber = 0;           //Gives the competitor their number.
        static int Income;                     //Sets an income variable so calculate total income. 
        static int Scores = 0;                 //Sets an scores variable so calculate total scores. 
        static string ConvertedAddedScores;    //Sets the scores int as a string. 
        #endregion

        #region Reports Data
        public string IncomeValues()  //Returns the Income value.
        {
            return Income.ToString();  //Truns the int into a string. 
        }

        public int GetSkiNumber()  //Returns the Competitor number count.
        {
            return (newSkiNumber);  //Retuns the amount of competitors. 
        }

        public void ClearData()  //Clears the dictionary and the top scores lists 
        {
            skiCompetitors.Clear();             //Clears dictionary.
            topAmateurScoresList.Clear();       //Clears top amateur scores list.
            topProfessionalScoresList.Clear();  //Clears top professional scores list.
            topCelebrityScoresList.Clear();     //Clears top celebrity scores list. 
        }

        public void ClearCompData()  //Clears competitor data in the SkiRun class. 
        { 
            skiCompetitors.Clear();             //Clears dictionary.   
            topAmateurScoresList.Clear();       //Clears top amateur scores list.
            topProfessionalScoresList.Clear();  //Clears top professional scores list.
            topCelebrityScoresList.Clear();     //Clears top celebrity scores list. 
            newSkiNumber = 0;                   //Resets the competitor number.
            Scores = 0;                         //Resets the scores back to 0.
            ConvertedAddedScores = "0";         //Resets the scores string to "0".
            Income = 0;                         //Resets the income ack to 0. 

        }

        public string TotalScores()  //Calculates the total score. 
        {
            try
            {
                foreach (Competitor TotalScores in skiCompetitors.Values)  //Foreach competitor in the dictionary we take their score and add it. 
                {
                    string TotalAddedScores = TotalScores.GetScore();      //Create a varabale makes it easier to see what we're doing. 
                    Scores = Scores + int.Parse(TotalAddedScores);         //Parse the string into an integer. 
                    ConvertedAddedScores = Scores.ToString();              //Turn the added scores back into a string. 
                }
            }
            catch
            {
            }

            Scores = 0;                        //Sets the score back to 0. 

            if (ConvertedAddedScores == null)  //If the scores total is null we can return a string. 
            {
                ConvertedAddedScores = "0";    //Returns "0". Makes the application look more consistent. 
            }

            return ConvertedAddedScores;       //Returns the total scores. 
        }
        #endregion

        #region Age Outputs
        public List<int> CompetitorAges = new List<int>();  //Makes  a list of ages. 

        static string ConvertedAddedAges;  //String for the added ages. 

        static int AgeCounter = 0;  //A counter for how many competitors have an age. 

        int AveAgeCounter;  //Average age counter.

        public void LoadInAges()  //Loads in the ages into the list. 
        {
            for (int i = 1; i <= skiCompetitors.Count; i++)  //For loop to go around the dictionary to get the competitors. Starts at 1 because there is no competitor "0". 
            {
                Competitor editSkier = FindSkier(i.ToString());  //Finds the competitors. 

                try
                {
                    int Ages = int.Parse(editSkier.GetAge());  //Parses their age intop an integer. 
                    CompetitorAges.Add(Ages);  //Adds it to the list. 
                    CompetitorAges.Sort();     //Sorts the list. 
                }
                catch
                {
                }
            }
        }

        public string MinAge()  //Returns the minimum age. 
        {
            try
            {
                string OutScore = CompetitorAges.First().ToString();  //Gets the first value in the ages list.
                return OutScore;                                      //Returns the first value. 
            }
            catch
            {
                return "NO DATA";  //If there is no value it will return "NO DATA". 
            }
        }

        public string MaxAge()  //Returns the maximum age. 
        {
            try
            {
                string OutScore = CompetitorAges.Last().ToString();  //Gets the last value in the ages list. 
                return OutScore;                                     //Returns the last value. 
            }
            catch
            {
                return "NO DATA";  //If there is no value it will return "NO DATA".
            }
        }

        public string AveAge()  //Returns the average age. 
        {

            try
            {
                foreach (Competitor TotalAges in skiCompetitors.Values)  //Foreach loop for each competitor in the dictionary.
                {
                    string TotalAddedAges = TotalAges.GetAge();  //Get their age and set to a new variable making it easier to understand. 
                    if (TotalAddedAges != "")  //If there is a score we increment the counter by 1.
                    {
                        AveAgeCounter++;  //Increments the counter by 1.
                    }
                    if (TotalAddedAges == "")  //If there is no age we make the variable equal "0".
                    {
                        TotalAddedAges = "0";  
                    }
                    AgeCounter = AgeCounter + int.Parse(TotalAddedAges);  //Parse the total added ages into an integer. 
                }
            }
            catch
            {
            }
            try
            {
                int AveAge = AgeCounter / AveAgeCounter;  //We take the added ages / how many people have an age. 
                ConvertedAddedAges = AveAge.ToString();   //Convert into a string.
                AgeCounter = 0;                           //Set the added ages back to 0.
                AveAgeCounter = 0;                        //Sets the counter back to 0.
                return ConvertedAddedAges;                //Returns the average age. 
            }
            catch
            {
                return "NO DATA";  //If there is no value it will return "NO DATA".
            }
        }

        public string ModeAge()  //Returns the mode age. 
        {

            var modeNumber = new Dictionary<int, int>();           //Makes a dictionary where we can hold the ages.
            foreach (Competitor ModeAge in skiCompetitors.Values)  //Foreach loops for each competitor in the dictionary.
            {
                int count;                            //Make an integer called count 
                string inModeAge = ModeAge.GetAge();  //Get the competitor age.
                if (inModeAge == "")                  //If the age does not have a value then we forget about it. 
                {
                    continue;                         //We leave this competitor and try the next one. 
                }

                int Mode = int.Parse(inModeAge);          //We parse the inputted age.
                modeNumber.TryGetValue(Mode, out count);  //Instead of using 'contains' we add the number as the key and get the count. 
                count++;                                  //We increment the count by 1 as this is the amount of times the number came up. 
                modeNumber[Mode] = count;                 //We match the number with the count. 
            }

            int mostCommonNumber = 0;         //Our most common number 
            int occurrences = 0;              //How many times that number came up.
            foreach (var pair in modeNumber)  //Foreach pair in the modeNumber dictionary we check the occurrences.
            {
                if (pair.Value > occurrences)  //If the number is greater than the occurrences when we can take it as the most common number. 
                {
                    occurrences = pair.Value;     //Occurrences becomes the number of times it came up. 
                    mostCommonNumber = pair.Key;  //The most common number becomes the key. 
                }
            }
            if (mostCommonNumber.ToString() == "0")
            {
                return "NO DATA";
            }
            return mostCommonNumber.ToString();
        }
        #endregion

        #region Making Competitors
        public static Competitor AddAmateurCompetitor(string inID, string inName, string inAddress, string inAge, string inScore)  //Makes our amateur competitor. 
        {
            newSkiNumber = newSkiNumber + 1;  //Increments the competitor number by 1.
            string newCompetitorNumberString = newSkiNumber.ToString();  //Turns that int into a string. 
            Amateur newAmateur = new Amateur(inID, inName, newCompetitorNumberString, inAddress, inAge, inScore);  //Calls the amateur constructor in the Competitor Class. 
            skiCompetitors.Add(newCompetitorNumberString, newAmateur);  //Adds the competitor to the dictionary.
            Income = Income + 100;  //Amateurs have to pay £100.
            return newAmateur;  //Returns the amateur. 
        }

        public static Competitor AddProfessionalCompetitor(string inID, string inName, string inAddress, string inAge, string inScore, string inSponsor)  //Makes our professsional competitor. 
        {
            newSkiNumber = newSkiNumber + 1;  //Increments the competitor number by 1.
            string newCompetitorNumberString = newSkiNumber.ToString();  //Turns that int into a string.
            Professional newProfessional = new Professional(inID, inName, newCompetitorNumberString, inAddress, inAge, inScore, inSponsor);  //Calls the professional constructor in the Competitor Class. 
            skiCompetitors.Add(newCompetitorNumberString, newProfessional);  //Adds the competitor to the dictionary.
            Income = Income + 200;  //Professionals have to pay £200.
            return newProfessional;  //Returns the professional.
        }

        public static Competitor AddCelebrityCompetitor(string inID, string inName, string inAddress, string inAge, string inScore, string inBlood, string inKin)  //Makes out celebrty competitor. 
        {
            newSkiNumber = newSkiNumber + 1;  //Increments the competitor number by 1.
            string newCompetitorNumberString = newSkiNumber.ToString();  //Turns that int into a string. 
            Celebrity newCelebrity = new Celebrity(inID, inName, newCompetitorNumberString, inAddress, inAge, inScore, inBlood, inKin);  //Calls the celebrity constructor in the Competitor Class. 
            skiCompetitors.Add(newCompetitorNumberString, newCelebrity);  //Adds the competitor to the dictionary.
            return newCelebrity;  //Returns the celebrity.
        }
        #endregion

        #region Find Competitors
        public static Competitor FindSkierByName(string inName)  //Finds the competitor by their name.
        {
            foreach (KeyValuePair<string, Competitor> competitor in skiCompetitors)  //Foreach pair in the dictionary 
            {
                if (competitor.Value.GetName().Contains(inName))  //If the name value contains any of the user inputted name then we say it's a match.
                {
                    return competitor.Value;  //Returns the competitor.
                }
            }
            return null;  //We return null if we couldnt find anyone.
        }

        public static Competitor FindSkier(string competitorNumber)  //Find the competitor by number.
        {
            if (!skiCompetitors.ContainsKey(competitorNumber))  //If the dictionary does not contain the competitor number it returns nothing.
            {
                return null;  //Returns null.
            }

            return skiCompetitors[competitorNumber];  //Else we return that competitor. 

        }

        public List<string> CheckingNames(string NameCheck)  //A list of all of the competitors who match the name input. 
        {
            SearchByName.Clear();  //Clear the list.

            foreach (KeyValuePair<string, Competitor> competitor in skiCompetitors)  //For each pair in the dictionary.
            {
                if (competitor.Value.GetName().Contains(NameCheck))  //If the user input contains part of a name we can then add it to a list.
                {
                    SearchByName.Add(competitor.Value.GetName());  //Add the competitor to the list. 
                }
            }
            return SearchByName;  //Returns the list. 
        }
        #endregion

        #region Save Competitors
        public void Save(StreamWriter outStream)  //Saves our competitor data.
        {
            outStream.WriteLine(skiName);               //Writes out the name of the SkiRunLodge.
            outStream.WriteLine(newSkiNumber);          //Writes out how many competitors there are.
            outStream.WriteLine(Income);                //Writes out the income.
            outStream.WriteLine(skiCompetitors.Count);  //Writes out how many competitors are in the dictionary. 

            foreach (Competitor saveCompetitor in skiCompetitors.Values)  //For each comeptitor in the dictionary.
            {
                outStream.WriteLine(saveCompetitor.GetType().Name); //Get competitor class.
                saveCompetitor.Save(outStream);  //Saves the inforamtion. 
            }
        }

        public void Save(string filename)  //Saving to the file.
        {
            StreamWriter textOut = null;  //Creates a StreamWriter.

            try
            {
                textOut = new StreamWriter(filename);  //textOut equals the filename we have chosen in the MainWindow.
                Save(textOut);
            }
            catch (Exception e)  //If it fails we throw an exception. 
            {
                throw e;
            }
            finally
            {
                if (textOut != null) textOut.Close();  //Finally we close. 
            }
        }
        #endregion

        #region Load Competitors
        public static SkiRun Load(StreamReader inStream)  //Loads our competitors. 
        {
            string skiName = inStream.ReadLine();                 //Reads in the SkiRunLodge name. 
            SkiRun result = new SkiRun(skiName);                  //Sets up our SkiRun inforamtion. 
            newSkiNumber = int.Parse(inStream.ReadLine());        //Inputs our competitor numbers. 
            int inIncome = int.Parse(inStream.ReadLine());        //Parses our income string into an int
            Income = inIncome;                                    //Makes our Income value equal the original variable. 
            int numberOfSkiers = int.Parse(inStream.ReadLine());  //The dictionary count is used to recreate everyone. 

            for (int i = 0; i < numberOfSkiers; i++)  //for loop goes round for every competitor. 
            {
                string ID = inStream.ReadLine();  //Reads in the competitor class.
                Competitor LoadCompetitors = CompetitorFactory.MakeCompetitor(ID, inStream);  //Calls the CompetitorFactory class0.12
                skiCompetitors.Add(LoadCompetitors.GetNumber(), LoadCompetitors);  //Adds the competitors to the dictionary. 
            }
            return result;  //Returns the results. 
        }

        public static SkiRun Load(string filename)  //Loads in the information from the file. nu
        { 
            SkiRun result;  //Creates a nerw instance of the SkiRunLodge. 

            StreamReader inStream = null;  //Creates a StreamReader.
            try
            {
                inStream = new StreamReader(filename);  //Read in the information into the file. 
                result = Load(inStream);  //Load in the competitor data. 
            }
            catch (Exception e)  //If something goes wrong we throw and expection. 
            {
                throw e;
            }
            finally  //Finally we close the StreamReader.
            {
                if (inStream != null) inStream.Close();
            }
            return result;  //We return the result. 
        }
        #endregion

        #region Edited Competitors
        public void EditAmateur(string inID, string inName, string inNumber, string inAddress, string inAge, string inScore)  //Saves the edited comepetitor data. 
        {
            Amateur EditedAmateur = new Amateur(inID, inName, inNumber, inAddress, inAge, inScore);  //Creates a new amateur called "EditedAmateur"

            skiCompetitors[inNumber] = EditedAmateur;  //Saves the new inforamtion too the dictionary. 
        }

        public void EditProfessional(string inID, string inName, string inNumber, string inAdress, string inAge, string inScore, string inSponsor)  //Saves the edited comepetitor data. 
        {
            Professional EditedProfessional = new Professional(inID, inName, inNumber, inAdress, inAge, inScore, inSponsor);  //Creates a new amateur called "EditedProfessional"

            skiCompetitors[inNumber] = EditedProfessional;  //Saves the new inforamtion too the dictionary. 
        }

        public void EditCelebrity(string inID, string inName, string inNumber, string inAdress, string inAge, string inScore, string inBlood, string inNoK)  //Saves the edited comepetitor data. 
        {
            Celebrity EditedCelebrity = new Celebrity(inID, inName, inNumber, inAdress, inAge, inScore, inBlood, inNoK);  //Creates a new amateur called "EditedProfessional"

            skiCompetitors[inNumber] = EditedCelebrity;  //Saves the new inforamtion too the dictionary. 
        }
        #endregion

        #region Top Scores     
        public void TopAmateurScores(string inScore, string inName)  //Saves the top amateur scores to the topAmateurScores list. 
        {
            int inTopScore = int.Parse(inScore);  //Converts the string score into an integer. 

            for (int i = 0; i < topAmateurScoresList.Count; i++)  //Loops round for each competitor in the list. 
            {
                if (topAmateurScoresList[i].Item2 == inName)  //Checks if someone with the same name is in the list. 
                {
                    topAmateurScoresList.RemoveAll(item => item.Item2 == inName);  //Removes that person. 
                }
            }
     
            if (topAmateurScoresList.Count < 3)  //If the list is less than 3 a competitor gets added. 
            {
                topAmateurScoresList.Add(new Tuple<int, string>(inTopScore, inName));  //Added competitor. 
            }
            else
            {
                topAmateurScoresList.Sort();     //List is sorted with smallest number first. 
                topAmateurScoresList.Reverse();  //List is reversed to get the highest numbers first.  

                if (inTopScore > topAmateurScoresList[2].Item1)  //If the next score is greater than the last item in the list it will be replaced. 
                { 
                    topAmateurScoresList[2] = new Tuple<int, string>(inTopScore, inName);  //New score comes in to the list. 
                }
            }
        }

        public void TopProfessionalScores(string inScore, string inName)  //Saves the top amateur scores to the topProfessionalScores list. 
        {
            int inTopScore = int.Parse(inScore);  //Converts the string score into an integer.

            for (int i = 0; i < topProfessionalScoresList.Count; i++)  //Loops round for each competitor in the list. 
            {
                if (topProfessionalScoresList[i].Item2 == inName)  //Checks if someone with the same name is in the list. 
                {
                    topProfessionalScoresList.RemoveAll(item => item.Item2 == inName);  //Removes that person. 
                }
            }

            if (topProfessionalScoresList.Count < 3)  //If the list is less than 3 a competitor gets added. 
            {
                topProfessionalScoresList.Add(new Tuple<int, string>(inTopScore, inName));  //Added competitor. 
            }
            else
            {
                topProfessionalScoresList.Sort();     //List is sorted with smallest number first. 
                topProfessionalScoresList.Reverse();  //List is reversed to get the highest numbers first. 

                if (inTopScore > topProfessionalScoresList[2].Item1)  //If the next score is greater than the last item in the list it will be replaced. 
                {
                    topProfessionalScoresList[2] = new Tuple<int, string>(inTopScore, inName);  //New score comes in to the list. 
                }
            }
        }

        public void TopCelebrityScores(string inScore, string inName)  //Saves the top amateur scores to the topCelebrityScores list. 
        {
            int inTopScore = int.Parse(inScore);  //Converts the string score into an integer.

            for (int i = 0; i < topCelebrityScoresList.Count; i++)  //Loops round for each competitor in the list.
            {
                if (topCelebrityScoresList[i].Item2 == inName)  //Checks if someone with the same name is in the list. 
                {
                    topCelebrityScoresList.RemoveAll(item => item.Item2 == inName);  //Removes that person. 
                }
            }

            if (topCelebrityScoresList.Count < 3)  //If the list is less than 3 a competitor gets added. 
            {
                topCelebrityScoresList.Add(new Tuple<int, string>(inTopScore, inName));  //Added competitor. 
            }
            else
            { 
                topCelebrityScoresList.Sort();     //List is sorted with smallest number first.
                topCelebrityScoresList.Reverse();  //List is reversed to get the highest numbers first. 

                if (inTopScore > topCelebrityScoresList[2].Item1)  //If the next score is greater than the last item in the list it will be replaced. 
                {
                    topCelebrityScoresList[2] = new Tuple<int, string>(inTopScore, inName);  //New score comes in to the list. 
                }
            }
        }

        public List<Tuple<int, string>> BestAmateurScores()  //Returns the best amateur scores list. 
        {
            topAmateurScoresList.Sort();     //Sorts the list with the smallest number first. 
            topAmateurScoresList.Reverse();  //Reverses the list so we get the highest number first. 
            return topAmateurScoresList;     //Returns the list. 
        }

        public List<Tuple<int, string>> BestProfessionalScores()  //Returns the best professional scores list. 
        {
            topProfessionalScoresList.Sort();     //Sorts the list with the smallest number first. 
            topProfessionalScoresList.Reverse();  //Reverses the list so we get the highest number first. 
            return topProfessionalScoresList;     //Returns the list. 
        }

        public List<Tuple<int, string>> BestCelebrityScores()  //Returns the best celebrity scores list. 
        {
            topCelebrityScoresList.Sort();     //Sorts the list with the smallest number first. 
            topCelebrityScoresList.Reverse();  //Reverses the list so we get the highest number first. 
            return topCelebrityScoresList;     //Returns the list. 
        }

        public void LoadScores()
        {
            for (int i = 1; i <= skiCompetitors.Count; i++)
            {
                Competitor editSkier = FindSkier(i.ToString());
                string Type = editSkier.GetType().Name;
                try
                {
                    editSkier.GetScore();
                }
                catch
                {

                }
                switch (Type)
                {
                    case "Amateur":
                        TopAmateurScores(editSkier.GetScore(), editSkier.GetName());
                        break;

                    case "Professional":
                        TopProfessionalScores(editSkier.GetScore(), editSkier.GetName());
                        break;
                    case "Celebrity":
                        TopCelebrityScores(editSkier.GetScore(), editSkier.GetName());
                        break;


                }
            }
        }
        #endregion

        #region TEST DATA
        static Random testRand = new Random(27);  //Creates a new random seed. 
        static string[] Address = new string[6] { "Avenue", "Road", "Street", "Close", "Drive", "Lane" };               //Array for the different location titles.
        static string[] Sponsor = new string[6] { "FireSkiers", "Microseft", "AUMD", "ENVEEDIA", "Entul", "iorange" };  //Array for sponsors. 
        static string[] BloodType = new string[8] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" };                 //Array from the different blood types. 
        static string vowels = "aeiou";  //String for the vowels. 
        static string consonants = "bcdfghjklmnprstvwxz";  //String for the consonants. 

        static string pickRandomLetter(string input)  //Picks a random letter. 
        {
            return input[testRand.Next(input.Length)].ToString();  //Returns a random letter. 
        }

        static string makeTestName(int parts)  //Makes a test name for a test competitor. 
        {
            string result = pickRandomLetter(consonants).ToUpper();  //Gives us a string for the first letter of the name. 
            for (int i = 0; i < parts; i = i + 1)  
            {
                result = result + pickRandomLetter(vowels); result = result + pickRandomLetter(consonants);  //Makes a name for us. 
            }
            return result;  //Returns the name. 
        }

        static string makeTestAddress(int parts)  //Makes a test address. 
        {
            string result = pickRandomLetter(consonants).ToUpper();  //Gives us a string for the first letter of the address. 
            for (int i = 0; i < parts; i = i + 1)
            {
                result = result + pickRandomLetter(vowels); result = result + pickRandomLetter(consonants);  //Makes an address for us. 
            }
            return result;  //Returns the address. 
        }

        static string makeFullTestName()  //Makes a full name. 
        {
            return makeTestName(testRand.Next(1, 4)) + " " + makeTestName(testRand.Next(2, 6)); //Returns the name. 
        }

        static string makeFullAddress()  //Makes a full address. 
        {
            int WhichAddress = testRand.Next(1, 6);  //Returns one of the location titles. 
            return (testRand.Next(1, 100)) + " " + makeTestAddress(testRand.Next(1, 5)) + " " + Address[WhichAddress];  //Returns the full address. 
        }

        static string makeFullSponsor()  //Makes a sponsor. 
        {
            int WhichSponsor = testRand.Next(1, 6);  //Selects one of the sponsors. 
            return Sponsor[WhichSponsor];  //Returns the sponsor. 
        }

        static string makeFullBloodType()  //Makes a blood type. 
        {
            int WhichBlood = testRand.Next(1, 8);  //Returns of the blood types. 
            return BloodType[WhichBlood];  //Returns the blood type chosen. 
        }

        static string makeFullNoK()  //Makes a next of kin. 
        {
            return makeTestName(testRand.Next(1, 4)) + " " + makeTestName(testRand.Next(2, 6));  //Returns a name for the next of kin. 
        }

        static int ClassDeciderRand()  //Makes a competitor class. 
        {
            int ClassDecider;
            return ClassDecider = (testRand.Next(1, 4));  //Returns a number which we use to chose the different classes. 
        }

        static string RandomScore()  //Makes a score. 
        {
            int Score = testRand.Next(1, 101);  //Makes the score. 
            return Score.ToString();  //Returs the score converted into a string. 
        }

        static string RandomAge()  //Makes a random age. 
        {
            int ShowAge = testRand.Next(1, 3);  //Decides whether the competitor will have an age. 
            if (ShowAge == 1)  //If the ShowAge int equals 1 then we give them an age. 
            {
                int Age = testRand.Next(12, 71);  //Makes a random age between 12 and 70. 
                return Age.ToString();  //Returns that age converted into a string. 
            }
            else
            {
                return "";  //If the random number is 2 then we return no age. 
            }
        }

        public int counter = 0;  //Makes so we cannot create more than 5000 competitors. 

        public void MakingCompetitors()  //Makes the test competitors. 
        {
            for (int i = 0; i < 120; i++)  //We need to create 120 competitors so we loop round that many times. 
            {
                if (counter < 5000)  //If the countr is less than 5000 then we can create the competitor. 
                {
                    int ClassCounter = ClassDeciderRand();  //We get the class decider number. 

                    if (ClassCounter == 1)  //If it's 1 then we make an amateur. 
                    {
                        string AmaScore = RandomScore();      //They are given a score. 
                        string AmaName = makeFullTestName();  //They are given a name. 
                        AddAmateurCompetitor("Amateur", AmaName, makeFullAddress(), RandomAge(), AmaScore);  //Calls the AddAmateurCompetitor method so we can make them.
                        TopAmateurScores(AmaScore, AmaName);  //Gets added to top the scores list. 
                        counter++;  //The counter increments by one so we can keep track of how many competitors we have made. 
                    }

                    if (ClassCounter == 2)  //If it's 2 then we make a professional. 
                    {
                        string ProScore = RandomScore();      //They are given a score. 
                        string ProName = makeFullTestName();  //They are given a name. 
                        AddProfessionalCompetitor("Professional", ProName, makeFullAddress(), RandomAge(), ProScore, makeFullSponsor());  //Calls the AddProfessionalCompetitor method so we can make them.
                        TopProfessionalScores(ProScore, ProName);  //Gets added to top the scores list. 
                        counter++;  //The counter increments by one so we can keep track of how many competitors we have made. 
                    }

                    if (ClassCounter == 3)  //If it's 3 then we make a celebrity. 
                        {
                        string CelScore = RandomScore();      //They are given a score. 
                        string CelName = makeFullTestName();  //They are given a name. 
                        AddCelebrityCompetitor("Celebrity", CelName, makeFullAddress(), RandomAge(), CelScore, makeFullBloodType(), makeFullNoK());  //Calls the   AddCelebrityCompetitor method so we can make them.
                        TopCelebrityScores(CelScore, CelName);  //Gets added to top the scores list. 
                        counter++;  //The counter increments by one so we can keep track of how many competitors we have made. 
                    }
                }

            }
        }
        #endregion
    }
}
