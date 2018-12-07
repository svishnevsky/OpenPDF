using System.Collections.Generic;
using System.Linq;

namespace OpenPDF
{
    public class PdfObject
    {
        public PdfObject(
            int number,
            int generation,
            IEnumerable<string> content)
        {
            this.Number = number;
            this.Generation = generation;
            this.Content = content.ToArray();
        }

        public int Number { get; }
        public int Generation { get; }
        public string[] Content { get; }
    }
}