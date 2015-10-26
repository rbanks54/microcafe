using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace UserInterface.Helpers.ActionResults
{
    public class PagedOkResult<T>: OkNegotiatedContentResult<T>
    {
        public PagedOkResult(T content, ApiController controller)
        : base(content, controller) { }

        public PagedOkResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) 
        : base(content, contentNegotiator, request, formatters) { }

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.ExecuteAsync(cancellationToken);

            var paginationHeader = new
            {
                TotalCount = TotalCount,
                TotalPages = TotalPages,
            };

            response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            return response;
        }
    }
}