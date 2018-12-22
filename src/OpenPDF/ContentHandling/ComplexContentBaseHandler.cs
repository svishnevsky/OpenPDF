using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.ContentHandling
{
    public abstract class ComplexContentBaseHandler
        : ObjectContentHandler
    {
        protected static readonly Dictionary<string, string> Brackets =
            new Dictionary<string, string>
            {
                { PdfTags.DictionaryStart, PdfTags.DictionaryEnd },
                { PdfTags.ArrayStart, PdfTags.ArrayEnd },
                { PdfTags.StringStart, PdfTags.StringEnd }
            };

        private readonly string startTag;
        private readonly string endTag;
        private readonly string itemSeparator;

        internal ObjectContentHandler PropHandler { get; set; }

        protected ComplexContentBaseHandler(
            ObjectContentHandler successor,
            ObjectContentHandler propHandler,
            string startTag,
            string endTag,
            string itemSeparator)
            : base(successor)
        {
            this.PropHandler = propHandler;
            this.startTag = startTag;
            this.endTag = endTag;
            this.itemSeparator = itemSeparator;
        }

        protected override bool IsContentSutable(string content)
        {
            return content.StartsWith(this.startTag) &&
                content.EndsWith(this.endTag);
        }

        protected string GetValue(string content)
        {
            int endIndex = Brackets.Keys.Any(content.StartsWith)
                                ? GetComplexObjectEnd(content)
                                : this.GetSimpleObjectEnd(content);
            return content.Substring(0, endIndex);
        }

        private int GetSimpleObjectEnd(string content)
        {
            int endIndex = content.IndexOf(this.itemSeparator, 1);
            if (endIndex == -1)
            {
                endIndex = content.Length;
            }

            return endIndex;
        }

        protected static int GetComplexObjectEnd(string content)
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

        protected string Unwrap(string content)
        {
            return content
                .Substring(
                    this.startTag.Length,
                    content.Length -
                        this.startTag.Length -
                        this.endTag.Length)
                .Trim();
        }
    }
}
