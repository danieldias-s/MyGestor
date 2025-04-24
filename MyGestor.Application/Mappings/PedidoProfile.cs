using AutoMapper;
using MyGestor.Domain.Entities;
using MyGestor.Application.Dtos;

namespace MyGestor.Application.Mapping;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        // Mapeamento de Pedido para PedidoDto
        CreateMap<Pedido, PedidoDto>()
            .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente.Nome))
            .ForMember(dest => dest.DataPedido, opt => opt.MapFrom(src => src.DataPedido))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total)) // Usa o valor do banco
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));

        // Mapeamento de PedidoItem para PedidoItemDto
        CreateMap<PedidoItem, PedidoItemDto>()
            .ForMember(dest => dest.ProdutoNome, opt => opt.MapFrom(src => src.Produto.Nome))
            .ForMember(dest => dest.PrecoUnitario, opt => opt.MapFrom(src => src.Produto.Preco))
            .ForMember(dest => dest.TotalItem, opt => opt.MapFrom(src =>
                src.Produto.Preco * src.Quantidade));

        // Mapeamento para criação de pedido
        CreateMap<CriarPedidoDto, Pedido>()
            .ForMember(dest => dest.DataPedido, opt => opt.MapFrom(_ => DateTime.Now))
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.Itens));

        // Mapeamento para itens do pedido
        CreateMap<CriarPedidoItemDto, PedidoItem>();
    }
}