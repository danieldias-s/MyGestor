using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGestor.Application.DTOs;

public class UpdatePedidoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }

    public List<UpdatePedidoItemDto> Itens { get; set; } = new();
}