using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tester.Csv
{
    [TestClass]
    public class CsvToListTest
    {
        private bool Compare(IEnumerable<IEnumerable<double>> expected, IEnumerable<IEnumerable<double>> result)
        {
            for (int row = 0; row < expected.Count(); row++)
            {
                if (!expected.ElementAt(row).SequenceEqual(result.ElementAt(row)))
                {
                    return false;
                }
            }

            return expected.Count() == result.Count();
        }

        [TestMethod]
        public void FromStringSimple()
        {
            string input = @"
                6; 3; 5; 7
                2; 4; 6; 9
                1; 5; 7; 2
            ";

            var expected = new List<List<double>>
            {
                new List<double> { 6, 3, 5, 7 },
                new List<double> { 2, 4, 6, 9 },
                new List<double> { 1, 5, 7, 2 }
            };

            var converter = new CsvToList();
            var result = converter.FromString(input);

            Assert.IsTrue(Compare(expected, result));
        }

        [TestMethod]
        public void FromStringLarge()
        {
            string input = @"
                2.4; 45; 10; 201; 4.566; 102; 400.34; 70.2; 122; 6044
                153; 56; 455; 22; 56.65; 1.2; 0.34; 890.2; 1345; 1222
                3434; 2323; 454; 2.22; 46.5; 2.3; 2122; 7; 12.0; 1233
                56.6; 1.02; 0.001; 2; 21; 1.2; 0.3; 0.03; 0.22; 3.141
                4.3; 156; 5; 23; 5.65; 1.0; 0.034; 8922.2; 1345; 1332
                2.4; 45; 10; 201; 4.566; 102; 400.34; 70.2; 122; 6044
                56.6; 1.02; 0.001; 2; 21; 1.2; 0.3; 0.03; 0.22; 3.141
                3434; 2323; 454; 2.22; 2.2; 1345; 1332; 7; 12.0; 0.22
            ";

            var expected = new List<List<double>>
            {
                new List<double> { 2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044 },
                new List<double> { 153, 56, 455, 22, 56.65, 1.2, 0.34, 890.2, 1345, 1222 },
                new List<double> { 3434, 2323, 454, 2.22, 46.5, 2.3, 2122, 7, 12.0, 1233 },
                new List<double> { 56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141 },
                new List<double> { 4.3, 156, 5, 23, 5.65, 1.0, 0.034, 8922.2, 1345, 1332 },
                new List<double> { 2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044 },
                new List<double> { 56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141 },
                new List<double> { 3434, 2323, 454, 2.22, 2.2, 1345, 1332, 7, 12.0, 0.22 }
            };

            var converter = new CsvToList();
            var result = converter.FromString(input);

            Assert.IsTrue(Compare(expected, result));
        }

        [TestMethod]
        public void UsingBadSeparator()
        {
            string input = @"
                6, 3, 5, 7
                2, 4, 6, 9
                1, 5, 7, 2
            ";

            var expected = new List<List<double>>
            {
                new List<double> { 6, 3, 5, 7 },
                new List<double> { 2, 4, 6, 9 },
                new List<double> { 1, 5, 7, 2 }
            };

            var converter = new CsvToList();
            var result = converter.FromString(input);

            Assert.IsFalse(Compare(expected, result));
        }

        [TestMethod]
        public void UsingAnotherSeparatorSimple()
        {
            char separator = ',';

            string input = @"
                6, 3, 5, 7
                2, 4, 6, 9
                1, 5, 7, 2
            ";

            var expected = new List<List<double>>
            {
                new List<double> { 6, 3, 5, 7 },
                new List<double> { 2, 4, 6, 9 },
                new List<double> { 1, 5, 7, 2 }
            };

            var converter = new CsvToList(separator);
            var result = converter.FromString(input);

            Assert.IsTrue(Compare(expected, result));
        }

        [TestMethod]
        public void UsingAnotherSeparatorLarge()
        {
            char separator = ',';

            string input = @"
                2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044
                153, 56, 455, 22, 56.65, 1.2, 0.34, 890.2, 1345, 1222
                3434, 2323, 454, 2.22, 46.5, 2.3, 2122, 7, 12.0, 1233
                56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141
                4.3, 156, 5, 23, 5.65, 1.0, 0.034, 8922.2, 1345, 1332
                2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044
                56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141
                3434, 2323, 454, 2.22, 2.2, 1345, 1332, 7, 12.0, 0.22
            ";

            var expected = new List<List<double>>
            {
                new List<double> { 2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044 },
                new List<double> { 153, 56, 455, 22, 56.65, 1.2, 0.34, 890.2, 1345, 1222 },
                new List<double> { 3434, 2323, 454, 2.22, 46.5, 2.3, 2122, 7, 12.0, 1233 },
                new List<double> { 56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141 },
                new List<double> { 4.3, 156, 5, 23, 5.65, 1.0, 0.034, 8922.2, 1345, 1332 },
                new List<double> { 2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044 },
                new List<double> { 56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141 },
                new List<double> { 3434, 2323, 454, 2.22, 2.2, 1345, 1332, 7, 12.0, 0.22 }
            };

            var converter = new CsvToList(separator);
            var result = converter.FromString(input);

            Assert.IsTrue(Compare(expected, result));
        }

        [TestMethod]
        public async Task FromStreamSimple()
        {
            string input = @"
                6; 3; 5; 7
                2; 4; 6; 9
                1; 5; 7; 2
            ";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

            var expected = new List<List<double>>
            {
                new List<double> { 6, 3, 5, 7 },
                new List<double> { 2, 4, 6, 9 },
                new List<double> { 1, 5, 7, 2 }
            };

            var converter = new CsvToList();
            var result = await converter.FromStreamAsync(stream);

            Assert.IsTrue(Compare(expected, result));
        }

        [TestMethod]
        public async Task FromStreamLarge()
        {
            string input = @"
                2.4; 45; 10; 201; 4.566; 102; 400.34; 70.2; 122; 6044
                153; 56; 455; 22; 56.65; 1.2; 0.34; 890.2; 1345; 1222
                3434; 2323; 454; 2.22; 46.5; 2.3; 2122; 7; 12.0; 1233
                56.6; 1.02; 0.001; 2; 21; 1.2; 0.3; 0.03; 0.22; 3.141
                4.3; 156; 5; 23; 5.65; 1.0; 0.034; 8922.2; 1345; 1332
                2.4; 45; 10; 201; 4.566; 102; 400.34; 70.2; 122; 6044
                56.6; 1.02; 0.001; 2; 21; 1.2; 0.3; 0.03; 0.22; 3.141
                3434; 2323; 454; 2.22; 2.2; 1345; 1332; 7; 12.0; 0.22
            ";

            var expected = new List<List<double>>
            {
                new List<double> { 2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044 },
                new List<double> { 153, 56, 455, 22, 56.65, 1.2, 0.34, 890.2, 1345, 1222 },
                new List<double> { 3434, 2323, 454, 2.22, 46.5, 2.3, 2122, 7, 12.0, 1233 },
                new List<double> { 56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141 },
                new List<double> { 4.3, 156, 5, 23, 5.65, 1.0, 0.034, 8922.2, 1345, 1332 },
                new List<double> { 2.4, 45, 10, 201, 4.566, 102, 400.34, 70.2, 122, 6044 },
                new List<double> { 56.6, 1.02, 0.001, 2, 21, 1.2, 0.3, 0.03, 0.22, 3.141 },
                new List<double> { 3434, 2323, 454, 2.22, 2.2, 1345, 1332, 7, 12.0, 0.22 }
            };

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

            var converter = new CsvToList();
            var result = await converter.FromStreamAsync(stream);

            Assert.IsTrue(Compare(expected, result));
        }
    }
}
