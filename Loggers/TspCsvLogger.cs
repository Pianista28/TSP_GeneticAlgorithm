using System.IO;
using System.Text;

namespace TSP_GeneticAlgorithm.Loggers
{
    class TspCsvLogger : ITspCsvLogger
    {
        private readonly string _filePath;
        private readonly StringBuilder _stringBuilder;

        public TspCsvLogger(string filePath)
        {
            _filePath = filePath;
            _stringBuilder = new StringBuilder();
        }

        public void Init()
        {
            _stringBuilder.AppendLine("Param,Best,Worst,Avg");
        }

        public void Log(string param, int best, int worst, int avg)
        {
            _stringBuilder.AppendLine($"{param},{best},{worst},{avg}");
        }

        public void SaveOptions()
        {
            File.AppendAllText(_filePath, _stringBuilder.ToString());

        }
    }
}
