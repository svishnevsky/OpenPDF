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

        private async Task<List<PagePdfObjectContent>> GetPages(
            PdfTrailer trailer,
            PdfCrossReferenceTable referenceTable)
        {
            PdfCrossReference catalogReference = referenceTable[trailer.Root];
            PdfObject catalogObject =
                await this.reader.ReadObject(catalogReference);
            var catalog = (CatalogPdfObjectContent)catalogObject.Content;
            PdfCrossReference pagesReference =
                referenceTable[catalog.Pages];
            PdfObject pagesTree = await this.reader.ReadObject(
                pagesReference);
            List<PagePdfObjectContent> pages = await this.ReadPages(
                referenceTable,
                ((PagesPdfObjectContent)pagesTree.Content).Kids);

            return pages;
        }

        private async Task<List<PagePdfObjectContent>> ReadPages(
            PdfCrossReferenceTable referenceTable,
            IEnumerable<PdfReference> pageReferences)
        {
            var pages = new List<PagePdfObjectContent>();
            foreach (PdfReference pageReference in pageReferences)
            {
                PagePdfObjectContent pageContent =
                    await this.ReadPage(referenceTable, pageReference);
                pages.Add(pageContent);
            }

            return pages;
        }

        private async Task<PagePdfObjectContent> ReadPage(
            PdfCrossReferenceTable referenceTable,
            PdfReference pageReference)
        {
            PdfObject page = await this.reader.ReadObject(
                                referenceTable[pageReference]);
            var pageContent = (PagePdfObjectContent)page.Content;
            await this.ResolveContentReferences(referenceTable, pageContent);

            return pageContent;
        }

        private async Task ResolveContentReferences(
            PdfCrossReferenceTable referenceTable,
            PagePdfObjectContent pageContent)
        {
            for (int i = 0; i < pageContent.Contents.Length; i++)
            {
                if (pageContent.Contents[i] is ReferencePdfObjectContent)
                {
                    var reference = pageContent.Contents[i] as ReferencePdfObjectContent;
                    PdfObject pdfObject = await this.reader.ReadObject(
                        referenceTable[reference.Value]);
                    pageContent.Contents[i] = pdfObject.Content;
                }
            }
        }

        private async Task<PdfInfo> GetInfo(PdfTrailer trailer, PdfCrossReferenceTable referenceTable)
        {
            PdfCrossReference infoReference = referenceTable[trailer.Info];
            PdfObject infoObj = await this.reader.ReadObject(infoReference);
            var pdfInfo = new PdfInfo(infoObj.Content as DictionaryPdfObjectContent);
            return pdfInfo;
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
