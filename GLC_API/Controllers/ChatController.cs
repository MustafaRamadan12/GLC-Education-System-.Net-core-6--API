using GLC.Core.IUnitOfWork;
using GLC.Core.Resources;
using GLC.EF;
using GLC_SAL.Hub;
using GLC_SAL.IHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GLC_API.Controllers
{
  [Route("api/chat")]
  [ApiController]
  public class ChatController : ControllerBase
  {
    private IHubContext<MessageHub, IMessageHub> _messageHub;
    private readonly GLCDbContext context;
    private readonly IUnitOfWork _unitOfWork;

    public ChatController(IUnitOfWork unitOfWork, IHubContext<MessageHub, IMessageHub> messageHub, GLCDbContext context)
    {
      _messageHub = messageHub;
      this.context = context;
      _unitOfWork = unitOfWork;
    }

    [HttpPost("{studentId}/{groupId}")]

    public async Task<IActionResult> GetMessages(Guid studentId, Guid groupId)
    {

      var messages = await _unitOfWork.Chats.getMessage(studentId, groupId);
      //List<string> messages = new List<string>()
      //{
      //    "Message1",
      //    "Message2",
      //    "Message3",
      //};
      await _messageHub.Clients.All.SendMessagesToUser(messages);

      return Ok(messages);
    }

    [HttpPost]
    [Route("/addMessage")]
    public async Task<IActionResult> AddMessage([FromBody] ChattingDetailsResource message)
    {
      var groupChatId = Guid.NewGuid();
      await _unitOfWork.GroupChats.AddAsync(new GroupChatResource { GroupChatId = groupChatId, level = 3, Message = "DUMMY" });
      await _unitOfWork.CompleteAsync();
      var studentId = new Guid("2411e7e6-5f85-4c88-b998-2d6264f25ac0");
      var groupId = new Guid("c4e749db-6a73-ed11-b731-7ce9d3e8d6eb");
      var obj = new ChattingDetailsResource
      {

        Message = message.Message,
        StId = studentId,
        GroupId = groupId,
        GroupChatId = groupChatId,
        TeacherId = new Guid("21003214-d96b-ed11-856b-e02a82c7cd64"),
        IsSenderStudent = message.IsSenderStudent,
        DateTime = DateTime.Now
      };

      var sentMessage = await _unitOfWork.ChattingDetails.AddAsync(obj);
      var messages = await _unitOfWork.Chats.getMessage(studentId, groupId);
      await _messageHub.Clients.All.SendMessagesToUser(messages);
      await _unitOfWork.CompleteAsync();
      return Ok(sentMessage);
    }
  }
}
