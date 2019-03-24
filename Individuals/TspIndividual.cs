using System;
using System.Linq;

namespace TSP_GeneticAlgorithm.Individuals
{
    internal class TspIndividual : Individual
    {
        public int[] Genom { get; set; }
        public Func<int[], int> _fitnessFunc { get; }

        private readonly IRandomProvider _randomProvider;
        public TspIndividual(Func<int[], int> fitnessFunc, int[] genom, IRandomProvider randomProvider)
        {
            _randomProvider = randomProvider;
            _fitnessFunc = fitnessFunc;
            Genom = genom;
        }

        public Individual Mutation()
        {
            var indA = _randomProvider.GetRandomValue(Genom.Length);
            var indB = _randomProvider.GetRandomValue(Genom.Length);
            var a = Genom[indA];
            Genom[indA] = Genom[indB];
            Genom[indB] = a;

            return this;
        }

        public void CreateRandomGenom()
        {
            Genom = Randomize(GetGrowingSequenceGenom());
        }

        public CrossingResult Cross(Individual other)
        {
            var first = this.Genom.Take(Genom.Length / 2).ToList();
            for (int i = 0; i < Genom.Length - (Genom.Length / 2); i++)
            {

                first.Add(other.Genom.First(x => !first.Contains(x)));
            }

            var second = other.Genom.Take(other.Genom.Length / 2).ToList();
            for (int i = 0; i < other.Genom.Length - (other.Genom.Length / 2); i++)
            {

                second.Add(Genom.First(x => !second.Contains(x)));
            }

            return new CrossingResult()
            {
                First = new TspIndividual(_fitnessFunc, first.ToArray(), _randomProvider),
                Second = new TspIndividual(_fitnessFunc, second.ToArray(), _randomProvider)
            };
        }

        private int[] GetGrowingSequenceGenom()
        {
            var sequence = new int[Genom.Length];
            for (var i = 0; i < Genom.Length; i++)
            {
                sequence[i] = i;
            }

            return sequence;
        }
        private int[] Randomize(int[] genom)
        {
            return Genom.OrderBy(x => _randomProvider.GetRandomValue(Int32.MaxValue)).ToArray();
        }

        public int CalculateAdaptation()
        {
            var fitness = _fitnessFunc(Genom);
            return fitness;

        }

        public override string ToString()
        {
            var toReturn = "";
            foreach (var gen in Genom)
            {
                toReturn += gen + " ";
            }

            return toReturn;
        }
    }
}
