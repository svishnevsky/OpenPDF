using System.IO;

namespace OpenPDF
{
    public static class FileStreamReaderExtensions
    {
        public static void SeekToStart(this FileStreamReader reader)
        {
            reader.Seek(0, SeekOrigin.Begin);
        }

        public static void SeekToEnd(this FileStreamReader reader)
        {
            reader.Seek(0, SeekOrigin.End);
        }
    }
}
