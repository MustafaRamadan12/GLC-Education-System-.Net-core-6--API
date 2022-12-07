using GLC.Core.Resources;
using GLC.Cores.Models;

namespace GLC.Core.IRepositories
{
  public interface IChatRepository : IGenericRepository<ChatingDetails, MessageResource>
  {
    Task<IEnumerable<MessageResource>> getMessage(Guid studentId, Guid groupId);
  }
}
