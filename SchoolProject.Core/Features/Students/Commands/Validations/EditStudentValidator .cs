using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public EditStudentValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            APPlyCustomValidationRules();
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
        }
        public void APPlyCustomValidationRules()
        {

        }
    }
}
