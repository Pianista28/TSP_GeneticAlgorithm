using System.Linq;

namespace TSP_GeneticAlgorithm.Extensions
{
    public static class IntArrayExtensions
    {
        internal static string Representation(this int[] array)
        {
            return array.Aggregate("", (current, item) => current + (item + " "));
        }
    }
}