﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Payments;
using Revision.Domain.Entities.Topics;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.TopicPayments;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Payments;

namespace Revision.Service.Services.Payments;

public class TopicPaymentService : ITopicPaymentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Education> _educationRepository;
    private readonly IRepository<TopicPayment> _paymentRepository;
    public TopicPaymentService(
        IMapper mapper,
        IRepository<Topic> topicRepository,
        IRepository<Education> educationRepository,
        IRepository<TopicPayment> paymentRepository)
    {
        _mapper = mapper;
        _topicRepository = topicRepository;
        _paymentRepository = paymentRepository;
        _educationRepository = educationRepository;
    }

    public async Task<TopicPaymentResultDto> CreateAsync(TopicPaymentCreationDto dto)
    {
        var existTopic = await _topicRepository.SelectAsync(topic => topic.Id.Equals(dto.TopicId))
            ?? throw new RevisionException(404, "This topic is not found");

        var education = await _educationRepository.SelectAsync(education => education.Id.Equals(dto.EducationId))
            ?? throw new RevisionException(404, "This education is not found");
        
        dto.NextDate = dto.NextDate.ToUniversalTime();
        dto.LastDate = dto.LastDate.ToUniversalTime();
        var mappedPayment = _mapper.Map<TopicPayment>(dto);

        mappedPayment.CreatedAt = TimeHelper.GetDateTime();
        mappedPayment.Topic = existTopic;
        mappedPayment.Education = education;

        await _paymentRepository.AddAsync(mappedPayment);
        await _paymentRepository.SaveAsync();

        return _mapper.Map<TopicPaymentResultDto>(mappedPayment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existPayment = await _paymentRepository.SelectAsync(payment => payment.Id.Equals(id))
            ?? throw new RevisionException(404, "This topic payment is not found");

        _paymentRepository.Delete(existPayment);
        await _paymentRepository.SaveAsync();

        return true;
    }

    public async Task<TopicPaymentResultDto> GetByIdAsync(long id)
    {
        var existPayment = await _paymentRepository.SelectAsync(payment => payment.Id.Equals(id),
            includes: new[] { "Topic", "Education" })
            ?? throw new RevisionException(404, "This topic payment is not found");

        return _mapper.Map<TopicPaymentResultDto>(existPayment);
    }

    public async Task<IEnumerable<TopicPaymentResultDto>> GetByEducationIdAsync(long educationId)
    {
        var existPayments = await _paymentRepository.SelectAll(payment => payment.EducationId.Equals(educationId))
            .ToListAsync();
        if (!existPayments.Any())
            throw new RevisionException(404, "This education is not found");

        return _mapper.Map<IEnumerable<TopicPaymentResultDto>>(existPayments);
    }

    public async Task<IEnumerable<TopicPaymentResultDto>> GetAllAsync(PaginationParams pagination, string search = null)
    {
        var topicPayments = _paymentRepository.SelectAll(includes: new[] { "Topic", "Education" });

        if (!string.IsNullOrEmpty(search))
        {
            topicPayments = topicPayments.Where(payment => 
            payment.Topic.Name.ToLower().Equals(search.ToLower()) || 
            payment.Education.Name.ToLower().Contains(search.ToLower()));
        }

        var result = topicPayments.ToPagedList(pagination);
        return _mapper.Map<IEnumerable<TopicPaymentResultDto>>(result);
    }
}