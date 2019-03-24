using System;
using System.IO;
using System.Linq;

namespace TSP_GeneticAlgorithm.DataReaders
{
    internal class IntTspDataReader : ITspReader
    {
        public int[][] ReadData(string filePath)
        {
            int[][] matrixOfPoints = null;

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    var firstRow = GetRowOfPoints(sr);
                    matrixOfPoints = new int[firstRow.Length][];
                    matrixOfPoints[0] = firstRow;

                    for (var i = 1; i < firstRow.Length; i++)
                    {
                        matrixOfPoints[i] = GetRowOfPoints(sr);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return matrixOfPoints;
        }

        private int[] GetRowOfPoints(StreamReader sr)
        {
            var row = sr.ReadLine().Split(' ').ToList();
            row.RemoveAll(string.IsNullOrWhiteSpace);
            return row.Select(int.Parse).ToArray();
        }
    }
}