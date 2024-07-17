using FluentValidation;
using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerAddressValidator : AbstractValidator<CustomerAddress>
    {
        public CustomerAddressValidator() 
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country cannot exceed 50 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");

            RuleFor(x => x.AddressLine)
                .NotEmpty().WithMessage("Address Line is required.")
                .MaximumLength(200).WithMessage("Address Line cannot exceed 200 characters.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zip Code is required.")
                .MaximumLength(10).WithMessage("Zip Code cannot exceed 10 characters.");

            RuleFor(x => x.IsDefault)
                .NotNull().WithMessage("IsDefault is required.");
        }
    }
}
