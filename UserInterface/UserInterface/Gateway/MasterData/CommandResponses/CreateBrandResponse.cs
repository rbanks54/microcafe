using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace UserInterface.MasterData
{
    /// <summary>
    /// WebGateway Response to CreateProductCommand: POST api/products
    /// </summary>
    public class CreateBrandResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}