using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Departements.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.Departements.Commands.Validation
{
    public class EditDepartementvalidator : AbstractValidator<AddDepartementCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public EditDepartementvalidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.DepartementNameAr)
                .NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.DepartementNameEn).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);
        }
    }
}
