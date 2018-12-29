using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.Content.Handling
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
            int endIndex;
            if (Brackets.Keys.Any(content.StartsWith))
            {
                KeyValuePair<string, string> brackets = Brackets
                    .Single(x => content.StartsWith(x.Key));
                endIndex = GetComplexObjectEnd(
                    content, brackets.Key, brackets.Value);
            }
            else
            {
                endIndex = this.GetSimpleObjectEnd(content);
            }

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

        protected static int GetComplexObjectEnd(
            string content,
            string openBracket,
            string closeBracket)
        {
            int openned = 1;
            int nextOpenIndex = 0;
            int nextCloseIndex = 0;
            int dept = 0;
            do
            {
                if (CloseAfterOpen(nextOpenIndex, nextCloseIndex))
                {
                    nextOpenIndex = content.IndexOf(
                        openBracket, nextOpenIndex + openBracket.Length);
                    if (nextOpenIndex != -1)
                    {
                        IncrementOpenned(
                            ref openned, 
                            nextOpenIndex, 
                            nextCloseIndex, 
                            ref dept);
                    }
                }

                if (OpenAfterClose(nextOpenIndex, nextCloseIndex))
                {
                    nextCloseIndex = content.IndexOf(
                        closeBracket,
                        nextCloseIndex + closeBracket.Length);
                    openned--;
                    if (ShouldApplyDept(
                        nextOpenIndex, nextCloseIndex, dept))
                    {
                        dept--;
                        openned++;
                    }
                }
            } while (openned > 0);

            return nextCloseIndex + closeBracket.Length;
        }

        private static void IncrementOpenned(
            ref int openned, 
            int nextOpenIndex, 
            int nextCloseIndex, 
            ref int dept)
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

        private static bool ShouldApplyDept(
            int nextOpenIndex, int nextCloseIndex, int dept)
        {
            return nextCloseIndex >= nextOpenIndex && dept > 0;
        }

        private static bool OpenAfterClose(
            int nextOpenIndex, int nextCloseIndex)
        {
            return nextOpenIndex > nextCloseIndex ||
                                nextOpenIndex == -1;
        }

        private static bool CloseAfterOpen(
            int nextOpenIndex, int nextCloseIndex)
        {
            return nextCloseIndex >= nextOpenIndex &&
                                    nextOpenIndex != -1;
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
