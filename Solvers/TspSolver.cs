using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MoreLinq;
using TSP_GeneticAlgorithm.DataReaders;
using TSP_GeneticAlgorithm.Individuals;

[assembly:InternalsVisibleTo("TspTests")]

namespace TSP_GeneticAlgorithm.Solvers
{
    internal class TspSolver : ITspSolver
    {
        public int[][] Data { get; private set; } 
        private readonly ITspReader _dataReader;
        private readonly IRandomProvider _randomProvider;

        public TspSolver(ITspReader dataReader, IRandomProvider randomProvider)
        {
            _dataReader = dataReader;
            _randomProvider = randomProvider;
        }

        public int FitnessFunction(int[] genom)
        {
            var fitness = 0;

            for (int i = 0; i < genom.Length - 1; i++)
            {
                fitness += Data[genom[i] - 1][genom[i + 1] - 1];
            }

            fitness += Data[genom[genom.Length - 1] - 1][genom[0] - 1];

            return fitness;
        }

        public IEnumerable<Individual> CreateInitialPopulation(int populationSize)
        {
            var population = new List<TspIndividual>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new TspIndividual(FitnessFunction, GetGrowingSequenceGenom(), _randomProvider));
                population[i].CreateRandomGenom();
            }

            return population;
        }

        internal int[] GetGrowingSequenceGenom()
        {
            var sequence = new int[Data.Length];
            for (int i = 1; i <= Data.Length; i++)
            {
                sequence[i - 1] = i;
            }

            return sequence;
        }

        public IEnumerable<Individual> SelectByTournament(List<Individual> population, int tournamentSize)
        {
            var selectedPopulation = new List<Individual>();
            foreach (var individual in population)
            {
                selectedPopulation.Add(DoTournament(population, tournamentSize));
            }

            return selectedPopulation;
        }

        private Individual DoTournament(IReadOnlyList<Individual> population, int tournamentSize)
        {
            var selectedForTournament = new List<Individual>();
            while (selectedForTournament.Count < tournamentSize)
            {
                selectedForTournament.Add(population[_randomProvider.GetRandomValue(population.Count())]);
            }

            var minDistance = selectedForTournament.Min(x => x.CalculateAdaptation());

            return selectedForTournament.First(y => y.CalculateAdaptation() == minDistance);
        }

        public IEnumerable<Individual> CrossPopulation(List<Individual> population, double crossProbability)
        {
            var crossedPopulation = new List<Individual>();
            for (int i = 0; i < population.Count; i += 2)
            {
                if (_randomProvider.GetRandomProbability() < crossProbability)
                {
                    var crossedResult = population[i].Cross(population[i + 1]);
                    crossedPopulation.AddRange(new List<Individual> { crossedResult.First, crossedResult.Second });
                }
                else
                {
                    crossedPopulation.AddRange(new List<Individual>{population[i], population[i+1]});
                }
            }

            return crossedPopulation;
        }


        public IEnumerable<Individual> MutatePopulation(IEnumerable<Individual> population, double mutationProbability)
        {
            return population.Select(x => MutateIndividual(x, mutationProbability));
        }

        private Individual MutateIndividual(Individual individual, double mutationProbability)
        {
            return _randomProvider.GetRandomProbability() < mutationProbability 
                ? individual.Mutation() 
                : individual;
        }

        public int CalculatePopulationFitness(IEnumerable<Individual> population)
        {
            return population.Sum(x => x.CalculateAdaptation());
        }

        public int CalculateBestFitness(IEnumerable<Individual> population)
        {
            return population.OrderBy(x => x.CalculateAdaptation()).First().CalculateAdaptation();
        }

        public int CalculateWorstFitness(IEnumerable<Individual> population)
        {
            return population.OrderByDescending(x => x.CalculateAdaptation()).First().CalculateAdaptation();
        }

        public int CalculateAverageFitness(IEnumerable<Individual> population)
        {
            return (int) population.Average(x => x.CalculateAdaptation());
        }

        public int CalculateFitness(int[] genom)
        {
            return FitnessFunction(genom);
        }

        public void LoadDataFromFile(string dataFilePath)
        {
            Data = _dataReader.ReadData(dataFilePath);
        }

        public int[] GetBest(IEnumerable<Individual> population)
        {
            return population.OrderBy(x => x.CalculateAdaptation()).First().Genom;
        }

        public int[] GetBest()
        {
            throw new NotImplementedException();
        }
    }
}
