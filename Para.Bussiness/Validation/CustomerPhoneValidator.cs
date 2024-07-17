using FluentValidation;
using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerPhoneValidator : AbstractValidator<CustomerPhone>
    {
        public CustomerPhoneValidator() 
        {
            RuleFor(x => x.CountyCode)
            .NotEmpty().WithMessage("County Code is required.")
            .MaximumLength(3).WithMessage("County Code cannot exceed 3 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\d+$").WithMessage("Phone must contain only digits.")
                .Length(10).WithMessage("Phone must be 10 digits long.");

            RuleFor(x => x.IsDefault)
                .NotNull().WithMessage("IsDefault is required.");
        }
    }
}
