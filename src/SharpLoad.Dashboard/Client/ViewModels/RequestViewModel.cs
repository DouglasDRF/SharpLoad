using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using SharpLoad.Dashboard.Client.ViewModels.Enums;


namespace SharpLoad.Dashboard.Client.ViewModels
{
    public class RequestViewModel
    {
        [Required]
        public string Path { get; set; }
        [Required]
        public HttpMethods Method { get; set; }
        public string Body { get; set; }

        public IEnumerable<RequestHeaderViewModel> RequestHeaders => ParseBulkHeadersToHeadersObject();

        [JsonIgnore]
        public string BulkHeaders { get; set; }

        private IEnumerable<RequestHeaderViewModel> ParseBulkHeadersToHeadersObject()
        {
            string[] subStr = BulkHeaders?.Split(";");
            return subStr?.Select(s => s.Split(":")).Select(keyVal => new RequestHeaderViewModel {Key = keyVal[0], Value = keyVal[1]}).ToList();
        }
    }
}
