using AutoMapper;
using GLC.Core.Resources;
using GLC.Core.ViewModels;
using GLC.Cores.Models;

namespace GLC.Core.MappingProfile
{
  public class MappingProfile : Profile
  {
        public MappingProfile()
        {
            //Creating Map between SignupVM ~ StudentResouce
            //Reason: during registering student, I have to add the data of student to 
            // student table. without changing the base code to avoid crashing.

            CreateMap<Student, StudentResource>().ReverseMap().ForMember(n => n.StudentId, m => m.Ignore());
            CreateMap<Group, GroupResource>().ReverseMap().ForMember(n => n.GroupId, m => m.Ignore());
            CreateMap<Teacher, TeacherResource>().ReverseMap().ForMember(n => n.TeacherId, m => m.Ignore());

            CreateMap<SignUpVM, StudentResource>().
          ForMember(sr => sr.Name, opt => opt.MapFrom(vm => vm.UserName));

            CreateMap<Student, StudentResource>();
            CreateMap<StudentResource, Student>().ForMember(sr => sr.StudentId, s => s.Ignore());

            CreateMap<Group, GroupResource>().ReverseMap();
            CreateMap<Teacher, TeacherResource>().ReverseMap();
            CreateMap<GroupChat, GroupChatResource>().ReverseMap();
            CreateMap<ChatingDetails, ChattingDetailsResource>();
            CreateMap<ChattingDetailsResource, ChatingDetails>();
            CreateMap<QuestionAnswer, QuestionAnswerResource>().ReverseMap();
            CreateMap<QuestionBank, QuestionBankResource>().ReverseMap();
            CreateMap<QuestionCategory, QuestionCategoryResource>().ReverseMap();
            CreateMap<Quiz, QuizResource>().ReverseMap();
            CreateMap<StudentQuizeQuestionBank, StudentQuizQuestionBankResource>().ReverseMap();
            CreateMap<Subject, SubjectResource>().ReverseMap();
            CreateMap<Video, VideoResource>().ReverseMap();
            CreateMap<ChatingDetails, MessageResource>().ReverseMap();
        }

  }
}
