using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validation
{
    public class ChangeUserPasswordvalidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public ChangeUserPasswordvalidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id).
                NotEmpty().WithMessage($"Id: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Id: {_stringLocalizer[SharedResourcesKeys.Empty]}");

            RuleFor(x => x.CurrentPassword).
                NotEmpty().WithMessage($"CurrentPassword: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"CurrentPassword: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.NewPassword).
               NotEmpty().WithMessage($"NewPassword: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .NotNull().WithMessage($"NewPassword: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.ConfirmPassword).
               NotEmpty().WithMessage($"Address: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .NotNull().WithMessage($"Address: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .Matches(x => x.NewPassword).WithMessage($"ConfirmPassword: {_stringLocalizer[SharedResourcesKeys.NotConfirmPassword]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);
        }
    }
}
