using System.Collections.Generic;
using TSP_GeneticAlgorithm.Individuals;

namespace TSP_GeneticAlgorithm.Solvers
{
    internal interface ITspSolver
    {
        IEnumerable<Individual> CreateInitialPopulation(int populationSize);
        IEnumerable<Individual> SelectByTournament(List<Individual> population, int tournamentSize);
        IEnumerable<Individual> CrossPopulation(List<Individual> population, double crossProbability);
        IEnumerable<Individual> MutatePopulation(IEnumerable<Individual> population, double mutationProbability);
        int CalculatePopulationFitness(IEnumerable<Individual> population);
        int CalculateBestFitness(IEnumerable<Individual> population);
        int CalculateWorstFitness(IEnumerable<Individual> population);
        int CalculateAverageFitness(IEnumerable<Individual> population);
        int CalculateFitness(int[] genom);
        void LoadDataFromFile(string dataFilePath);
        int[] GetBest(IEnumerable<Individual> population);
    }
}