using System;
using System.Drawing;

namespace TSP_GeneticAlgorithm.Individuals
{
    internal interface Individual
    {
        int[] Genom { get; set; }
        Func<int[], int> _fitnessFunc { get; }
        int CalculateAdaptation();
        Individual Mutation();
        void CreateRandomGenom();
        CrossingResult Cross(Individual other);
    }
}