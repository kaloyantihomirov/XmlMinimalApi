using Microsoft.AspNetCore.WebUtilities;
using System.Xml.Serialization;

namespace XmlMinimalApi
{
    public class XmlResult<T> : IResult
    {
        //Specify the type that needs to be serialized - T in our case
        private static readonly XmlSerializer Serializer = new(typeof(T));

        private readonly T _result;

        public XmlResult(T result)
        {
            _result = result;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            using var ms = new FileBufferingWriteStream();

            Serializer.Serialize(ms, _result);
            ms.Position = 0;

            httpContext.Response.ContentType = "application/xml";
            await ms.DrainBufferAsync(httpContext.Response.Body);
        }
    }
}
