using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace UserInterface.Gateway.Admin.CommandResponses.Products
{
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class AlterProductResponse
    {
        public Guid Id { get; set; }
        public string NewCode { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public int OriginalVersion { get; set; }
    }
}