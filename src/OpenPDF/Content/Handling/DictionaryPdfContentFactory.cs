using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenPDF.Content.Handling
{
    public sealed class DictionaryPdfContentFactory :
        IDictionaryPdfContentFactory
    {
        private static readonly IDictionary<string, ConstructorInfo> contentFactories
            = GetFactories();

        public DictionaryPdfObjectContent Create(
            IDictionary<string, PdfObjectContent> props)
        {
            string typeKey = props.Keys
                .FirstOrDefault(x => x.ToUpper().Equals("TYPE"));
            if (typeKey == null)
            {
                return new DictionaryPdfObjectContent(props);
            }

            string type = (props[typeKey].Content as string)?.ToUpper();
            if (contentFactories.ContainsKey(type))
            {
                return (DictionaryPdfObjectContent)contentFactories[type]
                    .Invoke(new object[] { props });
            }

            return new DictionaryPdfObjectContent(props);
        }

        private static IDictionary<string, ConstructorInfo> GetFactories()
        {
            return typeof(DictionaryPdfContentFactory).Assembly
                .GetTypes()
                .Where(x => !x.IsAbstract &&
                    x.IsClass &&
                    x.IsSubclassOf(typeof(DictionaryPdfObjectContent)) &&
                    x.GetConstructor(new[]
                    {
                        typeof(IDictionary<string, PdfObjectContent>)
                    }) != null)
                .Select(x => x.GetConstructor(new[]
                {
                    typeof(IDictionary<string, PdfObjectContent>)
                }))
                .Where(x => x != null)
                .ToDictionary(
                    x => x.ReflectedType.Name.Substring(
                            0,
                            x.ReflectedType.Name.Length - 
                                "PdfObjectContent".Length)
                        .ToUpper(),
                    x => x);
        }
    }
}
