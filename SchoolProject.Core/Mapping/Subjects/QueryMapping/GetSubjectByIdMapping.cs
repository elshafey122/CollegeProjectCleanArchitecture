using SchoolProject.Core.Features.Subjects.Queires.ViewModels;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subjects
{
    public partial class SubjectProfile
    {
        public void GetSubjectByIdMapping()
        {
            CreateMap<Subject, SubjectByIdResponse>()
                .ForMember(des => des.SubjectName, opt => opt.MapFrom(src => src.Localize(src.SubNameAr, src.SubNameEn)));
        }
    }
}
