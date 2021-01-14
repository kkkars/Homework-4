using System;

namespace Homework4.Task2
{
    public static class Authorization
    {
        public static string GetInitialCurrency()
        {
            string initialCurrency;
            do
            {
                Console.Write("Enter the initial currency: ");
                initialCurrency = Console.ReadLine().Trim().ToUpper();
                if (!CurrencyIsValid(initialCurrency))
                    Console.WriteLine("Please, try again.");
            } while (!CurrencyIsValid(initialCurrency));
            return initialCurrency;
        }
        public static string GetCurrencyTo()
        {
            string currencyTo;
            do
            {
                Console.Write("Enter the currency you want to convert to: ");
                currencyTo = Console.ReadLine().Trim().ToUpper();
                if (!CurrencyIsValid(currencyTo))
                    Console.WriteLine("Please, try again.");
            } while (!CurrencyIsValid(currencyTo));
            return currencyTo;
        }
        public static decimal GetAmount()
        {
            decimal amount = default;
            bool isCorrect;
            do
            {
                do
                {
                    Console.Write("Enter the sum to convert: ");
                    try
                    {
                        amount = Convert.ToInt32(Console.ReadLine());
                        isCorrect = true;
                    }
                    catch
                    {
                        isCorrect = false;
                        Console.WriteLine("Please, try again.");
                    }
                } while (!isCorrect);

                if (!AmountIsValid(amount))
                    Console.WriteLine("Please, try again.");
            } while (!AmountIsValid(amount));
            return amount;
        }

        private static bool CurrencyIsValid(string currency)
        {
            if (currency.Length == 3)
                return true;
            else
            {
                Console.WriteLine("Wrong currency.");
                return false;
            }
        }
        private static bool AmountIsValid(decimal amount)
        {
            if (amount > 0)
                return true;
            else
            {
                Console.WriteLine("Wrong amount.");
                return false;
            }
        }
    }
}
