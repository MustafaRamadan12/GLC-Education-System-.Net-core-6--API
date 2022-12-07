using AutoMapper;
using GLC.Core.IRepositories;
using GLC.Core.Resources;
using GLC.Cores.Models;
using Microsoft.EntityFrameworkCore;

namespace GLC.EF.Repositories
{
  public class ChatRepository : GenericRepository<ChatingDetails, MessageResource>, IChatRepository
  {
    private readonly GLCDbContext context;
    private readonly IMapper mapper;

    public ChatRepository(GLCDbContext context, IMapper mapper) : base(context, mapper)
    {
      this.context = context;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<MessageResource>> getMessage(Guid studentId, Guid groupId)
    {

      var messages = await context.ChatingDetails
        .Where(cd => cd.StId == studentId && cd.GroupId == groupId)
        .OrderBy(d => d.DateTime)
        .Select(s => new ChatingDetails { IsSenderStudent = s.IsSenderStudent, Message = s.Message })
        .ToListAsync();

      return mapper.Map<List<ChatingDetails>, List<MessageResource>>(messages);
    }
  }
}
