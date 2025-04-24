using AutoMapper;
using MyGestor.Application.DTOs.Cliente;
using MyGestor.Application.Interfaces;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;

namespace MyGestor.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClienteDto>> ObterTodosAsync()
    {
        var clientes = await _repo.ObterTodosAsync();
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto?> ObterPorIdAsync(int id)
    {
        var cliente = await _repo.ObterPorIdAsync(id);
        return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
    }

    public async Task AdicionarAsync(CreateClienteDto dto)
    {
        var cliente = _mapper.Map<Cliente>(dto);
        await _repo.AdicionarAsync(cliente);
    }

    public async Task AtualizarAsync(int id, CreateClienteDto dto)
    {
        var cliente = await _repo.ObterPorIdAsync(id);
        if (cliente == null) throw new Exception("Cliente não encontrado");

        _mapper.Map(dto, cliente);
        await _repo.AtualizarAsync(cliente);
    }

    public async Task RemoverAsync(int id)
    {
        var cliente = await _repo.ObterPorIdAsync(id);
        if (cliente == null) throw new Exception("Cliente não encontrado");

        await _repo.RemoverAsync(cliente);
    }
}
