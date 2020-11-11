using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SharpLoad.Dashboard.Client.ViewModels.Enums;


namespace SharpLoad.Dashboard.Client.ViewModels
{
    public class RequestViewModel
    {
        [Required]
        public Uri Path { get; set; }
        [Required]
        public HttpMethod Method { get; set; }
        [Required]
        public string Body { get; set; }

        public IEnumerable<RequestHeaderViewModel> RequestHeaders => ParseBulkHeadersToHeadersObject();

        [JsonIgnore]
        public string BulkHeaders { get; set; }

        private IEnumerable<RequestHeaderViewModel> ParseBulkHeadersToHeadersObject()
        {
            IList<RequestHeaderViewModel> requestHeaders = new List<RequestHeaderViewModel>();
            string[] subStr = BulkHeaders.Split(";");

            foreach (var s in subStr)
            {
                string[] keyVal = s.Split(":");
                requestHeaders.Add(new RequestHeaderViewModel{ Key = keyVal[0], Value = keyVal[1]});
            }

            return requestHeaders;
        }
    }
}
