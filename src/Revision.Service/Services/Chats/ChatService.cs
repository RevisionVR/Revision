using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Chats;
using Revision.Service.DTOs.Chats;
using Revision.Service.Interfaces.Chats;

namespace Revision.Service.Services.Chats;

public class ChatService : IChatService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Chat> _chatRepository;

    public Task<ChatResultDto> CreateAsync(ChatCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ChatResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ChatResultDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ChatResultDto>> GetByRoomIdAsync(long roomId)
    {
        throw new NotImplementedException();
    }

    public Task<ChatResultDto> UpdateAsync(ChatUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
