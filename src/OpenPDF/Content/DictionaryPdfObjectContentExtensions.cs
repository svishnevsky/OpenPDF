using System;

namespace OpenPDF.Content
{
    public static class DictionaryPdfObjectContentExtensions
    {
        public static T Value<T>(
            this DictionaryPdfObjectContent content, 
            string key)
        {
            if (!content.Value.ContainsKey(key))
            {
                return default(T);
            }

            PdfObjectContent value = content.Value[key];
            if (value.Content is T)
            {
                return (T)value.Content;
            }

            return (T)Convert.ChangeType(value.Content, typeof(T));
        }
    }
}
