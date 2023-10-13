using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IStudentService _studentservice;

        public AddStudentValidator(IStringLocalizer<SharedResources> stringLocalizer, IStudentService istudentservice)
        {
            _stringLocalizer = stringLocalizer;
            _studentservice = istudentservice;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.StuNamear).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.StuNameen).
                NotEmpty().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Address).
                NotEmpty().WithMessage($"Address: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"StuName: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(x => x.Phone).
                NotEmpty().WithMessage($"Phone: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .NotNull().WithMessage($"Phone: {_stringLocalizer[SharedResourcesKeys.Empty]}")
                .MaximumLength(11).WithMessage("length no more than 11");

            RuleFor(x => x.DepartementId)
               .NotEmpty().WithMessage("Department ID is required.")
               .MustAsync(async (key, CancellationToken) => await _studentservice.IsDepartementIdExist(key))
               .WithMessage($"departementid: {_stringLocalizer[SharedResourcesKeys.NotFound]}");
        }
    }
}
