using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Instructors.Commands.Validation
{
    public class EditInstructorValidator : AbstractValidator<EditInstructorCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IInstructorService _instructorService;
        private readonly IDeparetementService _deparetementService;

        public EditInstructorValidator(IStringLocalizer<SharedResources> stringLocalizer, IInstructorService instructorService,
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
               NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.InstructorNameEn).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.DepartementId)
               .NotEmpty().WithMessage("DepartementId ID is required.")
               .MustAsync(async (key, CancellationToken) => await _deparetementService.IsDepartementExist(key))
               .WithMessage($"departementid: {_stringLocalizer[SharedResourcesKeys.NotFound]}");

            RuleFor(x => x.SupervisorId)
               .NotEmpty().WithMessage("SupervisorId is required.")
               .MustAsync(async (key, CancellationToken) => await _instructorService.IsInstructorIsExist(key))
               .WithMessage($"SupervisorId: {_stringLocalizer[SharedResourcesKeys.NotFound]}");

            RuleFor(x => x.InstructorId)
               .NotEmpty().WithMessage("InstructorId is required.")
               .MustAsync(async (key, CancellationToken) => await _instructorService.IsInstructorIsExist(key))
               .WithMessage($"InstructorId: {_stringLocalizer[SharedResourcesKeys.NotFound]}");

            RuleFor(x => x.InstructotNameAr)
               .NotEmpty().WithMessage("InstructotNameAr is required.")
               .MustAsync(async (model, key, CancellationToken) => await _instructorService.IsInstructorNameArExceptItselfIsExist(key, model.InstructorId))
               .WithMessage($"InstructorNameAr: {_stringLocalizer[SharedResourcesKeys.IsExist]}");

            RuleFor(x => x.InstructorNameEn)
               .NotEmpty().WithMessage("InstructorNameEn is required.")
               .MustAsync(async (model, key, CancellationToken) => await _instructorService.IsInstructorNameEnExceptItselfIsExist(key, model.InstructorId))
               .WithMessage($"InstructorNameEn: {_stringLocalizer[SharedResourcesKeys.IsExist]}");
        }
    }
}
