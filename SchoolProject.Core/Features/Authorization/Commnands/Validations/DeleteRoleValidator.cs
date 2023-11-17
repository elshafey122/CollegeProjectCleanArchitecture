using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commnands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.Authorization.Commnands.Validations
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DeleteRoleValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}");
        }
        public void ApplyCustomValidationRules()
        {
        }

    }
}
