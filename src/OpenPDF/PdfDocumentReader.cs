using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenPDF
{
    public class PdfDocumentReader : IDisposable
    {
        private readonly PdfReader reader;
        private bool disposed = false;

        public PdfDocumentReader(Stream stream)
        {
            this.reader = new PdfReader(stream);
        }

        public Task<PdfDocument> ReadDocument()
        {
            this.EnsureNotDisposed();
            return Task.FromResult(
                new PdfDocument(this.reader.ReadVersion()));
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
