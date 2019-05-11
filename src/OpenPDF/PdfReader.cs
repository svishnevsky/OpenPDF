using System;
using System.IO;
using System.Threading.Tasks;
using OpenPDF.Content.Handling;
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

        public async Task<string> ReadVersion()
        {
            this.EnsureNotDisposed();
            return await new VersionReader().Read(this.fileReader);
        }

        public async Task<PdfObject> ReadObject(PdfCrossReference reference)
        {
            this.EnsureNotDisposed();
            return await this.ObjectReader.Read(reference);
        }

        public async Task<PdfTrailer> ReadTrailer()
        {
            this.EnsureNotDisposed();
            return this.pdfTrailer = await new TrailerReader()
                .Read(this.fileReader);
        }

        public async Task<PdfCrossReferenceTable> ReadCrossReference()
        {
            this.EnsureNotDisposed();
            PdfTrailer trailer = this.pdfTrailer ?? 
                (this.pdfTrailer = await this.ReadTrailer());
            return await new CrossReferenceTableReader()
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
