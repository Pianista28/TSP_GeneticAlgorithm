using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Ninject.Modules;
using TSP_GeneticAlgorithm.DataReaders;
using TSP_GeneticAlgorithm.Individuals;
using TSP_GeneticAlgorithm.Loggers;
using TSP_GeneticAlgorithm.Solvers;

namespace TSP_GeneticAlgorithm.DI
{
    public class TspModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITspReader>().To<IntTspDataReader>();
            Bind<ITspCsvLogger>().ToMethod(x => new TspCsvLogger("C:\\Users\\228108\\source\\repos\\SI1_GeneticAlgorithm\\SI1_GeneticAlgorithm\\bin\\Debug\\netcoreapp2.0\\Test.csv"));
            Bind<IRandomProvider>().To<RandomProvider>();
            Bind<IGeneticAlgorithm>().To<GeneticAlgorithm>();
            Bind<ITspSolver>().To<TspSolver>();
            Bind<Individual>().To<TspIndividual>();
        }
    }
}
