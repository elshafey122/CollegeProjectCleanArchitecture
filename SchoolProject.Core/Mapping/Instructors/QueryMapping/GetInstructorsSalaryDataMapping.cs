using SchoolProject.Core.Features.Instructors.Queries.ViewModels;
using SchoolProject.Data.Entities.Functions;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void GetInstructorsSalaryDataMapping()
        {
            CreateMap<InstructorSalaryData, GetInstructorsSalaryDataResponse>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}
