using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    /// <summary>
    /// Deviation calculator for a list of numeric elements
    /// </summary>
    public class DeviationCalculator
    {
        /// <summary>
        /// Calculate the average deviation from a set of numbers
        /// </summary>
        /// <param name="suit"></param>
        /// <returns>Average where the numbers are from the set</returns>
        public double Calculate(IEnumerable<double> suit)
        {
            var averageCalculator = new AverageCalculator();

            double average = averageCalculator.Calculate(suit);
            var list = new List<double>();

            foreach (var number in suit)
            {
                list.Add(Math.Pow((number - average), 2));
            }

            return Math.Sqrt(averageCalculator.Calculate(list));
        }
    }
}
