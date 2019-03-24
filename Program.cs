using System;
using System.Reflection;
using Ninject;
using TSP_GeneticAlgorithm.DataReaders;
using TSP_GeneticAlgorithm.Extensions;

namespace TSP_GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var geneticAlgorithm = kernel.Get<IGeneticAlgorithm>();

            var solution = geneticAlgorithm.FindSolution();
            Console.WriteLine(solution.Representation());

            Console.ReadLine();
        }
    }
}
