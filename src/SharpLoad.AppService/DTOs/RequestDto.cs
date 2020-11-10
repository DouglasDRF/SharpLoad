using SharpLoad.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpLoad.AppService.DTOs
{
    public class RequestDto : BaseDto
    {
        [Required]
        public Uri Path { get; set; }
        [Required]
        public HttpMethods Method {get; set;}
        [Required]
        public byte[] Body { get; set; }
        public IEnumerable<RequestHeaderDto> Headers { get; set; }
    }
}