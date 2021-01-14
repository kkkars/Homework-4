namespace Homework4.Task2
{
    public class CurrencyData
    {
        public float Rate { get; set; }
        public string Cc { get; set; }
        public string ExchangeDate { get; set; }
        public CurrencyData(string cc)
        {
            Cc = cc;
        }
    }
}
