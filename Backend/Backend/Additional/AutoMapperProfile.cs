using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Additional;

public class AutoMapperProfile :Profile
{
    public AutoMapperProfile()
    {
        /*
        CreateMap<User, UserCreateDto>().ReverseMap()
            .ForMember(dest=> dest.Password, opt=>opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))//Encrypts the password
            ;

         */
       
        CreateMap<PartyListAddDto, PartyList>().ReverseMap();
        
        CreateMap<RegisteredPartyListCandidate, PartyListCandidateAddDto>().ReverseMap();


        CreateMap<PartyListGetDto, PartyList>().ReverseMap()
            .ForMember(dest => dest.FirstOnList, opt => opt.MapFrom(src => src.RegisteredCandidates.FirstOrDefault(k=>k.RankInList == 1)!.Name ));
        
        
        CreateMap<SingleMemberCandidatesGetDto, SingleMemberCandidate>().ReverseMap();
        
        CreateMap<SingleMemberCandidateAddDto, SingleMemberCandidate>().ReverseMap();
    }
}