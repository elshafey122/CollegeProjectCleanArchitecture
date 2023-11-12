using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.Authentication.Commands.Validation
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public SignInValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName).
                NotEmpty().WithMessage($"FullName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"FullName: {_stringLocalizer[SharedResourcesKeys.Empty]}");

            RuleFor(x => x.Password).
                NotEmpty().WithMessage($"UserName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"UserName: {_stringLocalizer[SharedResourcesKeys.Empty]}");
        }
    }
}