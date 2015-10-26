using System;

namespace Barista.Common.Dto
{
    public class OperatorDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
    }
}

