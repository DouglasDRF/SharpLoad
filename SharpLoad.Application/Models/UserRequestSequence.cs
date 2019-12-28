namespace SharpLoad.Application.Models
{
    public class UserRequestSequence
    {
        public HttpMethods HttpMethod { get; set; }
        public string Path { get; set; }
        public object Body { get; set; }
        public string ApplicationType { get; set; }
    }
}