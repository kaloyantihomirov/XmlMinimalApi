using Microsoft.AspNetCore.WebUtilities;
using System.Xml.Serialization;

namespace XmlMinimalApi
{
    public class XmlResult<T> : IResult
    {
        /// <remarks>
        /// We can specify the type that needs to be serialized - T in our case
        /// </remarks>
        private static readonly XmlSerializer Serializer = new(typeof(T));

        private readonly T _result;

        public XmlResult(T result)
        {
            _result = result;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            /// <remarks>
            /// Uses the combination of buffer pooling and avoiding creating huge buffers in memory 
            /// to increase the performance
            /// </remarks>
            using var ms = new FileBufferingWriteStream();

            Serializer.Serialize(ms, _result);
            ms.Position = 0;

            httpContext.Response.ContentType = "application/xml";
            await ms.DrainBufferAsync(httpContext.Response.Body);
        }
    }
}
