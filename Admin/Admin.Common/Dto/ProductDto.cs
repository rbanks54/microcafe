using MicroServices.Common;

namespace Admin.Common.Dto
{
    public class ProductDto : ReadObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Version { get; set; }
        public string DisplayName { get; set; }
    }
}

