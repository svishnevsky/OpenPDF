using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.Content
{
    public sealed class PagePdfObjectContent : 
        DictionaryPdfObjectContent, 
        IEquatable<PagePdfObjectContent>
    {
        public PagePdfObjectContent(
            IDictionary<string, PdfObjectContent> value) : base(value)
        {
        }

        public long Rotate => (long?)this["Rotate"].Content ?? 0;

        public Box MediaBox => GetBox(this["MediaBox"]);

        public Box CropBox => GetBox(this["CropBox"]);

        public PdfObjectContent[] Contents => 
            (PdfObjectContent[])this["Contents"].Content;

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PagePdfObjectContent);
        }

        public bool Equals(PagePdfObjectContent other)
        {
            return other != null &&
                   base.Equals(other) &&
                   this.Rotate == other.Rotate &&
                   EqualityComparer<Box>.Default.Equals(
                       this.MediaBox, other.MediaBox) &&
                   EqualityComparer<Box>.Default.Equals(
                       this.CropBox, other.CropBox) &&
                   this.Contents.SequenceEqual(other.Contents);
        }

        public override int GetHashCode()
        {
            int hashCode = 1094502092;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Rotate.GetHashCode();
            hashCode = hashCode * -1521134295 + 
                EqualityComparer<Box>.Default.GetHashCode(this.MediaBox);
            hashCode = hashCode * -1521134295 + 
                EqualityComparer<Box>.Default.GetHashCode(this.CropBox);
            return hashCode;
        }

        private static Box GetBox(PdfObjectContent rawBox)
        {
            ArrayPdfObjectContent coords;
            if (rawBox == null ||
                ((coords = rawBox as ArrayPdfObjectContent) == null) ||
                coords.Value.Length != 4)
            {
                return null;
            }

            return new Box(
                new Point(
                    (float)coords.Value[0].Content,
                    (float)coords.Value[1].Content),
                new Point(
                    (float)coords.Value[2].Content,
                    (float)coords.Value[3].Content));
        }
    }
}
