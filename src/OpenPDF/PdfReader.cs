using System;
using System.Collections.Generic;
using System.IO;
using OpenPDF.Readers;

namespace OpenPDF
{
    public class PdfReader : IDisposable
    {
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

        public string ReadVersion()
        {
            this.EnsureNotDisposed();
            return new VersionReader().Read(this.reader);
        }

        public IEnumerable<PdfObject> ReadObjects()
        {
            this.EnsureNotDisposed();
            return new ObjectReader().Read(this.reader);
        }

        public PdfTrailer ReadTrailer()
        {
            this.EnsureNotDisposed();
            return new TrailerReader().Read(this.reader);
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
