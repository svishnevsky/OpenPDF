using System;
using System.IO;
using OpenPDF.Readers;

namespace OpenPDF
{
    public class PdfReader : IDisposable
    {
        private PdfTrailer pdfTrailer;
        private ObjectReader objectReader;
        private readonly FileStreamReader reader;
        private bool disposed = false;

        public PdfReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            this.reader = new FileStreamReader(stream);
        }

        internal ObjectReader ObjectReader => this.objectReader 
            ?? (this.objectReader = new ObjectReader(this.reader));

        public string ReadVersion()
        {
            this.EnsureNotDisposed();
            return new VersionReader().Read(this.reader);
        }

        public PdfObject ReadObject(PdfCrossReference reference)
        {
            this.EnsureNotDisposed();
            return this.ObjectReader.Read(reference);
        }

        public PdfTrailer ReadTrailer()
        {
            this.EnsureNotDisposed();
            return this.pdfTrailer = new TrailerReader().Read(this.reader);
        }

        public PdfCrossReferenceTable ReadCrossReference()
        {
            this.EnsureNotDisposed();
            PdfTrailer trailer = this.pdfTrailer ?? this.ReadTrailer();
            return new CrossReferenceTableReader()
                .Read(this.reader, trailer.XrefSeek);
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
                this.reader.Dispose();
                this.disposed = true;
            }
        }

        private void EnsureNotDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(this.reader));
            }
        }
    }
}
