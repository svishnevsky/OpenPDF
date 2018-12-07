using System;
using System.Collections.Generic;
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
            this.EnsureNotDisposed();
            this.SeekToStart();
            var versionLine = await this.reader.ReadLineAsync();
            if (!versionLine.StartsWith(versionPrefix))
            {
                throw new InvalidDataException(
                    ErrorMessages.InvalidStreamFormat);
            }

            return versionLine.Substring(versionPrefix.Length);
        }

        public IEnumerable<PdfObject> ReadObjects()
        {
            this.EnsureNotDisposed();
            this.SeekToStart();
            string currentLine;
            while ((currentLine = this.FindNextObjectDefinition())
                    != null)
            {
                var objectDefinition = currentLine.Split(' ');
                var objectContent = new List<string>();
                while((currentLine = this.reader.ReadLine())
                    != PdfTags.EndObj)
                {
                    objectContent.Add(currentLine);
                }

                yield return new PdfObject(
                    int.Parse(objectDefinition[0]),
                    int.Parse(objectDefinition[1]),
                    objectContent);
            }

            yield break;
        }

        private string FindNextObjectDefinition()
        {
            var previousPosition = this.reader.BaseStream.Position;
            string currentLine = this.reader.ReadLine();
            while (!this.reader.EndOfStream)
            {
                if (IsObjectDefinition(currentLine))
                {
                    return currentLine;
                }

                if (PdfTags.Xref.Equals(currentLine) ||
                    PdfTags.Trailer.Equals(currentLine))
                {
                    this.reader.BaseStream.Seek(
                        previousPosition, SeekOrigin.Begin);
                    return null;
                }

                previousPosition = this.reader.BaseStream.Position;
                currentLine = this.reader.ReadLine();
            }

            return null;
        }

        private static bool IsObjectDefinition(string currentLine)
        {
            return !currentLine.StartsWith("%") && 
                currentLine.EndsWith(PdfTags.Obj);
        }

        private void SeekToStart()
        {
            this.reader.BaseStream.Seek(0, SeekOrigin.Begin);
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