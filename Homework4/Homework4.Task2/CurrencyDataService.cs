using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;

namespace Homework4.Task2
{
    class CurrencyDataService
    {
        private readonly string _cacheFileName;
        private List<CurrencyData> _currencyData = new List<CurrencyData>();

        public CurrencyDataService(string cacheFileName)
        {
            _cacheFileName = cacheFileName;
        }

        public CurrencyData[] GetAllCurrencyData()
        {
            if (_currencyData.Count != 0) return _currencyData.ToArray();
            var json = File.ReadAllText(_cacheFileName);
            _currencyData = JsonConvert.DeserializeObject<List<CurrencyData>>(json);
            return _currencyData.ToArray();
        }
        public bool UpdateCacheFile()
        {
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);

            var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, @"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json")).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync();

            File.WriteAllText(_cacheFileName, json.GetAwaiter().GetResult());
            return true;
        }
        public CurrencyData GetCurrencyData(string cc)
        {
            CurrencyData currency = new CurrencyData("UAH");
            foreach (CurrencyData currData in _currencyData)
            {
                if (currData.Cc == cc)
                    currency = currData;
            }
            return currency;
        }
        public bool CurrencyIsExist(string cc)
            => cc == "UAH" || _currencyData.Any(currency => currency.Cc == cc);
        public void ConvertCurrency(CurrencyData initialCurrency, CurrencyData currencyTo, decimal amount)
        {
            if (initialCurrency.Cc == currencyTo.Cc)
                Console.WriteLine($"{amount} {initialCurrency.Cc} x 1 = {amount} {currencyTo.Cc}");
            else
            {
                if (initialCurrency.Cc == "UAH")
                    Console.WriteLine($"{amount} \"UAH\" / {currencyTo.Rate} = {Math.Round(amount / (decimal)currencyTo.Rate, 2)} {currencyTo.Cc} (from {currencyTo.ExchangeDate})");
                else
                if (currencyTo.Cc == "UAH")
                    Console.WriteLine($"{amount} {initialCurrency.Cc} x {initialCurrency.Rate} = {Math.Round(amount * (decimal)initialCurrency.Rate, 2)} \"UAH\" (from {initialCurrency.ExchangeDate})");
                else
                    Console.WriteLine($"{amount} {initialCurrency.Cc} x {initialCurrency.Rate / currencyTo.Rate} = {Math.Round((amount * (decimal)(initialCurrency.Rate / currencyTo.Rate)), 2)} {currencyTo.Cc} (from {initialCurrency.ExchangeDate})");
            }
        }
    }
}
