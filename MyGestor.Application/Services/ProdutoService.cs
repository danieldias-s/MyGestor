using AutoMapper;
using MyGestor.Application.DTOs.Produto;
using MyGestor.Application.Interfaces;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;

namespace MyGestor.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repo;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
    {
        var produtos = await _repo.ObterTodosAsync();
        return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
    }

    public async Task<ProdutoDto?> ObterPorIdAsync(int id)
    {
        var produto = await _repo.ObterPorIdAsync(id);
        return produto == null ? null : _mapper.Map<ProdutoDto>(produto);
    }

    public async Task AdicionarAsync(CreateProdutoDto dto)
    {
        var produto = _mapper.Map<Produto>(dto);
        await _repo.AdicionarAsync(produto);
    }

    public async Task AtualizarAsync(int id, CreateProdutoDto dto)
    {
        var produto = await _repo.ObterPorIdAsync(id);
        if (produto == null) throw new Exception("Produto não encontrado");

        _mapper.Map(dto, produto);
        await _repo.AtualizarAsync(produto);
    }

    public async Task RemoverAsync(int id)
    {
        var produto = await _repo.ObterPorIdAsync(id);
        if (produto == null) throw new Exception("Produto não encontrado");

        await _repo.RemoverAsync(produto);
    }
}
