using System;
using System.Linq;
using Cashier.Common.Events;
using MicroServices.Common;

namespace Cashier.Service.MicroServices.Brand.Domain
{
    public class Brand : Aggregate
    {
        private Brand()
        {
        }

        public Brand(Guid id, string code, string name)
        {
            // Business Logic Validation
            ValidateCode(code);

            ApplyEvent(new BrandCreated(id, code, name));
        }

        public string Code { get; private set; }
        public string Name { get; private set; }

        #region "Apply Events"

        private void Apply(BrandCreated e)
        {
            Id = e.Id;
            Code = e.Code;
        }

        private void Apply(BrandCodeChanged e)
        {
            Code = e.NewCode;
        }

        private void Apply(BrandNameChanged e)
        {
            Name = e.NewName;
        }

        #endregion

        #region "Change Aggregate"

        public void ChangeCode(string newCode, int originalVersion)
        {
            // Business Logic Validation
            ValidateCode(newCode);

            //can only rename the current version of the aggregate
            ValidateVersion(originalVersion);

            // Raise a series here?
            ApplyEvent(new BrandCodeChanged(Id, newCode));
        }

        public void ChangeName(string newName, int originalVersion)
        {
            //can only rename the current version of the aggregate
            ValidateVersion(originalVersion);

            // Raise a series here?
            ApplyEvent(new BrandNameChanged(Id, newName));
        }

        #endregion

        #region "Validation Methods"

        private void ValidateCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Invalid code specified: cannot be empty.", "code");
            }

            if (code.Any(char.IsLower))
            {
                throw new ArgumentException("Invalid code specified: cannot contain lowercase letters.", "code");
            }
        }

        private void ValidateVersion(int version)
        {
            if (Version != version)
            {
                throw new ArgumentOutOfRangeException("version", "Invalid version specified: the version is out of sync.");
            }
        }

        #endregion
    }
}