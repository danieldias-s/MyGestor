using AutoMapper;
using MyGestor.Application.DTOs.Produto;
using MyGestor.Domain.Entities;

namespace MyGestor.Application.Mappings;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoDto>().ReverseMap();
        CreateMap<CreateProdutoDto, Produto>();
    }
}
