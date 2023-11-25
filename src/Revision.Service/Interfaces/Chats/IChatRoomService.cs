using Revision.Service.DTOs.ChatRooms;

namespace Revision.Service.Interfaces.Chats;

public interface IChatRoomService
{
    Task<ChatRoomResultDto> CreateAsync(ChatRoomCreationDto dto);
    Task<bool> DestroyAsync(long id);
    Task<IEnumerable<ChatRoomResultDto>> GetByUserIdAsync(long userId);
    Task<ChatRoomResultDto> GetByEducationIdAsync(long educationId);
    Task<IEnumerable<ChatRoomResultDto>> GetAllAsync();
}