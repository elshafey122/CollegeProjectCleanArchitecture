using SchoolProject.Core.Features.Departements.Queries.ViewModels;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departements
{
    public partial class DepartementProfile
    {
        public void GetDepartementByIdMapping()
        {

            // complex mapping 
            CreateMap<Departement, DepartementByIdResponse>()
                .ForMember(des => des.DepartementName, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(des => des.InstructorManager, opt => opt.MapFrom(src => src.Instructor.Localize(src.Instructor.NameAr, src.Instructor.NameEn)))
                //.ForMember(des => des.StudentsList, opt => opt.MapFrom(src => src.Students))
                .ForMember(des => des.InstuctorsList, opt => opt.MapFrom(src => src.Instructors))
                .ForMember(des => des.SubjectsList, opt => opt.MapFrom(src => src.DepartementSubjects));

            CreateMap<DepartementSubject, SubjectResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.SubNameAr, src.Subject.SubNameEn)));

            //CreateMap<Student, StudentResponse>()
            //    .ForMember(des => des.Id, opt => opt.MapFrom(src => src.StuId))
            //    .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.StuNameAr, src.StuNameEn)));

            CreateMap<Instructor, InstuctorResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

        }
    }
}
