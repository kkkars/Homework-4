using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Homework4.Task1
{
    class Program
    {
        static Stopwatch timer = new Stopwatch();
        static void Main(string[] args)
        {
            timer.Start();

            var settingService = new SettingsService("settings.json");

            try
            {
                var settings = settingService.GetSettings();
                int[] primeNumbers = GetPrimeNumbers(settings.LowerBound, settings.UpperBound);
                timer.Stop();
                ResultService resultService = new ResultService("result.json", new Result(true, null, FormatTimer(timer), primeNumbers));
                resultService.WriteResultIntoFile();
            }
            catch (Exception e)
            {
                timer.Stop();
                ResultService resultService = new ResultService("result.json", new Result(false, e.Message, FormatTimer(timer), null));
                resultService.WriteResultIntoFile();
                return;
            }
        }
        static int[] GetPrimeNumbers(int lowerBound, int upperBound)
        {
            List<int> primeNumbers = new List<int>();
            for (int i = lowerBound; i < upperBound; i++)
            {
                bool IsPrime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        IsPrime = false;
                        break;
                    }
                }
                if (!IsPrime || i <= 1) continue;

                primeNumbers.Add(i);
            }
            return primeNumbers.ToArray();
        }
        static string FormatTimer(Stopwatch timer)
        {
            TimeSpan ts = timer.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            return elapsedTime;
        }
    }
}
