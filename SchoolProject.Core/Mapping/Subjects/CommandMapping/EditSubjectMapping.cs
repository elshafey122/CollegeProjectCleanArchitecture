using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subjects
{
    public partial class SubjectProfile
    {
        public void EditSubjectMapping()
        {
            CreateMap<EditSubjectCommand, Subject>()
                .ForMember(des => des.SubNameAr, opt => opt.MapFrom(src => src.SubjectNameAr))
                .ForMember(des => des.SubNameEn, opt => opt.MapFrom(src => src.SubjectNameEn))
                .ForMember(des => des.SubID, opt => opt.MapFrom(src => src.SubjectId));
        }
    }
}