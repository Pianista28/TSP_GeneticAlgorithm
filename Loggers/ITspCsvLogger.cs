namespace TSP_GeneticAlgorithm.Loggers
{
    public interface ITspCsvLogger
    {
        void Init();
        void Log(string param, int best, int worst, int avg);
        void SaveOptions();
    }
}