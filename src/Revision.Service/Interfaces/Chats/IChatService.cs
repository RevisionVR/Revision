using Revision.Service.DTOs.Chats;

namespace Revision.Service.Interfaces.Chats;

public interface IChatService
{
    Task<ChatResultDto> CreateAsync(ChatCreationDto dto);
    Task<ChatResultDto> UpdateAsync(ChatUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<ChatResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ChatResultDto>> GetAllAsync();
    Task<IEnumerable<ChatResultDto>> GetByRoomIdAsync(long roomId);
}