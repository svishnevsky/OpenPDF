using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OpenPDF
{
    public class FileStreamReader : IDisposable
    {
        private readonly Stream stream;

        private bool disposed = false;

        public long Position => this.stream.Position;

        public bool EndOfStream => this.Position == this.stream.Length;

        public FileStreamReader(Stream stream)
        {
            this.stream = stream 
                ?? throw new ArgumentNullException(nameof(stream));
        }

        public Task<string> ReadLine(
            ReadDirection direction = ReadDirection.TopToBottom)
        {
            return Task.Factory.StartNew(() =>
            {
                if (direction == ReadDirection.TopToBottom)
                {
                    var lineBuilder = new StringBuilder();
                    int current = 0;
                    while (!this.EndOfStream &&
                        (current = this.stream.ReadByte()) != 10)
                    {
                        lineBuilder.Append(Convert.ToChar(current));
                    }

                    return lineBuilder.ToString();
                }

                return this.ReadLineBackward();
            });
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            this.stream.Seek(offset, origin);
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
                this.stream.Dispose();
                this.disposed = true;
            }
        }

        private string ReadLineBackward()
        {
            if (this.stream.Position == this.stream.Length)
            {
                this.stream.Position--;
            }

            this.stream.Position--;
            var lineBuilder = new StringBuilder();
            int current = this.stream.ReadByte();
            this.stream.Position -= current == 10 ? 2 : 1;
            while ((current = this.stream.ReadByte()) != 10)
            {
                lineBuilder.Insert(0, Convert.ToChar(current));
                this.stream.Position -= 2;
            }

            return lineBuilder.ToString();
        }
    }
}
