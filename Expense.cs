/*
 * Program:         BudgetBuilderLibrary
 * Module:          Expense.cs
 * Date:            April 19, 2022
 * Coder:           Bohdan Simakov
 * Description:     Defines an Expense class that represents a household expense.
 */

using System;

namespace BudgetBuilderLibrary
{
    public class Expense
    {
        // 3-arg. c'tor.

        internal Expense(string description, double amount, FrequencyType frequency)
        {
            this.Description = description;
            this.Amount = amount;
            this.Frequency = frequency;
        }

        // Class properties.

        public string Description { get; private set; }

        public double Amount { get; private set; }

        public FrequencyType Frequency { get; private set; }


        /* Method Name: GetMonthlyAmount
         * Input:       void
         * Output:      double
         * Description: Based on frequency type calculate the monthly amount.
        */
        public double GetMonthlyAmount()
        {
            double monthly = 0;
            switch (Frequency)
            {
                case FrequencyType.Weekly:
                    monthly = (Amount * 52) / 12;
                    break;
                case FrequencyType.Monthly:
                    monthly = Amount;
                    break;
                case FrequencyType.Yearly:
                    monthly = Amount / 12;
                    break;
                default:
                    monthly = 0;
                    break;
            }
            return monthly;
        }

        public override string ToString()
        {
            return string.Format("{0} @ {1:C} {2} = {3:C}",
                Description, Amount, Frequency, GetMonthlyAmount());
        }
    }
}
