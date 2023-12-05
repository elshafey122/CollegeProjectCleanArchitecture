using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.Authentication.Commands.Validation
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public SendResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email).
                NotEmpty().WithMessage($"FullName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"FullName: {_stringLocalizer[SharedResourcesKeys.Empty]}");
        }
    }
}