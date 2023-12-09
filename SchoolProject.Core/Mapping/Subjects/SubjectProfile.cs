using AutoMapper;

namespace SchoolProject.Core.Mapping.Subjects
{
    public partial class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            GetSubjectByIdMapping();
            AddSubjectMapping();
            EditSubjectMapping();
        }
    }
}
