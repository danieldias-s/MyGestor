using AutoMapper;
using MyGestor.Application.Dtos;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;

namespace MyGestor.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public PedidoService(
        IPedidoRepository pedidoRepository,
        IProdutoRepository produtoRepository,
        IClienteRepository clienteRepository,
        IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PedidoDto>> ListarAsync()
    {
        var pedidos = await _pedidoRepository.ListarAsync();
        var pedidosDto = new List<PedidoDto>();

        foreach (var pedido in pedidos)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(pedido.ClienteId);
            var pedidoDto = _mapper.Map<PedidoDto>(pedido);

            pedidoDto.ClienteNome = cliente?.Nome ?? "Cliente não encontrado";
            pedidoDto.Total = pedido.Total;


            pedidosDto.Add(pedidoDto);
        }

        return pedidosDto;
    }

    public async Task<PedidoDto?> ObterPorIdAsync(int id)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(id);
        if (pedido == null) return null;

        var cliente = await _clienteRepository.ObterPorIdAsync(pedido.ClienteId);
        var pedidoDto = _mapper.Map<PedidoDto>(pedido);

        pedidoDto.ClienteNome = cliente?.Nome ?? "Cliente não encontrado";
        pedidoDto.Total = pedido.Itens.Sum(i => i.Produto?.Preco * i.Quantidade ?? 0);

        return pedidoDto;
    }

    public async Task CriarAsync(CriarPedidoDto dto)
    {
        if (!await _clienteRepository.ExisteAsync(dto.ClienteId))
            throw new Exception("Cliente não encontrado.");

        var itens = new List<PedidoItem>();

        foreach (var itemDto in dto.Itens)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(itemDto.ProdutoId)
                          ?? throw new Exception("Produto não encontrado");

            itens.Add(new PedidoItem
            {
                ProdutoId = itemDto.ProdutoId,
                Quantidade = itemDto.Quantidade,
                PrecoUnitario = produto.Preco 
            });
        }

        var pedido = new Pedido
        {
            ClienteId = dto.ClienteId,
            DataPedido = DateTime.Now,
            Itens = new List<PedidoItem>(),
            Total = itens.Sum(i => i.Quantidade * i.PrecoUnitario)
        };

        foreach (var itemDto in dto.Itens)
        {
            if (!await _produtoRepository.ExisteAsync(itemDto.ProdutoId))
                throw new Exception($"Produto {itemDto.ProdutoId} não encontrado.");

            pedido.Itens.Add(new PedidoItem
            {
                ProdutoId = itemDto.ProdutoId,
                Quantidade = itemDto.Quantidade
            });
        }

        await _pedidoRepository.AdicionarAsync(pedido);
    }

    public async Task AtualizarAsync(int id, CriarPedidoDto dto)
    {
        var pedidoExistente = await _pedidoRepository.ObterPorIdAsync(id);
        if (pedidoExistente == null) throw new Exception("Pedido não encontrado.");

        if (!await _clienteRepository.ExisteAsync(dto.ClienteId))
            throw new Exception("Cliente inválido.");

        pedidoExistente.ClienteId = dto.ClienteId;
        pedidoExistente.Itens.Clear();

        foreach (var itemDto in dto.Itens)
        {
            if (!await _produtoRepository.ExisteAsync(itemDto.ProdutoId))
                throw new Exception($"Produto {itemDto.ProdutoId} inválido.");

            pedidoExistente.Itens.Add(new PedidoItem
            {
                ProdutoId = itemDto.ProdutoId,
                Quantidade = itemDto.Quantidade
            });
        }

        await _pedidoRepository.AtualizarAsync(pedidoExistente);
    }

    public async Task RemoverAsync(int id)
    {
        var pedido = await _pedidoRepository.ObterPorIdAsync(id);
        if (pedido == null) throw new Exception("Pedido não encontrado.");

        await _pedidoRepository.RemoverAsync(pedido);
    }

    public async Task<IEnumerable<PedidoDto>> BuscarRelatorioAsync(PedidoRelatorioFiltroDto filtro)
    {
        var pedidos = await _pedidoRepository.BuscarPedidosComFiltroAsync(filtro.Ano, filtro.Mes);

        return pedidos.Select(p => new PedidoDto
        {
            Id = p.Id,
            DataPedido = p.DataPedido,
            ClienteId = p.ClienteId,
            ClienteNome = p.Cliente?.Nome ?? string.Empty,
            Total = p.Total,
            Itens = p.Itens.Select(i => new PedidoItemDto
            {
                ProdutoId = i.ProdutoId,
                ProdutoNome = i.Produto?.Nome ?? string.Empty,
                Quantidade = i.Quantidade,
                PrecoUnitario = i.PrecoUnitario,
                TotalItem = i.PrecoUnitario * i.Quantidade
            }).ToList()
        }).ToList();
    }



}