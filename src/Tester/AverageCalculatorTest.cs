using System.Collections.Generic;
using Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester
{
    [TestClass]
    public class AverageCalculatorTest
    {
        [TestMethod]
        public void SimpleSuitTest()
        {
            var averageCalculator = new AverageCalculator();
            var suit = new List<double>{ 6, 2, 3, 1 };

            double expectedValue = 3;
            Assert.AreEqual(expectedValue, averageCalculator.Calculate(suit));
        }

        [TestMethod]
        public void LargeSuitTest()
        {
            var averageCalculator = new AverageCalculator();
            var suit = new List<double> { 3, 5, 6, 7, 9, 12, 23, 86, 1, 23, 2, 5, 6, 9, 11, 21, 42, 24, 80, 93 };

            double expectedValue = 23.4;
            Assert.AreEqual(expectedValue, averageCalculator.Calculate(suit));
        }
    }
}
