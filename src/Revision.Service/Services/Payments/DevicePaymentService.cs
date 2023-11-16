using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Payments;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.DevicePayments;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Payments;

namespace Revision.Service.Services.Payments;

public class DevicePaymentService : IDevicePaymentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Education> _educationRepository;
    private readonly IRepository<DevicePayment> _paymentRepository;
    public DevicePaymentService(
        IMapper mapper,
        IRepository<Education> educationRepository,
        IRepository<DevicePayment> paymentRepository)
    {
        _mapper = mapper;
        _paymentRepository = paymentRepository;
        _educationRepository = educationRepository;
    }

    public async Task<DevicePaymentResultDto> CreateAsync(DevicePaymentCreationDto dto)
    {
        var existEducation = await _educationRepository.SelectAsync(education => education.Id.Equals(dto.EducationId))
            ?? throw new RevisionException(404, "This education is not found");

        var mappedPayment = _mapper.Map<DevicePayment>(dto);

        mappedPayment.LastDay = TimeHelper.GetDateTime();
        mappedPayment.NextDay = mappedPayment.LastDay.AddMonths(1);

        mappedPayment.CreatedAt = TimeHelper.GetDateTime();
        mappedPayment.Education = existEducation;

        await _paymentRepository.AddAsync(mappedPayment);
        await _paymentRepository.SaveAsync();

        return _mapper.Map<DevicePaymentResultDto>(mappedPayment);
    }

    public async Task<DevicePaymentResultDto> GetByIdAsync(long id)
    {
        var existPayment = await _paymentRepository.SelectAsync(payment => payment.Id.Equals(id),
            includes: new[] { "Education" })
            ?? throw new RevisionException(404, "This device payment is not found");

        return _mapper.Map<DevicePaymentResultDto>(existPayment);
    }
    public async Task<IEnumerable<DevicePaymentResultDto>> GetByEducationIdAsync(long educationId)
    {
        var existPayments = await _paymentRepository.SelectAll(payment => payment.EducationId.Equals(educationId),
        includes: new[] { "Education" })
            .ToListAsync();
        if (!existPayments.Any())
            throw new RevisionException(404, "This education is not found");

        return _mapper.Map<IEnumerable<DevicePaymentResultDto>>(existPayments);
    }

    public async Task<IEnumerable<DevicePaymentResultDto>> GetAllAsync()
    {
        var payments = await _paymentRepository.SelectAll(includes: new[] { "Education" })
            .ToListAsync();

        return _mapper.Map<IEnumerable<DevicePaymentResultDto>>(payments);
    }
}
