using System.IO;
using System.Text.Json;

namespace Homework4.Task1
{
    public class ResultService
    {
        private readonly string _resultFileName;
        private Result _result = null;

        public ResultService (string resultFileName, Result obj)
        {
            _resultFileName = resultFileName;
            _result = obj;
        }

        public void WriteResultIntoFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(_result, options);
            File.WriteAllText(_resultFileName, json);
        }
    }
}
