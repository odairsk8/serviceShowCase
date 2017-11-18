using FluentValidation;
using GC.Core.Entities;

namespace GC.Core.Validations
{
    public class PhotoValidator : AbstractValidator<Photo>
    {
        public PhotoValidator()
        {
            RuleFor(c => c.FileName)
                .NotEmpty().MaximumLength(255);
        }
    }
}
