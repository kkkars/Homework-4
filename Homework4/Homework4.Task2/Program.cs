using System;
using System.Net.Http;
using System.Threading.Tasks;
using static Homework4.Task2.Authorization;

namespace Homework4.Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Currency conventer (Task 2) by Bilotska Karyna");

            var currencyDataService = new CurrencyDataService("cache.json");

            string initialCurrency = GetInitialCurrency();
            string currencyTo = GetCurrencyTo();
            decimal amount = GetAmount();

            try
            {
                var currencyData = currencyDataService.GetAllCurrencyData();
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("\nError: file not found");
                return;
            }


            try
            {
                currencyDataService.UpdateCacheFile();
                //Console.WriteLine("Currency data was updated successfuly\n");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Currency data can not be updated.\n");
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Can not connect to the host.\nCurrency data can not be updated.\n");
            }


            if ((currencyDataService.CurrencyIsExist(initialCurrency) || initialCurrency == "UAN") && (currencyDataService.CurrencyIsExist(currencyTo) || currencyTo == "UAN"))
            {
                currencyDataService.ConvertCurrency(currencyDataService.GetCurrencyData(initialCurrency), currencyDataService.GetCurrencyData(currencyTo), amount);
            }
            else
                Console.WriteLine($"Pair {initialCurrency}, {currencyTo} was not found");
        }
    }
}
