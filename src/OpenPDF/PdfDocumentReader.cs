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

        public async Task<PdfDocument> ReadDocument()
        {
            this.EnsureNotDisposed();
            string version = await this.reader.ReadVersion();
            return new PdfDocument(version);
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
