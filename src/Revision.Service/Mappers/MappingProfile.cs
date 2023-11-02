using AutoMapper;
using Revision.Domain.Entities.Addresses;
using Revision.Domain.Entities.Assets;
using Revision.Domain.Entities.Chats;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Payments;
using Revision.Domain.Entities.Subjects;
using Revision.Domain.Entities.Topics;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Models;
using Revision.Service.DTOs.Addresses;
using Revision.Service.DTOs.Assets;
using Revision.Service.DTOs.ChatRooms;
using Revision.Service.DTOs.Chats;
using Revision.Service.DTOs.Countries;
using Revision.Service.DTOs.DevicePayments;
using Revision.Service.DTOs.Devices;
using Revision.Service.DTOs.Districts;
using Revision.Service.DTOs.EducationCategories;
using Revision.Service.DTOs.Educations;
using Revision.Service.DTOs.Regions;
using Revision.Service.DTOs.SubjectCategories;
using Revision.Service.DTOs.Subjects;
using Revision.Service.DTOs.TopicPayments;
using Revision.Service.DTOs.Topics;
using Revision.Service.DTOs.UserEducations;
using Revision.Service.DTOs.Users;

namespace Revision.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Users
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<User, UserCreationDto>().ReverseMap();

        //Assets
        CreateMap<Asset, AssetResultDto>().ReverseMap();
        CreateMap<Asset, AssetCreationDto>().ReverseMap();

        //ChatRooms
        CreateMap<ChatRoom, ChatRoomUpdateDto>().ReverseMap();
        CreateMap<ChatRoom, ChatRoomResultDto>().ReverseMap();
        CreateMap<ChatRoom, ChatRoomCreationDto>().ReverseMap();

        //Chat
        CreateMap<Chat, ChatUpdateDto>().ReverseMap();
        CreateMap<Chat, ChatResultDto>().ReverseMap();
        CreateMap<Chat, ChatCreationDto>().ReverseMap();

        //Addresses
        CreateMap<Address, AddressResultDto>().ReverseMap();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();
        CreateMap<Address, AddressCreationDto>().ReverseMap();

        //Countries
        CreateMap<Country, CountryModel>().ReverseMap();
        CreateMap<Country, CountryResultDto>().ReverseMap();
        CreateMap<Country, CountryCreationDto>().ReverseMap();

        //DevicePayments
        CreateMap<DevicePayment, DevicePaymentResultDto>().ReverseMap();
        CreateMap<DevicePayment, DevicePaymentCreationDto>().ReverseMap();

        //Devices
        CreateMap<Device, DeviceResultDto>().ReverseMap();
        CreateMap<Device, DeviceUpdateDto>().ReverseMap();
        CreateMap<Device, DeviceCreationDto>().ReverseMap();

        //Districts
        CreateMap<District, DistrictModel>().ReverseMap();
        CreateMap<District, DistrictResultDto>().ReverseMap();
        CreateMap<District, DistrictCreationDto>().ReverseMap();

        //EducationCategories
        CreateMap<EducationCategory, EducationCategoryResultDto>().ReverseMap();
        CreateMap<EducationCategory, EducationCategoryUpdateDto>().ReverseMap();
        CreateMap<EducationCategory, EducationCategoryCreationDto>().ReverseMap();

        //UserEducations
        CreateMap<UserEducation, UserEducationResultDto>().ReverseMap();
        CreateMap<UserEducation, UserEducationUpdateDto>().ReverseMap();
        CreateMap<UserEducation, UserEducationCreationDto>().ReverseMap();

        //Educations
        CreateMap<Education, EducationResultDto>().ReverseMap();
        CreateMap<Education, EducationUpdateDto>().ReverseMap();
        CreateMap<Education, EducationCreationDto>().ReverseMap();

        //Regions
        CreateMap<Region, RegionModel>().ReverseMap();
        CreateMap<Region, RegionResultDto>().ReverseMap();
        CreateMap<Region, RegionCreationDto>().ReverseMap();

        //SubjectCategories
        CreateMap<SubjectCategory, SubjectCategoryResultDto>().ReverseMap();
        CreateMap<SubjectCategory, SubjectCategoryUpdateDto>().ReverseMap();
        CreateMap<SubjectCategory, SubjectCategoryCreationDto>().ReverseMap();

        //Subjects
        CreateMap<Subject, SubjectResultDto>().ReverseMap();
        CreateMap<Subject, SubjectUpdateDto>().ReverseMap();
        CreateMap<Subject, SubjectCreationDto>().ReverseMap();

        //TopicPayments
        CreateMap<TopicPayment, TopicPaymentResultDto>().ReverseMap();
        CreateMap<TopicPayment, TopicPaymentCreationDto>().ReverseMap();

        //Topics
        CreateMap<Topic, TopicResultDto>().ReverseMap();
        CreateMap<Topic, TopicUpdateDto>().ReverseMap();
        CreateMap<Topic, TopicCreationDto>().ReverseMap();
    }
}
