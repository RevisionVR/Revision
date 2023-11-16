using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Chats;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.ChatRooms;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Chats;

namespace Revision.Service.Services.Chats;

public class ChatRoomService : IChatRoomService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<ChatRoom> _chatRoomRepository;
    private readonly IRepository<Education> _educationRepository;
    public ChatRoomService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<ChatRoom> chatRoomRepository,
        IRepository<Education> educationRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _chatRoomRepository = chatRoomRepository;
        _educationRepository = educationRepository;
    }

    public async Task<ChatRoomResultDto> CreateAsync(ChatRoomCreationDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(dto.UserId))
            ?? throw new RevisionException(404, "This user is not found");
        var existEducation = await _educationRepository.SelectAsync(education => education.Id.Equals(dto.EducationId))
            ?? throw new RevisionException(404, "This education is not found");

        var mappedRoom = _mapper.Map<ChatRoom>(dto);
        if (string.IsNullOrEmpty(dto.Name))
            mappedRoom.Name = existEducation.Name;

        mappedRoom.CreatedAt = TimeHelper.GetDateTime();
        mappedRoom.User = existUser;
        mappedRoom.Education = existEducation;

        await _chatRoomRepository.AddAsync(mappedRoom);
        await _chatRoomRepository.SaveAsync();

        return _mapper.Map<ChatRoomResultDto>(mappedRoom);
    }

    public async Task<bool> DestroyAsync(long id)
    {
        var existRoom = await _chatRoomRepository.SelectAsync(room => room.Id.Equals(id),
            includes: new[] { "User", "Education" })
            ?? throw new RevisionException(404, "This room is not found");

        _chatRoomRepository.Delete(existRoom);
        await _chatRoomRepository.SaveAsync();
        return true;
    }

    public async Task<IEnumerable<ChatRoomResultDto>> GetByUserIdAsync(long userId)
    {
        var existRoom = await _chatRoomRepository.SelectAll(room => room.UserId.Equals(userId),
            includes: new[] { "User", "Education", "Chats" }).ToListAsync();

        if (!existRoom.Any())
            throw new RevisionException(404, "This room is not found");

        return _mapper.Map<IEnumerable<ChatRoomResultDto>>(existRoom);
    }

    public async Task<IEnumerable<ChatRoomResultDto>> GetByEducationIdAsync(long educationId)
    {
        var existRoom = await _chatRoomRepository.SelectAll(room => room.EducationId.Equals(educationId),
            includes: new[] { "User", "Education", "Chats" }).ToListAsync();

        if (!existRoom.Any())
            throw new RevisionException(404, "This room is not found");

        return _mapper.Map<IEnumerable<ChatRoomResultDto>>(existRoom);
    }
}