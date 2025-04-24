namespace MyGestor.Application.Dtos
{
    public class PedidoRelatorioFiltroDto
    {
        public string? NomeCliente { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}