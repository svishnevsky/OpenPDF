using System;
using System.Collections.Generic;
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
            return new PdfDocument(
                version,
                await this.GetInfo(trailer, referenceTable),
                await this.GetPages(trailer, referenceTable));
        }

        private async Task<List<PagePdfObjectContent>> GetPages(PdfTrailer trailer, PdfCrossReferenceTable referenceTable)
        {
            PdfCrossReference catalogReference = referenceTable[trailer.Root];
            PdfObject catalogObject =
                await this.reader.ReadObject(catalogReference);
            var catalog = (CatalogPdfObjectContent)catalogObject.Content;
            PdfCrossReference pagesReference =
                referenceTable[catalog.Pages];
            PdfObject pagesTree = await this.reader.ReadObject(
                pagesReference);
            var pages = new List<PagePdfObjectContent>();
            foreach (PdfReference pageReference in
                ((PagesPdfObjectContent)pagesTree.Content).Kids)
            {
                PdfObject page = await this.reader.ReadObject(
                    referenceTable[pageReference]);
                pages.Add((PagePdfObjectContent)page.Content);
            }

            return pages;
        }

        private async Task<PdfInfo> GetInfo(PdfTrailer trailer, PdfCrossReferenceTable referenceTable)
        {
            PdfCrossReference infoReference = referenceTable[trailer.Info];
            PdfObject infoObj = await this.reader.ReadObject(infoReference);
            var pdfInfo = new PdfInfo(infoObj.Content as DictionaryPdfObjectContent);
            return pdfInfo;
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
