using AutoMapper;
using Revision.Domain.Entities.Addresses;
using Revision.Domain.Entities.Categories;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Payments;
using Revision.Domain.Entities.Subjects;
using Revision.Domain.Entities.Topics;
using Revision.Domain.Entities.Users;
using Revision.Service.DTOs.Addresses;
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

        //Addresses
        CreateMap<Address, AddressResultDto>().ReverseMap();
        CreateMap<Address, AddressUpdateDto>().ReverseMap();
        CreateMap<Address, AddressCreationDto>().ReverseMap();

        //Countries
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
        CreateMap<District, DistrictResultDto>().ReverseMap();
        CreateMap<District, DistrictCreationDto>().ReverseMap();

        //EducationCategories
        CreateMap<EducationCategory, EducationCategoryResultDto>().ReverseMap();
        CreateMap<EducationCategory, EducationCategoryUpdateDto>().ReverseMap();
        CreateMap<EducationCategory, EducationCategoryCreationDto>().ReverseMap();

        //Educations
        CreateMap<Education, EducationResultDto>().ReverseMap();
        CreateMap<Education, EducationUpdateDto>().ReverseMap();
        CreateMap<Education, EducationCreationDto>().ReverseMap();

        //Messages
        //CreateMap<Message, MessageResultDto>().ReverseMap();
        //CreateMap<Message, MessageUpdateDto>().ReverseMap();
        //CreateMap<Message, MessageCreationDto>().ReverseMap();

        //Regions
        CreateMap<Region, RegionResultDto>().ReverseMap();
        CreateMap<Region, RegionCreationDto>().ReverseMap();

        //Rooms
        //CreateMap<Room, RoomResultDto>().ReverseMap();
        //CreateMap<Room, RoomUpdateDto>().ReverseMap();
        //CreateMap<Room, RoomCreationDto>().ReverseMap();

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

        //Topics
        CreateMap<Topic, TopicResultDto>().ReverseMap();
        CreateMap<Topic, TopicUpdateDto>().ReverseMap();
        CreateMap<Topic, TopicCreationDto>().ReverseMap();
    }
}
