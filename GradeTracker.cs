/* File Name:   GradeTracker.js
 * Date:        04/06/2021
 * Coder:       Bohdan Simakov
 * Description: Get input from user (new course, assignmment, etc) and insert it into grades.json file complying to grade-schema1.json.
 */

using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace GradeTracker
{
    class GradeTracker
    {
        // Declare constants for the file path names 
        private const string LIST_FILE = "grades.json";
        private const string SCHEMA_FILE = "grade-schema1.json";
        static void Main(string[] args)
        {
            string json_schema;
            string jsondata;
            bool done = false;
            List<Item> Courses = new List<Item>();
            CourseHolder holder = new CourseHolder();
            // Reading schema file
            if (ReadFile(SCHEMA_FILE, out json_schema))
            {
                // Reading json file
                if (ReadFile(LIST_FILE, out jsondata))
                {
                    // Making json file into an object to use
                    holder = JsonConvert.DeserializeObject <CourseHolder>(jsondata);
                    // Taking list out of the courseholder
                    Courses = holder.courses;
                    // Validating input
                    for (int i = 0; i < Courses.Count; i++)
                    {
                        if (!ValidateItem(Courses[i], json_schema))
                        {
                            Console.WriteLine("Grades data file grades.json does not comply with json_schema.\nPlease modify your json file");
                            Environment.Exit(1);
                        }
                    }
                }
                else
                {
                    // Giving user the ability to make own json file
                    Console.Write("Grades data file grades.json not found. Create new file? (Y/N):");
                    object input = Console.ReadLine();
                    if (input.Equals("Y"))
                    {
                        Console.Write("\nNew Data set created. Press any key to continue...");
                        Console.ReadLine();
                    }
                    // Closing program if no is selected
                    else if (input.Equals("N"))
                    {
                        Console.WriteLine("\nThanks for running my program, have a wonderful day");
                        Environment.Exit(1);
                    }
                }
                do
                {
                    // Creating variables for needed dor do while loop
                    Console.Clear();
                    bool valid;
                    Item item = new Item();
                    // Printing the title
                    PrintTitle();
                    // Checking to see if courses need to be printed out
                    if (Courses.Count == 0)
                    {
                        Console.WriteLine("There are currently no saved courses.");
                    }
                    else
                    {
                        // Printing out titles
                        Console.Write("#. Course\t");
                        Console.Write("Marks Earned\t");
                        Console.Write("Out Of\t");
                        Console.WriteLine("Percent\n");
                        for (int i = 0; i < Courses.Count; i++)
                        {
                            Console.Write(i + 1 + ". ");
                            Console.Write(Courses[i].Code + "\t\t");
                            // Checking to see if evaluations in null
                            if (Courses[i].Evalulations == null)
                            {
                                Console.Write("0.0\t");
                                Console.Write("0.0\t");
                                Console.WriteLine("0.0");
                            }
                            else
                            {
                                // Preparing for calculations
                                double totalEarned;
                                double totalPotential;
                                double?[] tempEarned = new double?[Courses[i].Evalulations.Count];
                                double[] tempPotential = new double[Courses[i].Evalulations.Count];
                                // Gathering all evaluations to do calculations
                                for (int j = 0; j < Courses[i].Evalulations.Count; j++)
                                {
                                    tempEarned[j] = Courses[i].Evalulations[j].EarnedMarks;
                                    tempPotential[j] = Courses[i].Evalulations[j].OutOf;
                                }
                                // Perfomring and printing calculations
                                totalEarned = CalcCourseTotalEarned(tempEarned);
                                totalPotential = CalcCourseTotalPotential(tempPotential);
                                Console.Write(String.Format("{0:0.0}", totalEarned) + "\t");
                                Console.Write(String.Format("{0:0.0}", totalPotential) + "\t");
                                Console.WriteLine(String.Format("{0:0.0}", CalcCoursePercent(totalEarned, totalPotential)));
                            }
                        }
                    }
                    // Printing info for first screen
                    PrintInfo();
                    object input = Console.ReadLine();
                    int tempInput = 0;
                    // Checking the type of input
                    if (int.TryParse((string)input, out tempInput))
                    {
                        // Checking if number is valid
                        if (Courses.Count > 0)
                        {
                            if (tempInput > 0 && tempInput <= Courses.Count)
                            {
                                //Going to the second screen
                                bool done2 = false;
                                do
                                {
                                    Evaluation evaluation = new Evaluation();
                                    bool valid2;
                                    // Printing title
                                    Console.Clear();
                                    PrintTitle();
                                    // Checking if there are evaluations
                                    if (Courses[tempInput - 1].Evalulations == null)
                                    {
                                        Console.WriteLine("There are currently no evalutions for " + Courses[tempInput - 1].Code);
                                        PrintInfo2();
                                        object input2 = Console.ReadLine();
                                        int tempInput2 = 0;
                                        // Checking second input to see if number is valid
                                        if (int.TryParse((string)input2, out tempInput2))
                                        {
                                            if (Courses[tempInput - 1].Evalulations.Count > 0)
                                            {
                                                bool done3 = false;
                                                bool valid3;
                                                if (tempInput2 > 0 && tempInput2 <= Courses[tempInput - 1].Evalulations.Count)
                                                {
                                                    // Going to the third screen
                                                    do
                                                    {
                                                        // Printing title
                                                        Console.Clear();
                                                        PrintTitle();
                                                        Console.Write("Marks Earned\t");
                                                        Console.Write("Out Of\t");
                                                        Console.Write("Percent\t");
                                                        Console.Write("Course Marks\t");
                                                        Console.WriteLine("Weight/100\n");
                                                        // Checking if there is earned marks
                                                        if (Courses[tempInput - 1].Evalulations[tempInput2 - 1].EarnedMarks == null)
                                                        {
                                                            Console.Write("\t");
                                                            Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput2 - 1].OutOf) + "\t");
                                                            Console.Write("\t");
                                                        }
                                                        else
                                                        {
                                                            Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput2 - 1].EarnedMarks) + "\t\t");
                                                            Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput2 - 1].OutOf) + "\t\t");
                                                            Console.Write(String.Format("{0:0.0}", CalcCoursePercent(Courses[tempInput - 1].Evalulations[tempInput2 - 1].EarnedMarks, Courses[tempInput - 1].Evalulations[tempInput2 - 1].OutOf)) + "\t\t");
                                                            Console.Write(String.Format("{0:0.0}", CalcCourseMarks(CalcCoursePercent(Courses[tempInput - 1].Evalulations[tempInput2 - 1].EarnedMarks, Courses[tempInput - 1].Evalulations[tempInput2 - 1].OutOf), Courses[tempInput - 1].Evalulations[tempInput2 - 1].Weight)) + "\t\t");
                                                        }
                                                        Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput2 - 1].Weight) + "\n");
                                                        PrintInfo3();
                                                        object input3 = Console.ReadLine();
                                                        // Checking input, no need to check type since only letters
                                                        // Delete
                                                        if (input3.Equals("D"))
                                                        {
                                                            // Checking if sure
                                                            Console.Write("Delete " + Courses[tempInput - 1].Evalulations[tempInput2 - 1].Description + "? (Y/N):");
                                                            string data = Console.ReadLine();
                                                            if (data.Equals("Y"))
                                                            {
                                                                // Deleting evaluations
                                                                if (Courses[tempInput - 1].Evalulations.Count > 1)
                                                                {
                                                                    Courses[tempInput - 1].Evalulations.RemoveAt(tempInput2 - 1);

                                                                }
                                                                // Deleting evalation list
                                                                else
                                                                {
                                                                    Courses[tempInput - 1].Evalulations = null;
                                                                }
                                                                done3 = true;
                                                            }
                                                        }
                                                        // Edit
                                                        else if (input3.Equals("E"))
                                                        {
                                                            do
                                                            {
                                                                // Asking for input
                                                                Console.Write("Enter the marks earned out of " + Courses[tempInput - 1].Evalulations[tempInput2 - 1].OutOf + ", press ENTER to leave unassigned:");
                                                                string data = Console.ReadLine();
                                                                // Validating input
                                                                if (data == "")
                                                                {
                                                                    evaluation.EarnedMarks = null;
                                                                    valid3 = true;
                                                                }
                                                                else
                                                                {
                                                                    double temp;
                                                                    valid3 = double.TryParse(data, out temp);
                                                                    if (valid3)
                                                                        evaluation.EarnedMarks = temp;
                                                                    else
                                                                        Console.WriteLine("\tERROR: 'Marks earned' must be a number or null");
                                                                }
                                                            } while (!valid3);// End of validation loop
                                                            // Editing Earned marks and validating object
                                                            Courses[tempInput - 1].Evalulations[tempInput2 - 1].EarnedMarks = evaluation.EarnedMarks;
                                                            valid3 = ValidateItem(Courses[tempInput - 1], json_schema);
                                                            if (!valid3)
                                                                Console.WriteLine("\nERROR:\tItem data does not match the required format. Please try again.\n");


                                                        }
                                                        // Cancel
                                                        else if (input3.Equals("X"))
                                                        {
                                                            done3 = true;
                                                        }
                                                    } while (!done3);// End of third screen loop
                                                }
                                                // Validation of evaluation number
                                                else
                                                {
                                                    Console.WriteLine("\nERROR:\tThat course doesn't exist, please input a new number.\n");
                                                }
                                            }
                                            // Validation of course number
                                            else
                                            {
                                                Console.WriteLine("\nERROR:\tNo courses exist, please input a new course.\n");
                                            }
                                        }
                                        // Checking second input to see if string
                                        else if (input2.GetType() == typeof(string))
                                        {
                                            // Delete
                                            if (input2.Equals("D"))
                                            {
                                                // Checking if sure
                                                Console.Write("Delete " + Courses[tempInput - 1].Code + "? (Y/N):");
                                                string data = Console.ReadLine();
                                                // Deleting courses
                                                if (data.Equals("Y"))
                                                {
                                                    Courses.RemoveAt(tempInput - 1);
                                                    done2 = true;
                                                }
                                            }
                                            // Add
                                            else if (input2.Equals("A"))
                                            {
                                                do
                                                {
                                                    // Having user add evalutions while validating everything
                                                    Console.Write("Enter a description:");
                                                    evaluation.Description = Console.ReadLine();
                                                    do
                                                    {
                                                        Console.Write("Enter the 'out of' mark:");
                                                        string data = Console.ReadLine();
                                                        int temp;
                                                        valid2 = int.TryParse(data, out temp);
                                                        if (valid2)
                                                            evaluation.OutOf = temp;
                                                        else
                                                            Console.WriteLine("\tERROR: 'Out of' must be a integer number");
                                                    } while (!valid2);
                                                    valid2 = false;
                                                    do
                                                    {
                                                        Console.Write("Enter the % weight:");
                                                        string data = Console.ReadLine();
                                                        double temp;
                                                        valid2 = double.TryParse(data, out temp);
                                                        if (valid2)
                                                            evaluation.Weight = temp;
                                                        else
                                                            Console.WriteLine("\tERROR: 'Weight' must be a number");
                                                    } while (!valid2);
                                                    valid2 = false;
                                                    do
                                                    {
                                                        Console.Write("Enter the marks earned or press ENTER to skip:");
                                                        string data = Console.ReadLine();
                                                        if (data == "")
                                                        {
                                                            evaluation.EarnedMarks = null;
                                                            valid2 = true;
                                                        }
                                                        else
                                                        {
                                                            double temp;
                                                            valid2 = double.TryParse(data, out temp);
                                                            if (valid2)
                                                                evaluation.EarnedMarks = temp;
                                                            else
                                                                Console.WriteLine("\tERROR: 'Marks earned' must be a number or null");
                                                        }
                                                    } while (!valid2);
                                                    // Making sure evaluation list exists
                                                    if (Courses[tempInput - 1].Evalulations == null)
                                                    {
                                                        Courses[tempInput - 1].Evalulations = new List<Evaluation>();
                                                    }
                                                    // Adding evalution
                                                    Courses[tempInput - 1].Evalulations.Add(evaluation);
                                                    // Validating Courses
                                                    valid = ValidateItem(Courses[tempInput - 1], json_schema);

                                                    if (!valid)
                                                        Console.WriteLine("\nERROR:\tItem data does not match the required format. Please try again.\n");
                                                } while (!valid);//End of validation loop
                                            }
                                            // Cancel
                                            else if (input2.Equals("X"))
                                            {
                                                done2 = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Printing out evaluations
                                        Console.Write("#. Evaluation\t");
                                        Console.Write("Marks Earned\t");
                                        Console.Write("Out Of\t");
                                        Console.Write("Percent\t");
                                        Console.Write("Course Marks\t");
                                        Console.WriteLine("Weight/100\n");
                                        for (int i = 0; i < Courses[tempInput - 1].Evalulations.Count; i++)
                                        {
                                            Console.Write(i + 1 + ". ");
                                            Console.Write(Courses[tempInput - 1].Evalulations[i].Description + "\t");
                                            // Making sure marks earned is there
                                            if (Courses[tempInput - 1].Evalulations[i].EarnedMarks == null)
                                            {
                                                Console.Write("\t\t");
                                                Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[i].OutOf) + "\t");
                                                Console.Write("0.0\t\t");
                                                Console.Write("0.0\t\t");
                                            }
                                            else
                                            {
                                                Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[i].EarnedMarks) + "\t\t");
                                                Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[i].OutOf) + "\t");
                                                Console.Write(String.Format("{0:0.0}", CalcCoursePercent(Courses[tempInput - 1].Evalulations[i].EarnedMarks, Courses[tempInput - 1].Evalulations[i].OutOf)) + "\t\t");
                                                Console.Write(String.Format("{0:0.0}", CalcCourseMarks(CalcCoursePercent(Courses[tempInput - 1].Evalulations[i].EarnedMarks, Courses[tempInput - 1].Evalulations[i].OutOf), Courses[tempInput - 1].Evalulations[i].Weight)) + "\t\t");
                                            }
                                            Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[i].Weight) + "\n");
                                        }
                                        PrintInfo2();
                                        object input2 = Console.ReadLine();
                                        int tempInput3 = 0;
                                        // Checking if input is a number
                                        if (int.TryParse((string)input2, out tempInput3))
                                        {
                                            // Check that number is valid
                                            if (Courses[tempInput - 1].Evalulations.Count > 0)
                                            {
                                                bool done3 = false;
                                                bool valid3;
                                                if (tempInput3 > 0 && tempInput3 <= Courses[tempInput - 1].Evalulations.Count)
                                                {
                                                    do
                                                    {
                                                        // Printing out that evalutions numbers
                                                        Console.Clear();
                                                        PrintTitle();
                                                        Console.Write("Marks Earned\t");
                                                        Console.Write("Out Of\t");
                                                        Console.Write("Percent\t");
                                                        Console.Write("Course Marks\t");
                                                        Console.WriteLine("Weight/100\n");
                                                        // Checking if earned marks is null
                                                        if (Courses[tempInput - 1].Evalulations[tempInput3 - 1].EarnedMarks == null)
                                                        {
                                                            Console.Write("\t\t");
                                                            Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput3 - 1].OutOf) + "\t");
                                                            Console.Write("0.0\t\t");
                                                            Console.Write("0.0\t\t");
                                                        }
                                                        else
                                                        {
                                                            Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput3 - 1].EarnedMarks) + "\t\t");
                                                            Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput3 - 1].OutOf) + "\t");
                                                            Console.Write(String.Format("{0:0.00}", CalcCoursePercent(Courses[tempInput - 1].Evalulations[tempInput3 - 1].EarnedMarks, Courses[tempInput - 1].Evalulations[tempInput3 - 1].OutOf)) + "\t\t");
                                                            Console.Write(String.Format("{0:0.0}", CalcCourseMarks(CalcCoursePercent(Courses[tempInput - 1].Evalulations[tempInput3 - 1].EarnedMarks, Courses[tempInput - 1].Evalulations[tempInput3 - 1].OutOf), Courses[tempInput - 1].Evalulations[tempInput3 - 1].Weight)) + "\t\t");
                                                        }
                                                        Console.Write(String.Format("{0:0.0}", Courses[tempInput - 1].Evalulations[tempInput3 - 1].Weight) + "\n");
                                                        PrintInfo3();
                                                        object input3 = Console.ReadLine();
                                                        //Delete
                                                        if (input3.Equals("D"))
                                                        {
                                                            // Checking for confirmation
                                                            Console.Write("Delete " + Courses[tempInput - 1].Evalulations[tempInput3 - 1].Description + "? (Y/N):");
                                                            string data = Console.ReadLine();
                                                            if (data.Equals("Y"))
                                                            {
                                                                // Deleting evaluation if list is big enough
                                                                if (Courses[tempInput - 1].Evalulations.Count > 1)
                                                                {
                                                                    Courses[tempInput - 1].Evalulations.RemoveAt(tempInput3 - 1);

                                                                }
                                                                // Deleting evaluation list if only evaluation
                                                                else
                                                                {
                                                                    Courses[tempInput - 1].Evalulations = null;
                                                                }
                                                                done3 = true;
                                                            }
                                                        }
                                                        // Edit
                                                        else if (input3.Equals("E"))
                                                        {
                                                            do
                                                            {
                                                                // Asking for new marks earned
                                                                Console.Write("Enter the marks earned out of " + Courses[tempInput - 1].Evalulations[tempInput3 - 1].OutOf + ", press ENTER to leave unassigned:");
                                                                string data = Console.ReadLine();
                                                                // Validating data
                                                                if (data == "")
                                                                {
                                                                    evaluation.EarnedMarks = null;
                                                                    valid3 = true;
                                                                }
                                                                else
                                                                {
                                                                    double temp;
                                                                    valid3 = double.TryParse(data, out temp);
                                                                    if (valid3)
                                                                        evaluation.EarnedMarks = temp;
                                                                    else
                                                                        Console.WriteLine("\tERROR: 'Marks earned' must be a number or null");
                                                                }
                                                            } while (!valid3);// End of validation loop
                                                            // Setting new marks earned
                                                            Courses[tempInput - 1].Evalulations[tempInput3 - 1].EarnedMarks = evaluation.EarnedMarks;
                                                            // Validating courses
                                                            valid3 = ValidateItem(Courses[tempInput - 1], json_schema);

                                                            if (!valid3)
                                                                Console.WriteLine("\nERROR:\tItem data does not match the required format. Please try again.\n");


                                                        }
                                                        // Cancel
                                                        else if (input3.Equals("X"))
                                                        {
                                                            done3 = true;
                                                        }

                                                    } while (!done3);//End of input loop
                                                }
                                                // Error for bad number
                                                else
                                                {
                                                    Console.WriteLine("\nERROR:\tThat course doesn't exist, please input a new number.\n");
                                                }
                                            }
                                        }
                                        // Checking second input to see if string
                                        else if (input2.GetType() == typeof(string))
                                        {
                                            // Delete
                                            if (input2.Equals("D"))
                                            {
                                                // Asking for confirmation
                                                Console.Write("Delete " + Courses[tempInput - 1].Code + "? (Y/N):");
                                                string data = Console.ReadLine();
                                                // Removing course
                                                if (data.Equals("Y"))
                                                {
                                                    Courses.RemoveAt(tempInput - 1);
                                                    done2 = true;
                                                }
                                            }
                                            // Add
                                            else if (input2.Equals("A"))
                                            {
                                                do
                                                {
                                                    // Asking for input and validating it
                                                    Console.Write("Enter a description:");
                                                    evaluation.Description = Console.ReadLine();
                                                    do
                                                    {
                                                        Console.Write("Enter the 'out of' mark:");
                                                        string data = Console.ReadLine();
                                                        int temp;
                                                        valid2 = int.TryParse(data, out temp);
                                                        if (valid2)
                                                            evaluation.OutOf = temp;
                                                        else
                                                            Console.WriteLine("\tERROR: 'Out of' must be a number");
                                                    } while (!valid2);
                                                    valid2 = false;
                                                    do
                                                    {
                                                        Console.Write("Enter the % weight:");
                                                        string data = Console.ReadLine();
                                                        double temp;
                                                        valid2 = double.TryParse(data, out temp);
                                                        if (valid2)
                                                            evaluation.Weight = temp;
                                                        else
                                                            Console.WriteLine("\tERROR: 'Weight' must be a number");
                                                    } while (!valid2);
                                                    valid2 = false;
                                                    do
                                                    {
                                                        Console.Write("Enter the marks earned or press ENTER to skip:");
                                                        string data = Console.ReadLine();
                                                        if (data == "")
                                                        {
                                                            evaluation.EarnedMarks = null;
                                                            valid2 = true;
                                                        }
                                                        else
                                                        {
                                                            double temp;
                                                            valid2 = double.TryParse(data, out temp);
                                                            if (valid2)
                                                                evaluation.EarnedMarks = temp;
                                                            else
                                                                Console.WriteLine("\tERROR: 'Marks earned' must be a number or null");
                                                        }
                                                    } while (!valid2);
                                                    // Checking if evaluations is null and making new evaluation list
                                                    if (Courses[tempInput - 1].Evalulations == null)
                                                    {
                                                        Courses[tempInput - 1].Evalulations = new List<Evaluation>();
                                                    }
                                                    // Adding new evaluation
                                                    Courses[tempInput - 1].Evalulations.Add(evaluation);
                                                    // Validating courses
                                                    valid = ValidateItem(Courses[tempInput - 1], json_schema);

                                                    if (!valid)
                                                        Console.WriteLine("\nERROR:\tItem data does not match the required format. Please try again.\n");
                                                } while (!valid);// End of validation loop
                                            }
                                            // Cancel
                                            else if (input2.Equals("X"))
                                            {
                                                done2 = true;
                                            }
                                        }
                                    }
                                } while (!done2);// End of second screen

                            }
                            // Making sure course number is correct
                            else
                            {
                                Console.WriteLine("\nERROR:\tThat course doesn't exist, please input a new number.\n");
                            }
                        }
                        // Making sure courses exist before number is selected
                        else
                        {
                            Console.WriteLine("\nERROR:\tNo courses exist, please input a new course.\n");
                        }
                    }
                    // Checking if input is string
                    else if (input.GetType() == typeof(string))
                    {
                        //Add
                        if (input.Equals("A"))
                        {
                            do
                            {
                                // Asking for code input and validating
                                Console.Write("\nEnter a course code: ");
                                item.Code = Console.ReadLine();
                                valid = ValidateItem(item, json_schema);

                                if (!valid)
                                    Console.WriteLine("\nERROR:\tItem data does not match the required format. Please try again.\n");

                            } while (!valid);// End of valid loop
                            // Adding item to courses
                            Courses.Add(item);
                        }
                        // Cancel
                        else if (input.Equals("X"))
                        {
                            done = true;
                        }
                    }
                } while (!done);// End of main screen
                // Validate all object of courses incase i missed a validation
                for (int i = 0; i < Courses.Count; i++)
                {
                    if (!ValidateItem(Courses[i], json_schema))
                    {
                        Console.WriteLine("Courses list does not comply with json_schema.\nPlease modify your courses file");
                        Environment.Exit(1);
                    }
                }
                // Writing list of courses to json file
                string returnFile = JsonConvert.SerializeObject(Courses);
                string Directory = Environment.CurrentDirectory + @"\returngrades.json";
                File.WriteAllText(Directory, returnFile);
            }
            // Making sure schema file exists
            else
            {
                Console.WriteLine("\nERROR:\tUnable to read the schema file.");
            }
        }
        /*
	        Method Name: PrintTitle
	        Input:       void
	        Output:      void
	        Description: Prints the title for each screen
        */
        static public void PrintTitle()
        {
            Console.WriteLine("\t\t~ GRADES TRACKING SYSTEM ~\n");
            Console.WriteLine("+----------------------------------------------------------------+");
            Console.WriteLine("|\t\t\tGrades Summary\t\t\t\t |");
            Console.WriteLine("+----------------------------------------------------------------+");
            Console.WriteLine();
        }
        /*
	        Method Name: PrintInfo
	        Input:       void
	        Output:      void
	        Description: Prints the command info for the first screen
        */
        static public void PrintInfo()
        {
            Console.WriteLine("\n------------------------------------------------------------------");
            Console.WriteLine("Press # from the above list to view/edit/delete a specific course.");
            Console.WriteLine("Press A to add a new course.");
            Console.WriteLine("Press X to quit.");
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write("Enter a command: ");
        }
        /*
	        Method Name: PrintInfo2
	        Input:       void
	        Output:      void
	        Description: Prints the command info for the second screen
        */
        static public void PrintInfo2()
        {
            Console.WriteLine("\n------------------------------------------------------------------");
            Console.WriteLine("Press D to delete this course.");
            Console.WriteLine("Press A to add an evaluation.");
            Console.WriteLine("Press # from the above list to edit/delete a specific evaluation.");
            Console.WriteLine("Press X to quit.");
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write("Enter a command: ");
        }
        /*
	        Method Name: PrintInfo3
	        Input:       void
	        Output:      void
	        Description: Prints the command info for the third screen
        */
        static public void PrintInfo3()
        {
            Console.WriteLine("\n------------------------------------------------------------------");
            Console.WriteLine("Press D to delete this evaluation.");
            Console.WriteLine("Press E to edit this evaluation.");
            Console.WriteLine("Press X to quit.");
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write("Enter a command: ");
        }
        /*
	        Method Name: CalcCourseTotalEarned
	        Input:       double?[]
	        Output:      doube
	        Description: Calculates the total earned marks for a course
        */
        static public double CalcCourseTotalEarned(double?[] earnedMarks)
        {
            double total = 0;
            for (int i = 0; i < earnedMarks.Length; i++)
            {
                if (earnedMarks != null)
                {
                    total = total + (double)earnedMarks[i];
                }
            }
            return total;
        }
        /*
	        Method Name: CalcCourseTotalPotential
	        Input:       double[]
	        Output:      doube
	        Description: Calculates the total potential marks for a course
        */
        static public double CalcCourseTotalPotential(double[] potentialMarks)
        {
            double total = 0;
            for (int i = 0; i < potentialMarks.Length; i++)
            {
                total = total + potentialMarks[i];
            }
            return total;
        }
        /*
	        Method Name: CalcCourseMarks
	        Input:       double, double
	        Output:      doube
	        Description: Calculates how many marks a person earned for the course from a specific evaluation
        */
        static public double CalcCourseMarks(double percentage, double weight)
        {
            double total = 0;
            total = weight * percentage / 100;
            return total;
        }
        /*
	        Method Name: CalcCoursePercent
	        Input:       double?
	        Output:      doube
	        Description: Calculates the percent for a given assignment
        */
        static public double CalcCoursePercent(double? totalEarnedMarks, double totalPotentialMarks)
        {
            double total = 0;
            if (totalEarnedMarks != null)
            {
                total = ((double)totalEarnedMarks / totalPotentialMarks) * 100;
            }
            return total;
        }
        /*
	        Method Name: ValidateItem
	        Input:       item, String
	        Output:      bool
	        Description: Validates an item against a json schema
        */
        private static bool ValidateItem(Item item, string json_schema)
        {
            // Convert item object to a JSON string 
            string json_data = JsonConvert.SerializeObject(item);

            // Validate the data string against the schema contained in the 
            // json_schema parameter. Also, modify or replace the following 
            // return statement to return 'true' if item is valid, or 'false' 
            // if invalid.
            JSchema schema = JSchema.Parse(json_schema);
            JObject itemObj = JObject.Parse(json_data);
            return itemObj.IsValid(schema);

        } 
        /*
	        Method Name: ReadFile
	        Input:       string, out string
	        Output:      bool
	        Description: Reads a file given and gives it back in string format
        */
        private static bool ReadFile(string path, out string json)
        {
            try
            {
                // Read JSON file data 
                json = File.ReadAllText(path);
                return true;
            }
            catch
            {
                json = null;
                return false;
            }
        } // end ReadFile

    }// End of class GradeTracker

    class Item
    {
        public string Code { get; set; }
        public List<Evaluation>? Evalulations { get; set; }

    }

    class CourseHolder
    {
        public List<Item> courses { get; set; }

    } 
    class Evaluation
    {
        public string Description { get; set; }
        public double Weight { get; set; }
        public int OutOf { get; set; }
        public double? EarnedMarks { get; set; }

    } 
}
