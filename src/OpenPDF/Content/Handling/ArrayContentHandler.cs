using System.Collections.Generic;

namespace OpenPDF.Content.Handling
{
    public class ArrayContentHandler : ComplexContentBaseHandler
    {
        public ArrayContentHandler(
            IObjectContentHandler successor,
            IObjectContentHandler propHandler)
            : base(
                  successor,
                  propHandler,
                  PdfTags.ArrayStart,
                  PdfTags.ArrayEnd,
                  " ")
        {
        }

        protected override PdfObjectContent Parse(string content)
        {
            content = this.Unwrap(content);
            var items = new List<PdfObjectContent>();
            while (!string.IsNullOrEmpty(content))
            {
                string value = this.GetValue(content);
                content = content.Substring(value.Length).TrimStart();
                items.Add(this.PropHandler.Handle(value.Trim()));
            }

            return new ArrayPdfObjectContent(items.ToArray());
        }

        protected override string GetValue(string content)
        {
            int rIndex = content.IndexOf(" R");
            if (rIndex != -1)
            {
                string referenceCandidate = 
                    content.Substring(0, rIndex + 2);
                if (ReferenceContentHandler.ReferenceFormat.IsMatch(
                    referenceCandidate))
                {
                    return referenceCandidate;
                }
            }

            return base.GetValue(content);
        }
    }
}
