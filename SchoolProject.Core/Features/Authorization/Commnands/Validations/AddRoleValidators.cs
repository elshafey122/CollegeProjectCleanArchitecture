using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commnands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Commnands.Validations
{
    public class AddRoleValidators : AbstractValidator<AddRoleCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;

        public AddRoleValidators(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.RoleName).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}");
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.RoleName)
               .MustAsync(async (key, CancellationToken) => !await _authorizationService.IsRoleExist(key))
               .WithMessage($"RoleName: {_stringLocalizer[SharedResourcesKeys.IsExist]}");
        }
    }
}
