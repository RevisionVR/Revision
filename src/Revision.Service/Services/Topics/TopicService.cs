﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Subjects;
using Revision.Domain.Entities.Topics;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Topics;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Topics;

namespace Revision.Service.Services.Topics;

public class TopicService : ITopicService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Subject> _subjectRepository;
    public TopicService(
        IMapper mapper,
        IRepository<Topic> topicRepository,
        IRepository<Subject> subjectRepository)
    {
        _mapper = mapper;
        _topicRepository = topicRepository;
        _subjectRepository = subjectRepository;
    }

    public async Task<TopicResultDto> CreateAsync(TopicCreationDto dto)
    {
        var existSubject = await _subjectRepository.SelectAsync(subject => subject.Id.Equals(dto.SubjectId))
            ?? throw new RevisionException(404, "This subject is not found");

        var mappedTopic = _mapper.Map<Topic>(dto);
        mappedTopic.Subject = existSubject;
        mappedTopic.CreatedAt = TimeHelper.GetDateTime();

        await _topicRepository.AddAsync(mappedTopic);
        await _topicRepository.SaveAsync();

        return _mapper.Map<TopicResultDto>(mappedTopic);
    }

    public async Task<TopicResultDto> UpdateAsync(long id, TopicUpdateDto dto)
    {
        var existTopic = await _topicRepository.SelectAsync(topic => topic.Id.Equals(id))
            ?? throw new RevisionException(404, "This topic is not found");

        var existSubject = await _subjectRepository.SelectAsync(subject => subject.Id.Equals(dto.SubjectId))
            ?? throw new RevisionException(404, "This subject is not found");

        var mappedTopic = _mapper.Map(dto, existTopic);
        mappedTopic.UpdatedAt = TimeHelper.GetDateTime();
        mappedTopic.Id = id;
        mappedTopic.Subject = existSubject;

        _topicRepository.Update(mappedTopic);
        await _topicRepository.SaveAsync();

        return _mapper.Map<TopicResultDto>(mappedTopic);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existTopic = await _topicRepository.SelectAsync(topic => topic.Id.Equals(id))
            ?? throw new RevisionException(404, "This topic is not found");

        _topicRepository.Delete(existTopic);
        await _topicRepository.SaveAsync();
        return true;
    }

    public async Task<bool> DestroyAsync(long id)
    {
        var existTopic = await _topicRepository.SelectAsync(topic => topic.Id.Equals(id))
            ?? throw new RevisionException(404, "This topic is not found");

        _topicRepository.Destroy(existTopic);
        await _topicRepository.SaveAsync();
        return true;
    }

    public async Task<TopicResultDto> GetByIdAsync(long id)
    {
        var existTopic = await _topicRepository.SelectAsync(topic => topic.Id.Equals(id),
            includes: new[] { "Subject.SubjectCategory", "TopicPayments" })
            ?? throw new RevisionException(404, "This topic is not found");

        return _mapper.Map<TopicResultDto>(existTopic);
    }

    public async Task<IEnumerable<TopicResultDto>> GetAllAsync(PaginationParams pagination, string search = null)
    {
        var topics = _topicRepository.SelectAll(includes: new[] { "Subject.SubjectCategory", "TopicPayments" });
        if (!string.IsNullOrEmpty(search))
        {
            topics = topics.Where(topic =>
            topic.Name.ToLower().Contains(search.ToLower()));
        }

        var result = topics.ToPagedList(pagination);
        return _mapper.Map<IEnumerable<TopicResultDto>>(result);
    }

    public async Task<IEnumerable<TopicResultDto>> GetAllAsync()
    {
        var topics = await _topicRepository.SelectAll(
            includes: new[] { "Subject.SubjectCategory", "TopicPayments" })
            .ToListAsync();
        return _mapper.Map<IEnumerable<TopicResultDto>>(topics);
    }
}