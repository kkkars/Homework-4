namespace Homework4.Task1
{
    public class Result
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Duration { get; set; }
        public int[] Primes { get; set; }

        public Result(bool success, string error, string duration, int[] primes)
        {
            Success = success;
            Error = error;
            Duration = duration;
            Primes = primes;
        }
    }
}
