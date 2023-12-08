using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Instructors.Commands.Validation
{
    public class AddInstructorValidator : AbstractValidator<AddInstructorCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IInstructorService _instructorService;
        private readonly IDeparetementService _deparetementService;

        public AddInstructorValidator(IStringLocalizer<SharedResources> stringLocalizer, IInstructorService instructorService,
                                      IDeparetementService deparetementService)
        {
            _stringLocalizer = stringLocalizer;
            _instructorService = instructorService;
            _deparetementService = deparetementService;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.InstructotNameAr).
               NotEmpty().WithMessage($"InstructotNameAr: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .NotNull().WithMessage($"InstructotNameAr: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100])
               .MustAsync(async (key, CancellationToken) => await _instructorService.IsInstructorNameArIsExist(key))
               .WithMessage($"InstructotNameAr: {_stringLocalizer[SharedResourcesKeys.UserNameIsExist]}");

            RuleFor(x => x.InstructorNameEn).
                NotEmpty().WithMessage($"InstructorNameEn: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"InstructorNameEn: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100])
                .MustAsync(async (key, CancellationToken) => await _instructorService.IsInstructorNameEnIsExist(key))
               .WithMessage($"InstructorNameEn: {_stringLocalizer[SharedResourcesKeys.UserNameIsExist]}");

            RuleFor(x => x.SupervisorId)
               .NotEmpty().WithMessage("Supervisor ID is required.")
               .MustAsync(async (key, CancellationToken) => await _instructorService.IsInstructorIsExist(key))
               .WithMessage($"SupervisorId: {_stringLocalizer[SharedResourcesKeys.NotFound]}");

            RuleFor(x => x.DepartementId)
               .NotEmpty().WithMessage("Department ID is required.")
               .MustAsync(async (key, CancellationToken) => await _deparetementService.IsDepartementExist(key))
               .WithMessage($"departementid: {_stringLocalizer[SharedResourcesKeys.NotFound]}");
        }
    }
}
