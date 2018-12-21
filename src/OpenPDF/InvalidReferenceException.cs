using System;
using System.Runtime.Serialization;

namespace OpenPDF
{
    [Serializable]
    public class InvalidReferenceException : Exception
    {
        public InvalidReferenceException(PdfCrossReference reference)
            : this(ErrorMessages.InvalidObjectReference)
        {
            this.Reference = reference;
        }

        public InvalidReferenceException(string message) : base(message)
        {
        }

        public InvalidReferenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PdfCrossReference Reference { get; }
    }
}
