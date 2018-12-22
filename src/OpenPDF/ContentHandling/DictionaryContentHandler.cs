using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.ContentHandling
{
    public class DictionaryContentHandler : ObjectContentHandler
    {
        private static readonly Dictionary<string, string> Brackets =
            new Dictionary<string, string>
            {
                { PdfTags.DictionaryStart, PdfTags.DictionaryEnd },
                { PdfTags.ArrayStart, PdfTags.ArrayEnd },
                { PdfTags.StringStart, PdfTags.StringEnd }
            };

        internal ObjectContentHandler PropHandler { get; set; }

        public DictionaryContentHandler(
            ObjectContentHandler successor,
            ObjectContentHandler propHandler)
            : base(successor)
        {
            this.PropHandler = propHandler;
        }

        protected override bool IsContentSutable(string content)
        {
            return content.StartsWith(PdfTags.DictionaryStart) &&
                content.EndsWith(PdfTags.DictionaryEnd);
        }

        protected override PdfObjectContent Parse(string content)
        {
            content = Unwrap(content);
            var props = new Dictionary<string, PdfObjectContent>();
            while (!string.IsNullOrEmpty(content))
            {
                string prop = content.Substring(1, content.IndexOf(' ') - 1);
                content = content.Substring(prop.Length + 2);
                int endIndex = Brackets.Keys.Any(content.StartsWith)
                    ? GetComplexObjectEnd(content)
                    : GetSimpleObjectEnd(content);
                string value = content.Substring(0, endIndex);
                content = content.Substring(value.Length).TrimStart();
                props.Add(prop, this.PropHandler.Handle(value.Trim()));
            }

            return new DictionaryPdfObjectContent(props);
        }

        private static int GetComplexObjectEnd(string content)
        {
            KeyValuePair<string, string> brackets = Brackets
                .Single(x => content.StartsWith(x.Key));
            int openned = 1;
            int nextOpenIndex = 0;
            int nextCloseIndex = 0;
            int dept = 0;
            do
            {
                if (nextCloseIndex >= nextOpenIndex &&
                        nextOpenIndex != -1)
                {
                    nextOpenIndex = content.IndexOf(
                        brackets.Key, nextOpenIndex + brackets.Key.Length);
                    if (nextOpenIndex != -1)
                    {
                        if (nextCloseIndex >= nextOpenIndex)
                        {
                            openned++;
                        }
                        else
                        {
                            dept++;
                        }
                    }
                }

                if (nextOpenIndex > nextCloseIndex ||
                    nextOpenIndex == -1)
                {
                    nextCloseIndex = content.IndexOf(
                        brackets.Value,
                        nextCloseIndex + brackets.Value.Length);
                    openned--;
                    if (nextCloseIndex >= nextOpenIndex && dept > 0)
                    {
                        dept--;
                        openned++;
                    }
                }
            } while (openned > 0);

            return nextCloseIndex + brackets.Value.Length;
        }

        private static int GetSimpleObjectEnd(string content)
        {
            int endIndex = content.IndexOf("/", 1);
            if (endIndex == -1)
            {
                endIndex = content.Length;
            }

            return endIndex;
        }

        private static string Unwrap(string content)
        {
            return content
                .Substring(
                    PdfTags.DictionaryStart.Length,
                    content.Length -
                        PdfTags.DictionaryStart.Length -
                        PdfTags.DictionaryEnd.Length)
                .Trim();
        }
    }
}
