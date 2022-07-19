/*
 * Program:         BudgetBuilderLibrary
 * Module:          Budget.cs
 * Date:            April 19, 2022
 * Coder:           Bohdan Simakov
 * Description:     Defines a Budget class that manages a collection of household expenses.
 */

using System;
using System.Collections.Generic;

namespace BudgetBuilderLibrary
{
    public class Budget
    {
        // Class variables.
        private List<Expense> expenses = null;
        private static uint objCount = 0;
        private uint objId;

        // C'tor
        public Budget()
        {
            objId = ++objCount;
            Console.WriteLine($"Budget #{objId} created.");

            expenses = new List<Expense>();
        }
        public string Description { get; set; }
        
        // Add another expense to the budget
        public void AddExpense(string description, double amount, FrequencyType frequency)
        {
            Console.WriteLine($"Budget #{objId} adding \"{description}\" of {amount:C} to be paid {frequency}.");
            expenses.Add(new Expense(description, amount, frequency));
        }

        // Returns the total amount of money that must be paid monthly to carry all expenses
        public double CalcMonthlyAmount()
        {
            double total = 0;
            foreach (Expense expense in expenses)
                total += expense.GetMonthlyAmount();
            return total;
        }

        // Returns an array of all expenses in the budget
        public Expense[] GetExpenses()
        {
            return expenses.ToArray();
        }

    } // end of class Budget

} // end of namespace
