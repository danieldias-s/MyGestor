namespace MyGestor.Application.Dtos;

public class PedidoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal Total { get; set; }

    public List<PedidoItemDto> Itens { get; set; }
}
public class PedidoItemDto
{
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }  
    public decimal TotalItem { get; set; }     
}
