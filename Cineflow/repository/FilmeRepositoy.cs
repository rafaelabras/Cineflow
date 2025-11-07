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

    public async Task<int> AddFilmeAsync(CriarFilmeDto filme)
    {
        var sql = @"INSERT INTO filme(nome_filme, sinopse, genero, duracao, classificacao_etaria, idioma
        , pais_origem, produtor, data_lancamento, diretor)
        VALUES (@nome_filme, @sinopse, @genero,@duracao, @classificacao_indicativa,
                @idioma, @pais_origem, @produtora, @data_lancamento, @diretor)";
        
        var executar = await _databaseService.ExecuteAsync(sql, filme);

        return executar;
    }

    public async Task<IEnumerable<Filme>> GetFilmesAsync()
    {
        var sql = @"SELECT id, nome_filme, sinopse, genero, duracao, classificacao_etaria, idioma
        ,pais_origem, produtor, data_lancamento, diretor FROM filme";

        return await _databaseService.QueryAsync<Filme>(sql);
    }
}