/*
 * Program:         BudgetBuilderClient
 * Module:          Program.cs
 * Date:            April 19, 2022
 * Coder:           Bohdan Simakov
 * Description:     A console client to test BudgetBuilderLibrary.
 */

using System;
using System.Linq;
using BudgetBuilderLibrary;

namespace BudgetBuilderClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Print title
            printHeader(60, "Budget Builder", true);

            // Declare and initialize an Budget object variable called 'budget'
            Budget budget = new Budget();
            Console.Write($"\nEnter a description for the budget: ");
            budget.Description = Console.ReadLine();

            // Obtain data for each expense and display a summary
            int count = 0;
            do
            {
                // New expense inputs
                Console.Write($"\nExpense {++count} description: ");
                string description = Console.ReadLine();
                double amount = getUserAmount();
                FrequencyType frequency = getUserFrequency();

                // Add expense
                budget.AddExpense(description, amount, frequency);

            } while (!endUserInput());

            // Summary report of the monthy budget
            Console.WriteLine();
            printHeader(60, $"{budget.Description}", true);
            foreach (Expense expense in budget.GetExpenses())
                Console.WriteLine(expense);
            printHeader(60, String.Format("Total monthly budget = {0:C}", budget.CalcMonthlyAmount()), false);

        } // end Main()

        #region helper_code

        // Obtains a frequency for an expense from the user
        private static FrequencyType getUserFrequency()
        {
            string input = null;
            FrequencyType frequency;
            bool validInput;
            do
            {
                Console.Write("Frequency (weekly, monthly or yearly): ");
                input = Console.ReadLine();
                input = Char.ToUpper(input[0]) + input.Substring(1);
                if (!(validInput = Enum.TryParse<FrequencyType>(input, out frequency)))
                    Console.WriteLine("ERROR: Input must be weekly, monthly or yearly.");
            } while (!validInput);
            return frequency;
        } // end getUserTextData()

        // Obtains a valid expense amount from the user 
        private static double getUserAmount()
        {
            string input;
            double amount;
            bool validInput;
            do
            {
                Console.Write("Amount: $");
                input = Console.ReadLine();
                if (!(validInput = double.TryParse(input, out amount) && amount > 0))
                    Console.WriteLine($"ERROR: Amount must be a number greater than 0.");
            } while (!validInput);
            return amount;
        } // end getUserRealData()

        // Returns true if the user has no more expenses to input
        private static bool endUserInput()
        {
            Console.Write("\nAdd another expense? (y/n): ");
            char input = Console.ReadKey().KeyChar;
            while (!"yn".Contains(input))
            {
                Console.Write("\nInput must be y or n. Add another expense?: ");
                input = Console.ReadKey().KeyChar;
            }
            Console.WriteLine();
            return input == 'n';
        }

        // Prints a horizontal line of "length" hyphens
        private static void printHeader(int width, string text, bool centre)
        {
            if (text != null)
            {
                string line = new string('-', width);
                Console.WriteLine(line);
                string format = "{0" + (centre ? "," + (width + text.Length) / 2 : "") + "}";
                Console.WriteLine(format, text);
                Console.WriteLine(line);
            }
        } // end printLine();

        #endregion

    }
}
