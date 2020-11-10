using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;


namespace SharpLoad.Dashboard.Client.ViewModels
{
    public class RequestViewModel
    {
        [Required]
        public Uri Path { get; set; }
        [Required]
        public HttpMethod Method { get; set; }
        [Required]
        public byte[] Body { get; set; }
        public IEnumerable<RequestHeaderViewModel> Headers { get; set; }
    }
}
