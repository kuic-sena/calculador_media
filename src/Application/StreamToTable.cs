using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    using Table = IEnumerable<IEnumerable<double>>;

    public sealed class StreamToTable
    {
        public async Task<Table> Convert(Stream stream)
        {
            using var reader = new StreamReader(stream);
            string text = await reader.ReadToEndAsync();

            var lines = text.Split('\n');
            var columns = lines[0].Count(t => t == ';') + 1;

            var table = new List<List<double>>();

            for (int i = 0; i < lines.Length; i++)
            {
                // read by rows

                var content = lines[i].Split(';');
                var row = new List<double>();

                for (int j = 0; j < content.Length; j++)
                {
                    // read by columns

                    bool wasParsed = double.TryParse(content[j], out double value);
                    row.Add(wasParsed ? value : 0);
                }

                table.Add(row);
            }

            return table;
        }
    }
}
