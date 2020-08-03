using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public sealed class CsvToList
    {
        private readonly char separator;

        public CsvToList(char separator)
        {
            this.separator = separator;
        }

        public CsvToList()
        {
            separator = ';';
        }

        private double StringToNumber(string text)
        {
            var numberStyle = NumberStyles.Integer | NumberStyles.Float;
            bool could = double.TryParse(text, numberStyle, CultureInfo.InvariantCulture, out double value);

            return could ? value : 0;
        }

        public IEnumerable<IEnumerable<double>> FromString(string text)
        {
            var lines = text.Trim().Split('\n');
            var columns = lines[0].Count(t => t == separator) + 1;

            var table = new List<List<double>>();
            
            for (int i = 0; i < lines.Length; i++)
            {
                // read by rows

                var content = lines[i].Split(separator);
                var row = new List<double>();

                for (int j = 0; j < content.Length; j++)
                {
                    // read by columns

                    double value = StringToNumber(content[j]);
                    row.Add(value);
                }

                table.Add(row);
            }

            return table;
        }

        public async Task<IEnumerable<IEnumerable<double>>> FromStreamAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            string text = await reader.ReadToEndAsync();

            return FromString(text);
        }
    }
}
