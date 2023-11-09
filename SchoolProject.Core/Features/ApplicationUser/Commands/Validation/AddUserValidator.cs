using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Localization;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validation
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public AddUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.FullName).
                NotEmpty().WithMessage($"FullName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"FullName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.UserName).
                NotEmpty().WithMessage($"UserName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"UserName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Email).
                NotEmpty().WithMessage($"Email: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Email: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Address).
                NotEmpty().WithMessage($"Address: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Address: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Phone).
                NotEmpty().WithMessage($"Phone: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Phone: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Password).
                NotEmpty().WithMessage($"Password: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Password: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.ConfirmPassword).
                NotEmpty().WithMessage($"ConfirmPassword: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"ConfirmPassword: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .Matches(x => x.Password).WithMessage($"ConfirmPassword: {_stringLocalizer[SharedResourcesKeys.NotConfirmPassword]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);
        }
    }
}
