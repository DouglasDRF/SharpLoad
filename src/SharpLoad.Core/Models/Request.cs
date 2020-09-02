using SharpLoad.Core.Enums;
using System;
using System.Collections.Generic;

namespace SharpLoad.Core.Models
{
    public class Request : BaseModel
    {
        public Uri Path { get; private set; }
        public HttpMethods Method { get; private set; }
        public byte[] Body { get; private set; }
        public virtual IEnumerable<RequestHeader> Headers { get; private set; }
        public Request(int id, Uri path, HttpMethods method, byte[] body, IEnumerable<RequestHeader> headers) : base(id)
        {
            Path = path;
            Method = method;
            Body = body;
            Headers = headers;
        }
        protected Request()
        {

        }
    }
}