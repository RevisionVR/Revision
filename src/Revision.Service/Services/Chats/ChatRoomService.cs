using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Chats;
using Revision.Service.DTOs.ChatRooms;
using Revision.Service.Interfaces.Chats;

namespace Revision.Service.Services.Chats;

public class ChatRoomService : IChatRoomService
{
    private readonly IMapper _mapper;
    private readonly IRepository<ChatRoom> _chatRoomRepository;

    public Task<ChatRoomResultDto> CreateAsync(ChatRoomCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DestroyAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ChatRoomResultDto>> GetByEducationIdAsync(long educationId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ChatRoomResultDto>> GetByUserIdAsync(long userId)
    {
        throw new NotImplementedException();
    }
}
