using System;
using System.Linq;
using TSP_GeneticAlgorithm.Individuals;
using TSP_GeneticAlgorithm.Loggers;
using TSP_GeneticAlgorithm.Solvers;

namespace TSP_GeneticAlgorithm
{
    internal class GeneticAlgorithm : IGeneticAlgorithm
    {
        public const double Px = 0.7;
        public const double Pm = 0.1;
        public const int TournamentSize = 5;
        public const int PopulationSize = 30;
        public const int NumberOfGenerations = 150;
        public const string DataFileName = "fri26_d.txt";
        //public const string DataFileName = "five_d.txt";

        private readonly ITspSolver _tspSolver;
        private readonly ITspCsvLogger _csvLogger;

        public BestSolution BestSolution { get; set; }

        public GeneticAlgorithm(ITspSolver tspSolver, ITspCsvLogger csvLogger)
        {
            _tspSolver = tspSolver;
            _csvLogger = csvLogger;
        }

        public void LoadData()
        {
            _tspSolver.LoadDataFromFile(DataFileName);
        }

        public int[] FindSolution()
        {
            LoadData();
            _csvLogger.Init();
            int[] generationBestSolution = null;
            var population = _tspSolver.CreateInitialPopulation(PopulationSize).ToList();
            for (int i = 0; i < NumberOfGenerations; i++)
            {
                var selectedForCrossing = _tspSolver.SelectByTournament(population, TournamentSize).ToList();
                var crossedPopulation = _tspSolver.CrossPopulation(selectedForCrossing, Px).ToList();
                var mutatedPopulation = _tspSolver.MutatePopulation(crossedPopulation, Pm).ToList();
                population = mutatedPopulation.ToList();

                var bestFitness = _tspSolver.CalculateBestFitness(population);
                var worstFitness = _tspSolver.CalculateWorstFitness(population);
                var averageFitness = _tspSolver.CalculateAverageFitness(population);
                generationBestSolution = _tspSolver.GetBest(population);

                AssignBestSolution(i + 1, generationBestSolution, bestFitness);

                _csvLogger.Log("x", bestFitness, worstFitness, averageFitness);
            }
            _csvLogger.SaveOptions();
            return generationBestSolution;
        }

        private BestSolution AssignBestSolution(int generationNumber, int[] generationBestSolution,
            int generationBestFitness)
        {
            if (BestSolution == null || generationBestFitness <= BestSolution.Fitness)
            {
                BestSolution = new BestSolution()
                {
                    Fitness = generationBestFitness,
                    Genom = generationBestSolution,
                    GenerationNumber = generationNumber
                };
            }
            return BestSolution;
        }
    }
}