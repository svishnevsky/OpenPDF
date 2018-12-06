using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenPDF
{
    public class PdfReader : IDisposable
    {
        private static readonly int versionPrefixLength = "%PDF-".Length;
        private readonly StreamReader reader;
        private bool disposed = false;

        public PdfReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            this.reader = new StreamReader(stream);
        }

        public async Task<string> ReadVersion()
        {
            EnsureNotDisposed();
            var versionLine = await reader.ReadLineAsync();
            return versionLine.Substring(versionPrefixLength);
        }

        private void EnsureNotDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(this.reader));
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed && this.reader != null)
            {
                this.reader.Dispose();
                this.disposed = true;
            }
        }
    }
}