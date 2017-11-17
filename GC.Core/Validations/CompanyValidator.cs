using FluentValidation;
using GC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC.Core.Validations
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().MinimumLength(5).MaximumLength(255);
            RuleFor(c => c.Foundation).NotNull().NotEqual(DateTime.MinValue);
            RuleFor(c => c.History).MaximumLength(1000);
            
        }
    }
}
