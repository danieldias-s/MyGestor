using AutoMapper;
using MyGestor.Application.DTOs.Cliente;
using MyGestor.Domain.Entities;

namespace MyGestor.Application.Mappings;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteDto>().ReverseMap();
        CreateMap<CreateClienteDto, Cliente>();
    }
}
