using System;
using System.IO;
using System.Threading.Tasks;
using OpenPDF.Content;

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
            PdfTrailer trailer = await this.reader.ReadTrailer();
            PdfCrossReferenceTable referenceTable = 
                await this.reader.ReadCrossReference();
            PdfCrossReference infoReference = referenceTable[trailer.Info];
            PdfObject infoObj = await this.reader.ReadObject(infoReference);
            return new PdfDocument(
                version, 
                new PdfInfo(infoObj.Content as DictionaryPdfObjectContent));
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
