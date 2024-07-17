using FluentValidation;
using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Identity Number is required.")
                .Length(11).WithMessage("Identity Number must be 11 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.CustomerNumber)
                .GreaterThan(0).WithMessage("Customer Number must be greater than 0.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");

            RuleForEach(x => x.CustomerAddresses).SetValidator(new CustomerAddressValidator());
            RuleForEach(x => x.CustomerPhones).SetValidator(new CustomerPhoneValidator());
            RuleFor(x => x.CustomerDetail).SetValidator(new CustomerDetailValidator());
        }
    }
}
