using System;
using System.IO;
using System.Threading.Tasks;

namespace OpenPDF
{
    public class PdfReader : IDisposable
    {
        private const string versionPrefix = "%PDF-";
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
            this.reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var versionLine = await this.reader.ReadLineAsync();
            if (!versionLine.StartsWith(versionPrefix))
            {
                throw new InvalidDataException(
                    ErrorMessages.InvalidStreamFormat);
            }

            return versionLine.Substring(versionPrefix.Length);
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
            if (disposing && !this.disposed)
            {
                this.reader.Dispose();
                this.disposed = true;
            }
        }
    }
}