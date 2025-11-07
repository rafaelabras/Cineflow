using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.IClienteRepository;
using Cineflow.models.cinema;

namespace Cineflow.repository;

public class FilmeRepositoy : IFilmeRepository
{
    private readonly DatabaseService _databaseService;

    public FilmeRepositoy(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public Task<int> AddFilmeAsync(CriarFilmeDto filme)
    {
        var sql = @"INSERT INTO filme(nome_filme, sinopse, genero, duracao, classificacao_etaria, idioma
        , pais_origem, produtor, data_lancamento, diretor)
        VALUES (@nome_filme, @sinopse, @genero,@duracao, @classificacao_indicativa,
                @idioma, @pais_origem, @produtora, @data_lancamento, @diretor)";
        
        return _databaseService.ExecuteAsync(sql, filme);
    }

    public Task<Result<IEnumerable<Filme>>> GetFilmesAsync()
    {
        throw new NotImplementedException();
    }
}