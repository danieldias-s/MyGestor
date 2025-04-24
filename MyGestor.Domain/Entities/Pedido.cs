using System.Text.Json.Serialization;

namespace MyGestor.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.Now;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }

    public ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();

    public decimal Total { get; set; }
}
