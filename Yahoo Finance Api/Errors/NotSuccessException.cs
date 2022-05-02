using System;
using System.Runtime.Serialization;

namespace Yahoo_Finance_Api.Errors;

public class NotSuccessException : Exception
{
    public NotSuccessException()
    {
    }

    public NotSuccessException(string message) : base(message)
    {
    }

    public NotSuccessException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected NotSuccessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
