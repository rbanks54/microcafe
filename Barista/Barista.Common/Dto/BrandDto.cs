using System;

namespace Barista.Common.Dto
{
    /// <summary>
    /// This is the projection of the Master Data - Brand object
    /// </summary>
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
