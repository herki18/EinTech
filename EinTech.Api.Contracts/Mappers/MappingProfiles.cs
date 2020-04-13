using AutoMapper;
using EinTech.Api.Contracts.Entities;
using EinTech.Api.Contracts.Models;

namespace EinTech.Api.Contracts.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PersonEntity, PersonModel>(MemberList.None)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ReverseMap();

            CreateMap<GroupEntity, GroupModel>(MemberList.None)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<CreatePersonModel, PersonEntity>(MemberList.None)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId));
        }
    }
}