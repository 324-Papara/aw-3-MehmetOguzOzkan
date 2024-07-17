using FluentValidation;
using Para.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Validation
{
    public class CustomerDetailValidator : AbstractValidator<CustomerDetail>
    {
        public CustomerDetailValidator() 
        {
            RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage("Father Name is required.")
            .MaximumLength(50).WithMessage("Father Name cannot exceed 50 characters.");

            RuleFor(x => x.MotherName)
                .NotEmpty().WithMessage("Mother Name is required.")
                .MaximumLength(50).WithMessage("Mother Name cannot exceed 50 characters.");

            RuleFor(x => x.EducationStatus)
                .NotEmpty().WithMessage("Education Status is required.")
                .MaximumLength(50).WithMessage("Education Status cannot exceed 50 characters.");

            RuleFor(x => x.MontlyIncome)
                .NotEmpty().WithMessage("Monthly Income is required.")
                .MaximumLength(50).WithMessage("Monthly Income cannot exceed 50 characters.");

            RuleFor(x => x.Occupation)
                .NotEmpty().WithMessage("Occupation is required.")
                .MaximumLength(50).WithMessage("Occupation cannot exceed 50 characters.");
        }
    }
}
