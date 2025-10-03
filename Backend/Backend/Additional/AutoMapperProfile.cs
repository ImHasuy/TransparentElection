using AutoMapper;

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

       
    }
}