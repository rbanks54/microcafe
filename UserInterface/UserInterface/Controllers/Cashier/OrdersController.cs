using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserInterface.Helpers.Configuration;
using UserInterface.Helpers.Services;
using System.Web;
using UserInterface.Models.Entities;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using UserInterface.Helpers.ActionResults;

namespace UserInterface.Controllers
{
    /// <summary>
    /// This controller acts as the API Gateway point for the Aggregate "Product"
    /// 
    /// NOTE:
    /// The implementation is design to reflect the methodology of using
    /// a web ui to call this endpoint and then this to communicate to 
    /// the various "Domain Conexts" apis.
    /// 
    /// NOTE:
    /// This is using a repository to dummy up the data interactions.
    /// This will be replaced with api calls to interact with the
    /// services.
    /// </summary>
    public class OrdersController : ApiController
    {
        private IProductsService ordersService;

        public OrdersController() : this(new ProductsService())
        {
        }

        public OrdersController(IProductsService productsService)
        {
            this.ordersService = productsService;
        }
    }
}
