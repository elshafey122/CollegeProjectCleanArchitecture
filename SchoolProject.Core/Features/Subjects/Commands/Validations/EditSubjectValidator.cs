using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Subjects.Commands.Validations
{
    public class EditSubjectValidator : AbstractValidator<EditSubjectCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ISubjectService _instructorService;

        public EditSubjectValidator(IStringLocalizer<SharedResources> stringLocalizer, ISubjectService instructorService,
                                      IDeparetementService deparetementService)
        {
            _stringLocalizer = stringLocalizer;
            _instructorService = instructorService;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.SubjectNameAr).
               NotEmpty().WithMessage($"SubjectNameAr: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .NotNull().WithMessage($"SubjectNameAr: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100])
               .MustAsync(async (key, CancellationToken) => await _instructorService.IsSubjectNameArIsExist(key))
               .WithMessage($"SubjectNameAr: {_stringLocalizer[SharedResourcesKeys.IsExist]}");

            RuleFor(x => x.SubjectNameEn).
                NotEmpty().WithMessage($"SubjectNameEn: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"SubjectNameEn: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100])
                .MustAsync(async (key, CancellationToken) => await _instructorService.IsSubjectNameEnIsExist(key))
               .WithMessage($"SubjectNameEn: {_stringLocalizer[SharedResourcesKeys.IsExist]}");

            RuleFor(x => x.Period)
               .NotEmpty().WithMessage("Period")
               .NotNull().WithMessage($"Period: {_stringLocalizer[SharedResourcesKeys.Empty]}")
               .WithMessage($"Period: {_stringLocalizer[SharedResourcesKeys.NotFound]}");
        }
    }
}
