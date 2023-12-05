using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Email.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.Email.Commands.Validation
{
    public class SendEmaiValidator : AbstractValidator<SendEmaiCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public SendEmaiValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage($"Email: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Email: {_stringLocalizer[SharedResourcesKeys.Empty]}");

            RuleFor(x => x.Message)
               .NotEmpty().WithMessage($"Message: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .NotNull().WithMessage($"Message: {_stringLocalizer[SharedResourcesKeys.Empty]}");
        }
    }
}
