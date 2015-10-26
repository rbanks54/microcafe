using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace UserInterface.CommandResponses
{
    /// <summary>
    /// WebGateway Response to CreateProductCommand: POST api/products
    /// </summary>
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}