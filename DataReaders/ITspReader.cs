namespace TSP_GeneticAlgorithm.DataReaders
{
    public interface ITspReader
    {
        int[][] ReadData(string dataFilePath);
    }
}