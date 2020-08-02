using System;
using System.Collections.Generic;
using Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester
{
    [TestClass]
    public class DeviationCalculatorTest
    {
        [TestMethod]
        public void SimpleSuitTest()
        {
            var deviationCalculator = new DeviationCalculator();
            var suit = new List<double> { 6, 2, 3, 1 };

            double expectedValue = 1.8708;
            Assert.AreEqual(expectedValue, Math.Round(deviationCalculator.Calculate(suit), 4));
        }

        [TestMethod]
        public void LargeSuitTest()
        {
            var deviationCalculator = new DeviationCalculator();
            var suit = new List<double> { 3, 5, 6, 7, 9, 12, 23, 86, 1, 23, 2, 5, 6, 9, 11, 21, 42, 24, 80, 93 };

            double expectedValue = 28.2354;
            Assert.AreEqual(expectedValue, Math.Round(deviationCalculator.Calculate(suit), 4));
        }
    }
}
