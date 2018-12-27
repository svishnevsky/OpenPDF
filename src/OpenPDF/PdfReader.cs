using System;
using System.IO;
using OpenPDF.ContentHandling;
using OpenPDF.Readers;

namespace OpenPDF
{
    public class PdfReader : IDisposable
    {
        private PdfTrailer pdfTrailer;
        private ObjectReader objectReader;
        private readonly FileStreamReader fileReader;
        private bool disposed = false;

        public PdfReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            this.fileReader = new FileStreamReader(stream);
        }

        internal ObjectReader ObjectReader => this.objectReader 
            ?? (this.objectReader = new ObjectReader(
                this.fileReader,
                new DefaultContentHandler()));

        public string ReadVersion()
        {
            this.EnsureNotDisposed();
            return new VersionReader().Read(this.fileReader);
        }

        public PdfObject ReadObject(PdfCrossReference reference)
        {
            this.EnsureNotDisposed();
            return this.ObjectReader.Read(reference);
        }

        public PdfTrailer ReadTrailer()
        {
            this.EnsureNotDisposed();
            return this.pdfTrailer = new TrailerReader().Read(this.fileReader);
        }

        public PdfCrossReferenceTable ReadCrossReference()
        {
            this.EnsureNotDisposed();
            PdfTrailer trailer = this.pdfTrailer ?? this.ReadTrailer();
            return new CrossReferenceTableReader()
                .Read(this.fileReader, trailer.XrefSeek);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.fileReader.Dispose();
                this.disposed = true;
            }
        }

        private void EnsureNotDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(this.fileReader));
            }
        }
    }
}
