using Revision.Domain.Configurations;
using Revision.Service.DTOs.TopicPayments;

namespace Revision.Service.Interfaces.Payments;

public interface ITopicPaymentService
{
    Task<TopicPaymentResultDto> CreateAsync(TopicPaymentCreationDto dto);
    Task<TopicPaymentResultDto> GetByIdAsync(long id);
    Task<IEnumerable<TopicPaymentResultDto>> GetAllAsync(PaginationParams pagination);
}
