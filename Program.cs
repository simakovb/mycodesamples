using System;
using System.Xml;
using System.Xml.XPath;

/*
 * File name:   Program.cs 
 * Description: C# console application that uses XML file for
                data storage and generates reports based on user input.
 * Date:        2021-07-29
 * Coder:      Bohdan Simakov
 */
namespace Project_2
{
    class Program
    {
        private const string GHG_FILE = "ghg-canada.xml";
        static void Main(string[] args)
        {
            try
            {
                // Declare temporary variables and set default starting/end years.
                bool done = false;
                int startingYear = 2015;
                int endingYear = 2019;
                XmlDocument doc = new XmlDocument();
                doc.Load(GHG_FILE);
                do
                {
                    Console.Clear();
                    PrintTitle();
                    PrintInfo();
                    object input = Console.ReadLine();
                    // if-clauses for user to navigate through menu.
                    if (input.Equals("Y"))
                        // Set years.
                    {
                        bool valid = false;
                        do
                        {
                            Console.Write("\nStarting year (1990 to 2019): ");
                            object input2 = Console.ReadLine();

                            if (int.TryParse((string)input2, out startingYear))
                            {
                                // Validate user input.
                                if (startingYear >= 1990 && startingYear <= 2019)
                                {
                                    valid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Starting Year must be an integer between 1990 and 2019.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Starting Year must be an integer between 1990 and 2019.");
                            }
                        } while (!valid);
                        valid = false;
                        do
                        {
                            Console.Write("\nEnding year (1990 to 2019): ");
                            object input2 = Console.ReadLine();
                            if (int.TryParse((string)input2, out endingYear))
                            {
                                // Validation for correct range of years.
                                if (endingYear >= 1990 && endingYear <= 2019 && endingYear <= startingYear + 5)
                                {
                                    valid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Ending Year must be an integer between 1990 and 2019.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ending Year must be an integer between 1990 and 2019.");
                            }
                        } while (!valid);
                    }
                    else if (input.Equals("R"))
                    {
                        // Generates report based on regions.
                        bool valid = false;
                        int tempInput = 0;
                        do
                        {
                            PrintRegions();
                            object input2 = Console.ReadLine();

                            if (int.TryParse((string)input2, out tempInput))
                            {
                                // Validate user input.
                                if (tempInput >= 1 && tempInput <= 15)
                                {
                                    valid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Selection must be an integer between 1 and 15.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Selection must be an integer between 1 and 15.\n");
                            }
                        } while (!valid);
                        // Display data.
                        PrintRegionGHG(doc, tempInput, startingYear, endingYear);
                    }
                    else if (input.Equals("S"))
                    {
                        // Generate report based on sources.
                        bool valid = false;
                        int tempInput = 0;
                        do
                        {
                            PrintSources();
                            object input2 = Console.ReadLine();

                            if (int.TryParse((string)input2, out tempInput))
                            {
                                if (tempInput >= 1 && tempInput <= 8)
                                {
                                    valid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Selection must be an integer between 1 and 15.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Selection must be an integer between 1 and 15.");
                            }
                        } while (!valid);
                        //Need to modify so that print actual data
                        PrintSourcesGHG(doc, tempInput, startingYear, endingYear);
                    }
                    else if (input.Equals("X"))
                    {
                        done = true;
                    }
                } while (!done);
                Console.WriteLine("\nAll Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        } // end of Main

        /* Method Name: PrintTitle
         * Takes: void
         * Returns: void
         * Purpose: Display title for the application.
         */
        static public void PrintTitle()
        {
            Console.WriteLine("Greenhouse Gas Emissions in Canada");
            Console.WriteLine("====================================================");
            Console.WriteLine();
        }

        /* Method Name: PrintInfo
         * Takes: void
         * Returns: void
         * Purpose: Display menu for the application.
         */
        static public void PrintInfo()
        {
            Console.WriteLine("'Y' to adjust the range of years");
            Console.WriteLine("'R' to select a region");
            Console.WriteLine("'S' to select a specific GHG source");
            Console.WriteLine("'X' to exit the program");
            Console.Write("Your Selection: ");
        }

        /* Method Name: PrintRegions
        * Takes: void
        * Returns: void
        * Purpose: Display the list of regions for user to choose from.
        */
        static public void PrintRegions()
        {
            Console.WriteLine("Select a region by number as shown below...");
            Console.WriteLine("  1. Alberta");
            Console.WriteLine("  2. British Columbia");
            Console.WriteLine("  3. Manitoba");
            Console.WriteLine("  4. New Brunswick");
            Console.WriteLine("  5. Newfoundland and Labrador");
            Console.WriteLine("  6. Northwest Territories");
            Console.WriteLine("  7. Northwest Territories and Nunavut");
            Console.WriteLine("  8. Nova Scotia");
            Console.WriteLine("  9. Nuavut");
            Console.WriteLine(" 10. Ontario");
            Console.WriteLine(" 11. Prince Edward Island");
            Console.WriteLine(" 12. Qubec");
            Console.WriteLine(" 13. Saskatchewan");
            Console.WriteLine(" 14. Yukon");
            Console.WriteLine(" 15. Canada");
            Console.WriteLine();
            Console.Write("Enter a Region #: ");
        }

        /* Method Name: PrintSources
        * Takes: void
        * Returns: void
        * Purpose: Display the list of sources for user to choose from.
        */
        static public void PrintSources()
        {
            Console.WriteLine("Select a source by number as shown below...");
            Console.WriteLine("  1. Agriculture");
            Console.WriteLine("  2. Buildings");
            Console.WriteLine("  3. Heavy Industry");
            Console.WriteLine("  4. Light Manufacturing, Construction and Forest Resources");
            Console.WriteLine("  5. Oil and Gas");
            Console.WriteLine("  6. Transport");
            Console.WriteLine("  7. Waste");
            Console.WriteLine("  8. Total");
            Console.WriteLine();
            Console.Write("Enter a Source #: ");
        }

        /* Method Name: PrintRegionGHG
        * Takes: XmlDocument, int, int, int
        * Returns: void
        * Purpose: Generate and display tabular report based on selected region(province or territory). 
        */
        static public void PrintRegionGHG(XmlDocument doc, int selection, int startingYear, int endingYear)
        {
            XPathNavigator nav = doc.CreateNavigator();
            XmlNodeList sourceList = doc.GetElementsByTagName("source");
            int difYear = (endingYear - startingYear) + 1;
            // Navigate through xml file using if-clause, based on user selection.
            if (selection == 1)
            {
                Console.WriteLine("Emissions in Alberta (Megatonnes)");
                Console.WriteLine("---------------------------------");

                // Find data for requested territory.
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");

                // Prints years, lists of sources and data.
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }

            }
            else if (selection == 2)
            {
                Console.WriteLine("Emissions in British Columbia (Megatonnes)");
                Console.WriteLine("------------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'British Columbia']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 3)
            {
                Console.WriteLine("Emissions in Manitoba (Megatonnes)");
                Console.WriteLine("----------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Manitoba']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }

            }
            else if (selection == 4)
            {
                Console.WriteLine("Emissions in New Brunswick (Megatonnes)");
                Console.WriteLine("---------------------------------------");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'New Brunswick']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 5)
            {
                Console.WriteLine("Emissions in Newfoundland and Labrador (Megatonnes)");
                Console.WriteLine("---------------------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Newfoundland and Labrador']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 6)
            {
                Console.WriteLine("Emissions in Northwest Territories (Megatonnes)");
                Console.WriteLine("-----------------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Northwest Territories']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 7)
            {
                Console.WriteLine("Emissions in Northwest Territories and Nunavut (Megatonnes)");
                Console.WriteLine("-----------------------------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Northwest Territories and Nunavut']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for(int j=0; j<difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 8)
            {
                Console.WriteLine("Emissions in Nova Scotia (Megatonnes)");
                Console.WriteLine("-------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Nova Scotia']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 9)
            {
                Console.WriteLine("Emissions in Nunavut (Megatonnes)");
                Console.WriteLine("---------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Nunavut']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 10)
            {
                Console.WriteLine("Emissions in Ontario (Megatonnes)");
                Console.WriteLine("---------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Ontario']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 11)
            {
                Console.WriteLine("Emissions in Prince Edward Island (Megatonnes)");
                Console.WriteLine("----------------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Prince Edward Island']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 12)
            {
                Console.WriteLine("Emissions in Qubec (Megatonnes)");
                Console.WriteLine("-------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Quebec']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 13)
            {
                Console.WriteLine("Emissions in Saskatchewan (Megatonnes)");
                Console.WriteLine("--------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Saskatchewan']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 14)
            {
                Console.WriteLine("Emissions in Yukon (Megatonnes)");
                Console.WriteLine("-------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Yukon']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 15)
            {
                Console.WriteLine("Emissions in Canada (Megatonnes)");
                Console.WriteLine("--------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region[@name = 'Canada']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator sourceHelper = nav.Select("//region[@name = 'Alberta']/source/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../@description");
                sourceHelper.MoveNext();
                Console.Write("\n{0,-60}", "Source");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int sourcesListCount = 0;
                    bool sourceMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (sourceMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                sourcesListCount++;
                                if (sourceMoveNext)
                                {
                                    sourceHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", sourceList[sourcesListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(sourceHelper.Current.Value.Equals(sourceList[sourcesListCount].Attributes[0].Value)))
                        {
                            sourceMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }

                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            sourceMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }
                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("\n{0,-60}", sourceList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            Console.WriteLine("\n");
            Console.Write("Press any key to continue");
            Console.ReadKey();

        }

        /* Method Name: PrintSourcesGHG
        * Takes: XmlDocument, int, int, int
        * Returns: void
        * Purpose: Generate and display tabular report based on selected source of GHG. 
        */
        static public void PrintSourcesGHG(XmlDocument doc, int selection, int startingYear, int endingYear)
        {
            XPathNavigator nav = doc.CreateNavigator();
            XmlNodeList regionList = doc.GetElementsByTagName("region");
            int difYear = (endingYear - startingYear) + 1;
            // Navigate through xml file using if-clause, based on user selection.
            if (selection == 1)
            {
                Console.WriteLine("Emmisions from Agriculture (Megatonnes)");
                Console.WriteLine("---------------------------------------");

                // Find data for selected sources.
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Agriculture']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Agriculture']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();


                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }

            }
            else if (selection == 2)
            {
                Console.WriteLine("Emmisions from Buildings (Megatonnes)");
                Console.WriteLine("-------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Buildings']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Buildings']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();
                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 3)
            {
                Console.WriteLine("Emmisions from Heavy Industry (Megatonnes)");
                Console.WriteLine("------------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Heavy Industry']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Heavy Industry']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();
                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 4)
            {
                Console.WriteLine("Emmisions from Light Manufacturing, Construction and Forest Resources (Megatonnes)");
                Console.WriteLine("----------------------------------------------------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Light Manufacturing, Construction and Forest Resources']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Light Manufacturing, Construction and Forest Resources']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();
                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 5)
            {
                Console.WriteLine("Emmisions from Oil and Gas (Megatonnes)");
                Console.WriteLine("---------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Oil and Gas']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Oil and Gas']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();
                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 6)
            {
                Console.WriteLine("Emmisions from Transport (Megatonnes)");
                Console.WriteLine("-------------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Transport']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Transport']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();
                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 7)
            {
                Console.WriteLine("Emmisions from Waste (Megatonnes)");
                Console.WriteLine("---------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Waste']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Waste']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();
                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            }
            else if (selection == 8)
            {
                Console.WriteLine("Emmisions from Total (Megatonnes)");
                Console.WriteLine("---------------------------------");
                XPathNodeIterator nodeIt = nav.Select("//region/source[@description='Total']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']");
                XPathNodeIterator regionHelper = nav.Select("//region/source[@description='Total']/emissions[@year>='" + startingYear + "' and @year<='" + endingYear + "']/../../@name");
                regionHelper.MoveNext();
                Console.Write("\n{0,-60}", "Region");
                for (int i = startingYear; i <= endingYear; i++)
                {
                    Console.Write("{0, -15}", i);
                }
                Console.WriteLine();
                if (nodeIt.Count > 0)
                {
                    int regionListCount = 0;
                    bool regionMoveNext = true;
                    bool first = true;
                    bool canMove = true;
                    bool newRow = false;
                    do
                    {
                        if (regionMoveNext)
                        {
                            canMove = nodeIt.MoveNext();
                        }
                        if (canMove == false)
                        {
                            break;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + startingYear) || newRow)
                        {
                            if (!first)
                            {
                                regionListCount++;
                                if (regionMoveNext)
                                {
                                    regionHelper.MoveNext();
                                }
                            }
                            Console.Write("\n{0,-60}", regionList[regionListCount].Attributes[0].Value);
                            newRow = false;
                        }
                        if (nodeIt.Current.GetAttribute("year", nav.NamespaceURI).Equals("" + endingYear))
                        {
                            newRow = true;
                        }
                        if (!(regionHelper.Current.Value.Equals(regionList[regionListCount].Attributes[0].Value)))
                        {
                            regionMoveNext = false;
                            for (int i = 0; i < difYear; i++)
                            {
                                Console.Write("{0, -15}", "-");
                            }
                        }
                        else
                        {
                            double temp = 0;
                            double.TryParse(nodeIt.Current.Value, out temp);
                            regionMoveNext = true;
                            Console.Write("{0, -15}", Math.Round(temp, 3));
                            first = false;
                        }

                    } while (canMove);
                }
                else
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Console.Write("\n{0,-60}", regionList[i].Attributes[0].Value);
                        for (int j = 0; j < difYear; j++)
                        {
                            Console.Write("{0, -15}", "-");
                        }
                    }
                }
            } 
            Console.WriteLine("\n");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
    } // end of class Program
} // end of namespace Project_2
