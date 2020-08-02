using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    /// <summary>
    /// Average calculator for a list of numeric elements
    /// </summary>
    public class AverageCalculator
    {
        /// <summary>
        /// Calculate the average from a set of numbers
        /// </summary>
        /// <param name="suit">Set of numbers</param>
        /// <returns>Average from the given set</returns>
        public double Calculate(IEnumerable<double> suit)
        {
            double accumulator = 0;

            foreach (var number in suit)
            {
                accumulator += number;
            }

            return accumulator / suit.Count();
        }
    }
}
