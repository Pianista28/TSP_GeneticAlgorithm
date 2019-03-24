using System;
using System.Collections.Generic;
using System.Text;

namespace TSP_GeneticAlgorithm
{
    internal class BestSolution
    {
        public int Fitness { get; set; }
        public int GenerationNumber { get; set; }
        public int[] Genom { get; set; }
    }
}
