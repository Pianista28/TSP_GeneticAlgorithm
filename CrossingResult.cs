using System;
using System.Collections.Generic;
using System.Text;
using TSP_GeneticAlgorithm.Individuals;

namespace TSP_GeneticAlgorithm
{
    internal struct CrossingResult
    {
        public Individual First { get; set; }
        public Individual Second { get; set; }
    }
}
