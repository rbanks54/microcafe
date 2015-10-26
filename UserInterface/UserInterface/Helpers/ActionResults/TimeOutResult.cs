using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace UserInterface.Helpers.ActionResults
{
    public class TimeOutResult : IHttpActionResult
    {
        public TimeOutResult(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            { StatusCode = HttpStatusCode.RequestTimeout, Content = new StringContent(Message) };

            return Task.FromResult(response);
        }
    }
}