using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()  //The main method which runs first when the program is open. 
        {
            InitializeComponent();
        }

        SkiRun activeSkiRun = new SkiRun("Skiers");  //Makes an instance of the SkiRun class so we can call methods we need for creating competitors for example. 

        #region Clearing Text
        private void ClearText()  //Clears the text and unchecks the radio buttons for the next competitor. 
        {
            NewNameTextBox.Clear();                     //Clears the new name text box for the next competitor. 
            NewCompetitorAddressTextBox.Clear();        //Clears the new address text box for the next competitor. 
            SponsorTextBox.Clear();                     //Cleas the sponsor text box for the next competitor. 
            BloodTypeTextBox.Text = "";                 //Clears the chosen blood type for the next competitor. 
            NextOfKinTypeTextBox.Clear();               //Clears the next of kin text box for the next competitor. 
            AgeTextBox.Clear();                         //Clears the age text box is the user decided to input their age. 
            AmateurRadioButton.IsChecked = false;       //Unchecks the amateur radio button at the start of making a new competitor. 
            ProfessionalRadioButton.IsChecked = false;  //Unchecks the professional radio button at the start of making a new competitor. 
            CelebrityRadioButton.IsChecked = false;     //Unchecks the celebrity radio button at the start of making a new competitor. 
        } 
        #endregion

        #region Making Competitors
        static int Score = 0;  //Sets score value to 0 at the start. 

        string NewScore = Score.ToString();  //Truns score into a string. 

        private bool AgeCheck()  //Method makes sure you cannot input letters or symbols for an age.  
        {
            try
            {
                if (AgeTextBox.Text == "")  //If the box does not equal an age then that's fine. 
                {
                    return true;
                }

                int ageCheck = int.Parse(AgeTextBox.Text); //If the age correctly parses into an integer then this is also fine. 

                if (ageCheck >= 12 && ageCheck <= 70)  //Checks if the meet the age limit.
                {
                    return true;  //Return true if they meet the age limit. 
                }

                return false;  //If both of the conditions above fail then it will return false. 

            }
            catch
            {
                return false;  //Anything else returns false. 
            }
        }

        private void MakingCompetitors()  //Makes our competitors. 
        {
            if (AgeCheck() == true)  //If the AgeCheck method returns true then we don't do anything and the code continues. 
            {

            }
            else                    //If AgeCheck returns false we return an error message and try again. 
            {
                MessageBox.Show("Please enter a number between 12 and 70 for your age.");
                AgeTextBox.Clear();
                return;
            }


            if (NewNameTextBox.Text.Trim() == "")  //If there is no name entered when you're making a competitor then it will be rejected. 
            {
                MessageBox.Show("Please enter your name.");  //The error message. 
            }

            else if (NewCompetitorAddressTextBox.Text.Trim() == "")  //If there is no address entered when you're making a competitor then it will be rejected. 
            {
                MessageBox.Show("Please enter your address.");  //The error message. 
            }


            else if (AmateurRadioButton.IsChecked == false && ProfessionalRadioButton.IsChecked == false && CelebrityRadioButton.IsChecked == false) //If there is no class selected when you're making a competitor then it will be rejected. 
            {
                MessageBox.Show("Please choose your class.");  //The error message. 
            }

            else if (AmateurRadioButton.IsChecked == true)  //Makes an amateur competitor. 
            {
                string ID = "Amateur";  //Gives the competitor their class ID. 
                SkiRun.AddAmateurCompetitor(ID, NewNameTextBox.Text.Trim(), NewCompetitorAddressTextBox.Text.Trim(), AgeTextBox.Text.Trim(), NewScore);  //Calls the amateur creator method in the SkiRun class. 
                CompetitorNumberTextBox.Text = activeSkiRun.GetSkiNumber().ToString();  //Gets the competitor number. 
                MessageBox.Show("Competitor Created!");  //Message for when you have created your competitor. 
                ClearText();  //Calls the ClearText method which clears all of the text. 
            }

            else if (ProfessionalRadioButton.IsChecked == true)  //Makes a professional competitor.
            {

                if (SponsorTextBox.Text == "")  //If no sponsor is selected it will be rejected. 
                {
                    MessageBox.Show("Please enter a sponsor!");  //The error message. 
                }

                else  //If there is a sponsor then we continue onto the creation. 
                {
                    string ID = "Professional";  //Gives the competitor their class ID. 
                    SkiRun.AddProfessionalCompetitor(ID, NewNameTextBox.Text.Trim(), NewCompetitorAddressTextBox.Text.Trim(), AgeTextBox.Text.Trim(), NewScore, SponsorTextBox.Text.Trim());  //Calls the professional creator method in the SkiRun class. 
                    CompetitorNumberTextBox.Text = activeSkiRun.GetSkiNumber().ToString();  //Gets the competitor number. 
                    MessageBox.Show("Competitor Created!");  //Message for when you have created your competitor. 
                    ClearText();  //Calls the ClearText method which clears all of the text. 
                }
            }
            else if (CelebrityRadioButton.IsChecked == true)  //Makes a celebrity competitor. 
            {

                if (BloodTypeTextBox.Text.Trim() == "" || NextOfKinTypeTextBox.Text.Trim() == "")  //If there is no blood type and/or next of kin then the cpmpetitor will be rejected. 
                {
                    MessageBox.Show("Please enter a blood type, next of kin or both!");  //The error message. 
                }

                else
                {
                    string ID = "Celebrity";  //Gives the competitor their class ID. 
                    SkiRun.AddCelebrityCompetitor(ID, NewNameTextBox.Text.Trim(), NewCompetitorAddressTextBox.Text.Trim(), AgeTextBox.Text.Trim(), NewScore, BloodTypeTextBox.Text.Trim(), NextOfKinTypeTextBox.Text.Trim());  //Calls the celebrity creator method in the SkiRun class. 
                    CompetitorNumberTextBox.Text = activeSkiRun.GetSkiNumber().ToString();  //Gets the competitor number. 
                    MessageBox.Show("Competitor Created!");  //Message for when you have created your competitor. 
                    ClearText();  //Calls the ClearText method which clears all of the text. 
                }
            }
        }

        private void AddCompetitor_Click(object sender, RoutedEventArgs e)  //Creates the different competitors. 
        {
            MakingCompetitors();
        } 
        #endregion

        #region Find Competitors
        private void FindCompetitor()  //Set up to find the competitors by their number.
        {
            DetailsTextBox.Clear();  //Clears the details textbox. 
            Competitor editSkier;  //Makes aninstance of the Competitor class. 
            if (SearchTextBox.Text.Trim() != "" && NumberTextBox.Text.Trim() != "")  //You can only use on search method so if there is text in both it will be rejected. 
            {
                MessageBox.Show("Please use only one search method!");  //The error message. 
            }
            else if (SearchByName.Text.Trim() == "" && NumberTextBox.Text.Trim() == "") //If there is no text in either search methods then the search will fail. 
            {
                editSkier = null;       //There's no competitor. 
                MessageBox.Show("There is no competitor with that number or name! Please try again.");  //The error message. 
                NameTextBox.Clear();    //Clears the name text box.
                AddressTextBox.Clear(); //Clears the address text box. 
                ScoreTextBox.Clear();   //Clears the score text box.
                NumberTextBox.Clear();  //Clears number text box.
                TagTextBox.Clear();     //Clears the tag text box. 
            }

            //***NOTE*** - This is a very inefficient way to seach. I have the same code twice for each search method because I didn't know how to differentiate between the two. 

            else if (SearchByName.Text.Trim() != "")  //If search by name has some text in it then it will make a competitor.
            {
                editSkier = SkiRun.FindSkierByName(SearchByName.Text.Trim());  //Calls the find competitor by name in the SkiRun class.
                try
                {
                    NameTextBox.Text = editSkier.GetName().Trim();        //Sets the competitor name.
                    AddressTextBox.Text = editSkier.GetAddress().Trim();  //Sets the competitor address. 
                    ScoreTextBox.Text = editSkier.GetScore().Trim();      //Sets the competitor score.   

                    if (editSkier.GetSponsor() == null && editSkier.GetBlood() == null && editSkier.GetNoK() == null)  //If there's no sponsor, no blood type and no next of kin then it must be an amateur. 
                    {

                        DetailsTextBox.Text = "Class: Amatuer" + Environment.NewLine + "Your age: " + editSkier.GetAge() + Environment.NewLine + "You have paid £100";  //Information show in details text box. 
                        TagTextBox.Text = "Amateur";  //Displays the tag in the tag text box. 
                    }
                    else if (editSkier.GetSponsor() != null)  //If the sponsor does no equal null then it must be a professional. 
                    {

                        DetailsTextBox.Text = "Class: Professional" + Environment.NewLine + "Your age: " + editSkier.GetAge() + Environment.NewLine + "You have paid £200" + Environment.NewLine + "Sponsor: " + editSkier.GetSponsor();  //Information show in details text box. 
                        TagTextBox.Text = "Professional";  //Displays the tag in the tag text box. 
                    }
                    else if (editSkier.GetBlood() != null)  //If blood type does not equal null then is must be a celebrity. 
                    {

                        DetailsTextBox.Text = "Class: Celebrity" + Environment.NewLine + "Your age: " + editSkier.GetAge() + Environment.NewLine + "You do not have to pay." + Environment.NewLine + "Blood Type: " + editSkier.GetBlood() + Environment.NewLine + "Next of Kin: " + editSkier.GetNoK();  //Information show in details text box. 
                        TagTextBox.Text = "Celebrity";  //Displays the tag in the tag text box. 
                    }
                }
                catch
                {
                    MessageBox.Show("Could not find competitor!");  //If the user types a name it cannot find then it will display this. 
                }
            }
            else if (NumberTextBox.Text.Trim() != "")  //If the number text box has something in it then it will use this method. 
            {
                editSkier = SkiRun.FindSkier(NumberTextBox.Text.Trim()); //Calls the find competitor in the SkiRun class.
                try
                {
                    NameTextBox.Text = editSkier.GetName();        //Sets the competitor name.
                    AddressTextBox.Text = editSkier.GetAddress();  //Sets the competitor address. 
                    ScoreTextBox.Text = editSkier.GetScore();      //Sets the competitor score. 

                    if (editSkier.GetSponsor() == null && editSkier.GetBlood() == null && editSkier.GetNoK() == null)  //If there's no sponsor, no blood type and no next of kin then it must be an amateur. 
                    {
                        DetailsTextBox.Text = "Class: Amatuer" + Environment.NewLine + "Your age: " + editSkier.GetAge() + Environment.NewLine + "You have paid £100";  //Information show in details text box.
                        TagTextBox.Text = "Amateur";  //Displays the tag in the tag text box. 
                    }
                    else if (editSkier.GetSponsor() != null)  //If the sponsor does no equal null then it must be a professional. 
                    {
                        DetailsTextBox.Text = "Class: Professional" + Environment.NewLine + "Your age: " + editSkier.GetAge() + Environment.NewLine + "You have paid £200" + Environment.NewLine + "Sponsor: " + editSkier.GetSponsor();  //Information show in details text box.
                        TagTextBox.Text = "Professional";  //Displays the tag in the tag text box. 
                    }
                    else if (editSkier.GetBlood() != null)  //If blood type does not equal null then is must be a celebrity. 
                    {
                        DetailsTextBox.Text = "Class: Celebrity" + Environment.NewLine + "Your age: " + editSkier.GetAge() + Environment.NewLine + "You do not have to pay." + Environment.NewLine + "Blood Type: " + editSkier.GetBlood() + Environment.NewLine + "Next of Kin: " + editSkier.GetNoK();  //Information show in details text box.
                        TagTextBox.Text = "Celebrity";  //Displays the tag in the tag text box. 
                    }
                }
                catch
                {
                    MessageBox.Show("Could not find competitor!");  //If the user types a name it cannot find then it will display this. 
                }
            }
            //NumberTextBox.Clear();
            //SearchByName.Items.Clear();
            //SearchTextBox.Clear();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)  //Finds the competitor in the SkiRun class. 
        {
            NameTextBox.Clear();    //Clears the name text box. 
            AddressTextBox.Clear(); //Clears the address text box. 
            ScoreTextBox.Clear();   //Clears the score text box. 
            TagTextBox.Clear();     //Clears the tag text box. 
            FindCompetitor();       //Calls the FindCompetitor method above. 

        }

        private void FindNameButton_Click(object sender, RoutedEventArgs e)  //Finds out if there is a competitor with a similar name to the one the user entered. 
        {
            SearchByName.Items.Clear();                                  //Clears the search by name combo box. 
            activeSkiRun.CheckingNames(SearchTextBox.Text.Trim());              //Calls the CheckingNames method in the SkiRun class. 

            foreach (string competitor in activeSkiRun.SearchByName)     //Foreach loop for each competitor in the SearchByName list.
            {
                SearchByName.Items.Add(competitor);                      //Competitor gets added to the combo box. 
            }
        }

        private void ClearNameButton_Click(object sender, RoutedEventArgs e)  //Clears the SearchByName combo box. 
        {
            SearchByName.Text = "";  //Sets SearchByName combo box to nothing. 
        }
        #endregion

        #region Saving Competitors
        //***NOTE*** - This is a very inefficient way to save I have the same code twice for each save method because I didn't know how to differentiate between the two. 

        string CompNumber;  //Competitor number variable. 

        private void SaveByNumber()  //Method for saving by number instead of name. 
        {
            if (NumberTextBox.Text == "")  //If there is no number in the number text box we will throw an error message.
            {
                MessageBox.Show("There is no competitor number or competitor name selected!");  //The error message.
                return;  //The return for the user to try again. 
            }

            Competitor editSkier = SkiRun.FindSkier(NumberTextBox.Text); //Finds the competitor. 

            try  //if CompNumber is null we can catch the exception. 
            {
                 CompNumber = editSkier.GetNumber(); 
            }
            catch (NullReferenceException)  //Instead of crashing the program we can display an error message and let the user try again. 
            {
                MessageBox.Show("There is no competitor with that number!");  //The error message. 
                return;  //The return for the user to try again. 
            }

            if (NameTextBox.Text.Trim() == "" || AddressTextBox.Text.Trim() == "")  //If the names text box and the address text box is empty when you save it will be rejected.
            {
                MessageBox.Show("Please re-enter your name and or address.");  //The error message. 
                return;  //Returns back to try again. 
            }

            if (ScoreTextBox.Text.Trim() == "" || ScoreTextBox.Text.Trim() == "0")  //If there is no score then it is rejected.
            {
                MessageBox.Show("Please enter a score.");  //The error message. 
            }

            else
            {
                try
                {
                    int Score = int.Parse(ScoreTextBox.Text);  //Turn the score text into an integer. 


                    if (Score < 0 || Score > 100)  //If it's below 0 and above 100 it is rejected. 
                    {
                        MessageBox.Show("Please enter a score between 0 and 100.");  //The error message. 
                    }
                    else

                        if (TagTextBox.Text == "Amateur" && Score > 0 && Score <= 100)  //If it is an amateur and the score is within range then it save the competitor. 
                    {
                        activeSkiRun.EditAmateur(TagTextBox.Text, NameTextBox.Text.Trim(), CompNumber, AddressTextBox.Text.Trim(), editSkier.GetAge(), ScoreTextBox.Text.Trim());  //Calls the EditAmateur method and saves all of the information. 
                        activeSkiRun.TopAmateurScores(ScoreTextBox.Text.Trim(), NameTextBox.Text.Trim());  //Adds the high scorers to the TopAmateurScores list. 
                        activeSkiRun.Save("skiers.txt");  //Which file to save to. 
                        MessageBox.Show("Successfully Saved!");  //The success message. 

                    }
                    else if (TagTextBox.Text == "Professional" && Score > 0 && Score <= 100)  //If it is an professional and the score is within range then it save the competitor. 
                    {
                        activeSkiRun.EditProfessional(TagTextBox.Text, NameTextBox.Text.Trim(), CompNumber, AddressTextBox.Text.Trim(), editSkier.GetAge(), ScoreTextBox.Text.Trim(), editSkier.GetSponsor());  //Calls the EditAmateur method and saves all of the information. 
                        activeSkiRun.TopProfessionalScores(ScoreTextBox.Text.Trim(), NameTextBox.Text.Trim());   //Adds the high scorers to the TopProfessionalScores list. 
                        activeSkiRun.Save("skiers.txt");  //Which file to save to. 
                        MessageBox.Show("Successfully Saved!");  //The success message. 
                    }
                    else if (TagTextBox.Text == "Celebrity" && Score > 0 && Score <= 100)  //If it is an celebrity and the score is within range then it save the competitor. 
                    {
                        activeSkiRun.EditCelebrity(TagTextBox.Text, NameTextBox.Text.Trim(), CompNumber, AddressTextBox.Text.Trim(), editSkier.GetAge(), ScoreTextBox.Text.Trim(), editSkier.GetBlood(), editSkier.GetNoK());  //Calls the EditAmateur method and saves all of the information. 
                        activeSkiRun.TopCelebrityScores(ScoreTextBox.Text.Trim(), NameTextBox.Text.Trim());   //Adds the high scorers to the TopCelebrityScores list. 
                        activeSkiRun.Save("skiers.txt");  //Which file to save to. 
                        MessageBox.Show("Successfully Saved!");  //The success message. 
                    }
                }
                catch
                {
                    MessageBox.Show("Please enter a valid score number.");  //If the user enters anything but a number then it will be rejected. 
                }
            }
        }

        private void SaveByName()  //Method for saving by name instead of number. 
        {
            if (SearchByName.Text == "")
            {
                MessageBox.Show("There is no competitor number or competitor name selected!");
                return;
            }

            Competitor editSkier = SkiRun.FindSkierByName(SearchByName.Text);  //Finds the competitor. 

            string CompNumber = editSkier.GetNumber();  //Gets the number becasue we are not saving the number in the box. 

            if (NameTextBox.Text.Trim() == "" || AddressTextBox.Text.Trim() == "")  //If the names text box and the address text box is empty when you save it will be rejected.
            {
                MessageBox.Show("Please re-enter your name and or address.");  //The error message. 
                return;  //Returns back to try again.
            }

            if (ScoreTextBox.Text.Trim() == "" || ScoreTextBox.Text.Trim() == "0")  //If there is no score then it is rejected.
            {
                MessageBox.Show("Please enter a score.");  //The error message. 
            }

            else
            {
                try
                {
                    int Score = int.Parse(ScoreTextBox.Text);  //Turn the score text into an integer. 


                    if (Score < 0 || Score > 100)  //If it's below 0 and above 100 it is rejected. 
                    {
                        MessageBox.Show("Please enter a score between 0 and 100.");  //The error message. 
                    }
                    else

                        if (TagTextBox.Text == "Amateur" && Score > 0 && Score <= 100)  //If it is an amateur and the score is within range then it save the competitor. 
                    {
                        activeSkiRun.EditAmateur(TagTextBox.Text, NameTextBox.Text.Trim(), CompNumber, AddressTextBox.Text.Trim(), editSkier.GetAge(), ScoreTextBox.Text);  //Calls the EditAmateur method and saves all of the information. 
                        activeSkiRun.TopAmateurScores(ScoreTextBox.Text.Trim(), NameTextBox.Text.Trim());  //Adds the high scorers to the TopAmateurScores list. 
                        activeSkiRun.Save("skiers.txt");  //Which file to save to. 
                        MessageBox.Show("Sucessfully Saved!");  //The success message. 

                    }
                    else if (TagTextBox.Text == "Professional" && Score > 0 && Score <= 100)  //If it is an professional and the score is within range then it save the competitor. 
                    {
                        activeSkiRun.EditProfessional(TagTextBox.Text, NameTextBox.Text.Trim(), CompNumber, AddressTextBox.Text.Trim(), editSkier.GetAge(), ScoreTextBox.Text.Trim(), editSkier.GetSponsor());  //Calls the EditAmateur method and saves all of the information. 
                        activeSkiRun.TopProfessionalScores(ScoreTextBox.Text.Trim(), NameTextBox.Text.Trim());  //Adds the high scorers to the TopProfessionalScores list. 
                        activeSkiRun.Save("skiers.txt");  //Which file to save to. 
                        MessageBox.Show("Sucessfully Saved!");  //The success message. 
                    }
                    else if (TagTextBox.Text == "Celebrity" && Score > 0 && Score <= 100)  //If it is an celebrity and the score is within range then it save the competitor. 
                    {
                        activeSkiRun.EditCelebrity(TagTextBox.Text, NameTextBox.Text.Trim(), CompNumber, AddressTextBox.Text.Trim(), editSkier.GetAge(), ScoreTextBox.Text.Trim(), editSkier.GetBlood(), editSkier.GetNoK());  //Calls the EditAmateur method and saves all of the information. 
                        activeSkiRun.TopCelebrityScores(ScoreTextBox.Text.Trim(), NameTextBox.Text.Trim());  //Adds the high scorers to the TopCelebrityScores list. 
                        activeSkiRun.Save("skiers.txt");  //Which file to save to. 
                        MessageBox.Show("Sucessfully Saved!");  //The success message. 
                    }
                }
                catch
                {
                    MessageBox.Show("Please enter a valid score number.");  //If the user enters anything but a number then it will be rejected. 
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)  //Runs code to save edits that have been made to the competitors. 
        {
            if (NumberTextBox.Text.Trim() != "")  //If the number text box has something it it then it will run the SaveByNumber method above. 
            {
                SaveByNumber();
            }
            else

            if (NumberTextBox.Text.Trim() == "")  //If the number text box does no have anything it then it will run the SaveByName method above. 
            {
                SaveByName();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)  //An "autosave" when the program is closed. 
        {
            activeSkiRun.Save("skiers.txt");  //Which file to save it to. 
            MessageBox.Show("Autosave Complete!");  //Success message. 
        }
        #endregion

        #region Class Radio Buttons
        private void AmateurRadioButton_Check(object sender, RoutedEventArgs e)  //When the AmateurRadioButton is selected, all of the other options are cleared. 
        {
            BloodTypeTextBox.IsEnabled = false;      //Disables the blood type combo box.
            NextOfKinTypeTextBox.IsEnabled = false;  //Disables the next of kin text box. 
            SponsorTextBox.IsEnabled = false;        //Disables the sponsor text box.
            SponsorTextBox.Clear();                  //Clears the sponsor text box. 
            BloodTypeTextBox.Text = "";              //Sets the bloody type combo box to nothing. This contains data we still need to we cannot clear it. 
            NextOfKinTypeTextBox.Clear();            //Clears the next of kin text box. 
        }

        private void ProfessionalRadioButton_Check(object sender, RoutedEventArgs e)  //When the ProfessionalRadioButton is selected, all of the other options are cleared. 
        {
            BloodTypeTextBox.IsEnabled = false;             //Disables the blood type combo box.
            NextOfKinTypeTextBox.IsEnabled = false;         //Disables the next of kin text box. 
            if (ProfessionalRadioButton.IsEnabled == true)  //If the ProfessionalRadioButton is enabled it re-enables all of the professional boxes. 
            {
                SponsorTextBox.IsEnabled = true;  //Enables the sponsor text box. 
                BloodTypeTextBox.Text = "";       //Sets the bloody type combo box to nothing. This contains data we still need to we cannot clear it. 
                NextOfKinTypeTextBox.Clear();     //Clears the next of kin text box. 
            }
        }

        private void CelebrityRadioButton_Check(object sender, RoutedEventArgs e)  //When the CelebrityRadioButton is selected, all of the other options are cleared. 
        {
            SponsorTextBox.IsEnabled = false;            //Disables the sponsor text box.
            if (CelebrityRadioButton.IsEnabled == true)  //If the CelebrityRadioButton is enabled it re-enables all of the celebrity boxes. 
            {
                BloodTypeTextBox.IsEnabled = true;      //Enables the blood type combo box.
                NextOfKinTypeTextBox.IsEnabled = true;  //Enables the next of kin text box. 
                SponsorTextBox.Clear();                 //Clears the sponsor text box.  
            }
        }

        #endregion

        #region Reports
        //***NOTE***- It takes a lot of time when producing a report for a lot of competitors. This could be due to inefficiency. 

        public void ClearReports()  //Clears all of the data in the reports section. 
        {
            EntriesTextBox.Clear();         //Clears the entires text box. 
            IncomeTextBox.Clear();          //Clears the income text box. 
            TotalScoresTextBox.Clear();     //Clears the total scores text box.     
            TopThreeScoresTextBox.Clear();  //Clears the top three scores text box. 
            activeSkiRun.ClearData();       //Clears the top scores lists and the competitor dictionary. 
        }

        private void CreateReports_Click(object sender, RoutedEventArgs e)  //Creates the reports for all of the competitors. 
        {
            EntriesTextBox.Clear();                                        //Clears the entires text box. 
            IncomeTextBox.Clear();                                         //Clears the income text box. 
            TotalScoresTextBox.Clear();                                    //Clears the top three scores text box.
            TopThreeScoresTextBox.Clear();                                 //Clears the top three scores text box. 
            EntriesTextBox.Text = activeSkiRun.GetSkiNumber().ToString();  //Gets number of entires. 
            IncomeTextBox.Text = "£" + activeSkiRun.IncomeValues();        //Gets all of the income from the competitors. 
            TotalScoresTextBox.Text = activeSkiRun.TotalScores();          //Gets the total score value. 
            activeSkiRun.LoadInAges();                                     //Loads in all of the ages.
            AgeMinTextBox.Text = activeSkiRun.MinAge();                    //Gets the minumum age. 
            AgeMaxTextBox.Text = activeSkiRun.MaxAge();                    //Gets the maximum age.
            AgeAveTextBox.Text = activeSkiRun.AveAge();                    //Gets the average age. 
            AgeModeTextBox.Text = activeSkiRun.ModeAge();                  //Gets the age that comes up most often. 
            TopAmateurScores();                                            //Calls the TopAmateurScores method below. 
            TopProfessionalScores();                                       //Calls the TopProfessionalScores method below. 
            TopCelebrityScores();                                          //Calls the TopCelebrityScores method below. 

        }
        #endregion

        #region Loading Competitors
        private void LoadButton_Click(object sender, RoutedEventArgs e)  //Load click calls the Load method. 
        {
            Load();
        }

        public void Load()  //Calls the load method in the SkiRun class to load the data of the competitors. 
        {
            try
            {
                SkiRun.Load("skiers.txt");  //Selects which file to load from.
                activeSkiRun.LoadScores();  //Loads in the scores 
                MessageBox.Show("Load successful.");  //Success message. 
            }
            catch
            {
                MessageBox.Show("Something went wrong when try to load. Information was not loaded.");  //Error message. 
            }
        }
        #endregion

        #region Top Scorers
        public void TopAmateurScores()  //Gets the top scores from the BestAmateurScores method in the SkiRun class.
        {
            TopThreeScoresTextBox.Text += "Amateur" + Environment.NewLine;              //Writes Amateur in the rop three scores text box so we know which class of competitor the scores are from. 
            foreach (Tuple<int, string> topscores in activeSkiRun.BestAmateurScores())  //Foreach loop top output all three top scores. 
            {
                TopThreeScoresTextBox.Text += topscores + Environment.NewLine;          //Writes the score and name out. 
            }
        }

        public void TopProfessionalScores()  //Gets the top scores from the BestProfessionalScores method in the SkiRun class.
        {
            TopThreeScoresTextBox.Text += "Professional" + Environment.NewLine;              //Writes Professional in the rop three scores text box so we know which class of competitor the scores are from. 
            foreach (Tuple<int, string> topscores in activeSkiRun.BestProfessionalScores())  //Foreach loop top output all three top scores. 
            {
                TopThreeScoresTextBox.Text += topscores + Environment.NewLine;               //Writes the score and name out.
            }
        }

        public void TopCelebrityScores()  //Gets the top scores from the BestCelebrityScores method in the SkiRun class.
        {
            TopThreeScoresTextBox.Text += "Celebrity" + Environment.NewLine;              //Writes Celebrity in the rop three scores text box so we know which class of competitor the scores are from. 
            foreach (Tuple<int, string> topscores in activeSkiRun.BestCelebrityScores())  //Foreach loop top output all three top scores. 
            {
                TopThreeScoresTextBox.Text += topscores + Environment.NewLine;            //Writes the score and name out.
            }
        }
        #endregion

        #region TEST DATA
        private void CreateTestData_Click(object sender, RoutedEventArgs e)
        {
            TopThreeScoresTextBox.Clear();                                          //Clears the high scores text box so data doesn't overlap. 
            activeSkiRun.MakingCompetitors();                                       //Calls the making competitors method which lives in the SkiRun class. 
            CompetitorNumberTextBox.Text = activeSkiRun.GetSkiNumber().ToString();  //Updates the competitor number text box. 
        }
        #endregion
   
        #region Clear All Data
        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            activeSkiRun.ClearCompData();             //Calls a method in the SkiRun class which empties the dictionary and the lists.
            IncomeTextBox.Clear();                    //Clears the income textbox.
            TotalScoresTextBox.Clear();               //Clears the total scores text box.
            EntriesTextBox.Clear();                   //Clears the ammount of entries the competiton had. 
            TopThreeScoresTextBox.Clear();            //Clears the high scores text box so data doesn't overlap. 
            AgeMinTextBox.Clear();                    //Clears the minimum age textbox.
            AgeMaxTextBox.Clear();                    //Clears the maximum age textbox.
            AgeAveTextBox.Clear();                    //Clears the average age textbox.
            AgeModeTextBox.Clear();                   //Clears the mode age textbox. 
            activeSkiRun.CompetitorAges.Clear();      //Clears the ages list. 
            CompetitorNumberTextBox.Text = "000000";  //Resets the competitor number text box. 
            NumberTextBox.Clear();                    //Clears the number text box.
            NameTextBox.Clear();                      //Clears the name text box. 
            AddressTextBox.Clear();                   //Clears the address text box.
            DetailsTextBox.Clear();                   //Clears the details text box. 
            ScoreTextBox.Clear();                     //Clears the scores text box.
            SearchTextBox.Clear();                    //Clears the seach by name text box. 
            TagTextBox.Clear();                       //Clears the tag text box. 
            SearchByName.Items.Clear();               //Clears the search by name combo box.

        }
        #endregion
    }
}
