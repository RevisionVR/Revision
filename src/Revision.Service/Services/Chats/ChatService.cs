﻿using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Chats;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Assets;
using Revision.Service.DTOs.Chats;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Assets;
using Revision.Service.Interfaces.Chats;

namespace Revision.Service.Services.Chats;

public class ChatService : IChatService
{
    private readonly IMapper _mapper;
    private readonly IAssetService _assetService;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Chat> _chatRepository;
    private readonly IRepository<ChatRoom> _chatRoomRepository;
    public ChatService(
        IMapper mapper,
        IAssetService assetService,
        IRepository<User> userRepository,
        IRepository<Chat> chatRepository,
        IRepository<ChatRoom> chatRoomRepository)
    {
        _mapper = mapper;
        _assetService = assetService;
        _userRepository = userRepository;
        _chatRepository = chatRepository;
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<ChatResultDto> CreateAsync(ChatCreationDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(dto.UserId))
            ?? throw new RevisionException(404, "This user is not found");

        var existRoom = await _chatRoomRepository.SelectAsync(room => room.Id.Equals(dto.ChatRoomId))
            ?? throw new RevisionException(404, "This chat room is not found");

        var mappedChat = _mapper.Map<Chat>(dto);
        if (dto.FormFile is not null)
        {
            var asset = await _assetService.UploadAsync(new AssetCreationDto { FormFile = dto.FormFile });
            mappedChat.AssetId = asset.Id;
            mappedChat.Asset = asset;
        }

        mappedChat.User = existUser;
        mappedChat.ChatRoom = existRoom;
        mappedChat.CreatedAt = TimeHelper.GetDateTime();
        await _chatRepository.AddAsync(mappedChat);
        await _chatRepository.SaveAsync();

        return _mapper.Map<ChatResultDto>(mappedChat);
    }

    public async Task<ChatResultDto> UpdateAsync(long id, ChatUpdateDto dto)
    {
        var existChat = await _chatRepository.SelectAsync(chat => chat.Id.Equals(id))
            ?? throw new RevisionException(404, "This chat is not found");

        var existRoom = await _chatRoomRepository.SelectAsync(room => room.Id.Equals(dto.ChatRoomId))
            ?? throw new RevisionException(404, "This chat room is not found");

        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(dto.UserId))
            ?? throw new RevisionException(404, "This user is not found");

        var mappedChat = _mapper.Map(dto, existChat);
        mappedChat.Id = id;
        if (dto.FormFile is not null)
        {
            var asset = await _assetService.UploadAsync(new AssetCreationDto { FormFile = dto.FormFile });
            mappedChat.AssetId = asset.Id;
            mappedChat.Asset = asset;
        }

        mappedChat.ChatRoom = existRoom;
        mappedChat.User = existUser;
        mappedChat.UpdatedAt = TimeHelper.GetDateTime();
        _chatRepository.Update(mappedChat);
        await _chatRepository.SaveAsync();

        return _mapper.Map<ChatResultDto>(mappedChat);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existChat = await _chatRepository.SelectAsync(chat => chat.Id.Equals(id))
            ?? throw new RevisionException(404, "This chat is not found");

        _chatRepository.Delete(existChat);
        await _chatRepository.SaveAsync();
        return true;
    }

    public async Task<ChatResultDto> GetByIdAsync(long id)
    {
        var existChat = await _chatRepository.SelectAsync(chat => chat.Id.Equals(id), 
            includes: new[] { "ChatRoom", "User", "Asset" })

            ?? throw new RevisionException(404, "This chat is not found");

        return _mapper.Map<ChatResultDto>(existChat);
    }

    public async Task<IEnumerable<ChatResultDto>> GetByRoomIdAsync(long roomId)
    {
        var chats = _chatRepository.SelectAll(chat => chat.ChatRoomId.Equals(roomId),
            includes: new[] { "ChatRoom", "User", "Asset" });

        return _mapper.Map<IEnumerable<ChatResultDto>>(chats);
    }

    public async Task<IEnumerable<ChatResultDto>> GetByUserIdAsync(long userId)
    {
        var chats = _chatRepository.SelectAll(chat => chat.UserId.Equals(userId),
            includes: new[] { "ChatRoom", "User", "Asset" });

        return _mapper.Map<IEnumerable<ChatResultDto>>(chats);
    }
}
