using System;
using MicroServices.Common;

namespace Cashier.Service.MicroServices.Brand.Commands
{
    public class CreateBrand : ICommand
    {
        public CreateBrand(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
    }

    public class AlterBrand : ICommand
    {
        public AlterBrand(Guid id, int originalVersion, string newCode, string newName)
        {
            Id = id;
            NewCode = newCode;
            NewName = newName;
            OriginalVersion = originalVersion;
        }

        public Guid Id { get; private set; }
        public string NewCode { get; private set; }
        public string NewName { get; private set; }
        public int OriginalVersion { get; private set; }
    }
}