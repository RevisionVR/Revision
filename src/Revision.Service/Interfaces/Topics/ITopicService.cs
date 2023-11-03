using Revision.Domain.Configurations;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.Interfaces.Topics;

public interface ITopicService
{
    Task<TopicResultDto> CreateAsync(TopicCreationDto dto);
    Task<TopicResultDto> UpdateAsync(long id, TopicUpdateDto dto);
    Task<bool> DeleteAsync(long id);
    Task<bool> DestroyAsync(long id);
    Task<TopicResultDto> GetByIdAsync(long id);
    Task<IEnumerable<TopicResultDto>> GetAllAsync(PaginationParams pagination);
}