﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenPDF.Content
{
    public sealed class PdfStream : IEquatable<PdfStream>
    {
        public PdfStream(
            IDictionary<string, PdfObjectContent> props,
            string content)
        {
            this.Props = props;
            this.Content = content;
        }

        public IDictionary<string, PdfObjectContent> Props { get; }

        public string Content { get; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PdfStream);
        }

        public bool Equals(PdfStream other)
        {
            return other != null &&
                   this.Props.SequenceEqual(other.Props) &&
                   this.Content == other.Content;
        }

        public override int GetHashCode()
        {
            int hashCode = -1997086341;
            hashCode = hashCode * -1521134295 + EqualityComparer<IDictionary<string, PdfObjectContent>>.Default.GetHashCode(this.Props);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Content);
            return hashCode;
        }
    }
}
