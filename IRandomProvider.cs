using System;
using System.Collections.Generic;
using System.Text;

namespace TSP_GeneticAlgorithm
{
    public interface IRandomProvider
    {
        int GetRandomValue(int range);
        double GetRandomProbability();
    }
}
