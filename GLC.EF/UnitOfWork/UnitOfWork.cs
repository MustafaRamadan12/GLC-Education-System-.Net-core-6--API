using AutoMapper;
using GLC.Core.IRepositories;
using GLC.Core.IUnitOfWork;
using GLC.Core.Resources;
using GLC.Cores.Models;
using GLC.EF.Repositories;

namespace GLC.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GLCDbContext _context;
        private readonly IMapper _mapper;

        // add all your models here as the line below.
        public IGenericRepository<Student, StudentResource> Students { get; private set; }
        public IGenericRepository<GroupChat, GroupChatResource> GroupChats { get; private set; }

        public IChatRepository Chats { get; private set; }

        public ITeacher teachers { get; }
        public IVideo videos { get; }
        public ISubject subject { get; }
        public IGenericRepository<ChatingDetails, ChattingDetailsResource> ChattingDetails { get; private set; }

        //public IGenericRepository<Test> Tests { get; private set; }
        public UnitOfWork(GLCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            // initialize all the models as the line below.
            Students = new GenericRepository<Student, StudentResource>(_context, _mapper);
            GroupChats = new GenericRepository<GroupChat, GroupChatResource>(_context, _mapper);
            Chats = new ChatRepository(_context, mapper);
            teachers = new Teacher_Repositry(_context, mapper);
            videos = new Video_Repositry(_context, mapper);
            subject = new Subject_Repositary(_context, mapper);
            ChattingDetails = new GenericRepository<ChatingDetails, ChattingDetailsResource>(_context, _mapper);
        }
        public async Task<int> CompleteAsync()
        {
            // return the (n) affected rows.
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
