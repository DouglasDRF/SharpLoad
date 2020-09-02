using SharpLoad.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpLoad.AppService.ViewModels
{
    public class RequestViewModel : BaseViewModel
    {
        [Required]
        public Uri Path { get; set; }
        [Required]
        public HttpMethods Method {get; set;}
        [Required]
        public byte[] Body { get; set; }
        public IEnumerable<RequestHeaderViewModel> Headers { get; set; }
    }
}