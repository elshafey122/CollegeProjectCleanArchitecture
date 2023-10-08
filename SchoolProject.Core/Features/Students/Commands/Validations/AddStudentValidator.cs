using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public AddStudentValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.StuNamear).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.StuNameen).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Address).
                NotEmpty().WithMessage($"Address: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Phone).
                NotEmpty().WithMessage($"Phone: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Phone: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(11).WithMessage("length no more than 11");
        }
    }
}
