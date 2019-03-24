using System;
using System.Collections.Generic;
using System.Text;

namespace TSP_GeneticAlgorithm
{
    internal class RandomProvider : IRandomProvider
    {
        private readonly Random _rand;

        public RandomProvider()
        {
            _rand = new Random();
        }

        public int GetRandomValue(int range)
        {
            return _rand.Next(range);
        }

        public double GetRandomProbability()
        {
            return _rand.NextDouble();
        }
    }
}
