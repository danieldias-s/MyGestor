using AutoMapper;
using MyGestor.Application.DTOs;
using MyGestor.Application.DTOs.Auth;
using MyGestor.Domain.Entities;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.SenhaHash, opt => opt.Ignore()); 

        CreateMap<RegisterDto, User>()
    .ForMember(dest => dest.SenhaHash, opt => opt.Ignore()) 
    .ForMember(dest => dest.Role, opt => opt.Ignore());     



    }
}
