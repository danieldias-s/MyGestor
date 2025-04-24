namespace MyGestor.Application.Dtos;

public class CriarPedidoDto
{
    public int ClienteId { get; set; }
    public List<CriarPedidoItemDto> Itens { get; set; } = new();
}