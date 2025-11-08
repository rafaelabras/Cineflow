using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.IClienteRepository;
using Cineflow.models.cinema;
using Dapper;

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

    public async Task<int> PutFilmeAsync(Filme filmeCreate)
    {
        
        var sql = @"UPDATE filme nome_filme =  @nome_filme, sinopse = @sinopse, duracao = @duracao,  classificacao_etaria = @classificacao_indicativa,
        idioma = @idioma, pais_origem = @pais_origem, produtora = @produtora,data_lancamento = @data_lancamento, diretor = @diretor WHERE id = @id";
        
        return await _databaseService.ExecuteAsync(sql, filmeCreate);
    }

    public async Task<bool> DeleteFilmeAsync(int ID)
    {
        var sql = @"DELETE FROM filme WHERE id = @id";

        var execute = await _databaseService.ExecuteAsync(sql, ID);

        if (execute == 1)
        {
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Filme>> GetFilmesAsync(FilmeFiltroDto filtro)
    {
        var sb = new StringBuilder();
        
        var sql = @"SELECT id, nome_filme, sinopse, genero, duracao, classificacao_etaria, idioma
        ,pais_origem, produtor, data_lancamento, diretor FROM filme WHERE 1=1";

        var parameters = new DynamicParameters();

        sb.Append(sql);
        
        montarFiltro(filtro, sb, parameters);
        
        return await _databaseService.QueryAsync<Filme>(sb.ToString(), parameters);
    }
    
    
    public static void montarFiltro(FilmeFiltroDto filtro, StringBuilder sb, DynamicParameters parameters)
    {
        if (filtro.id != null)
            sb.Append(" AND  id = @id");
            parameters.Add("id", filtro.id);
            
        if (!string.IsNullOrWhiteSpace(filtro.classificacao_indicativa))
            sb.Append(" AND classificacao_etaria = @classificacao_indicativa ");
            parameters.Add("classificacao_etaria", filtro.classificacao_indicativa);
        
        if (!string.IsNullOrWhiteSpace(filtro.idioma.ToString()))
            sb.Append(" AND idioma = @idioma ");
            parameters.Add("idioma", filtro.idioma);
        
        if (!string.IsNullOrWhiteSpace(filtro.pais_origem))
            sb.Append(" AND pais_origem = @pais_origem ");
            parameters.Add("pais_origem", filtro.pais_origem);
        
        if (!string.IsNullOrWhiteSpace(filtro.produtora))
            sb.Append(" AND produtor = @produtora ");
            parameters.Add("produtor", filtro.produtora);
        
        if (filtro.data_lancamento != null)
            sb.Append(" AND data_lancamento = @data_lancamento ");
            parameters.Add("data_lancamento", filtro.data_lancamento);
        
        if (!string.IsNullOrWhiteSpace(filtro.diretor))
            sb.Append(" AND diretor = @diretor ");
            parameters.Add("diretor", filtro.diretor);
        
        if (!string.IsNullOrWhiteSpace(filtro.nome_filme))
            sb.Append(" AND nome_filme = @nome_filme ");
            parameters.Add("nome_filme", filtro.nome_filme);
        
        if (!string.IsNullOrWhiteSpace(filtro.pais_origem))
            sb.Append(" AND pais_origem = @pais_origem ");
            parameters.Add("pais_origem", filtro.pais_origem);
        
        if(!string.IsNullOrWhiteSpace(filtro.sinopse))
            sb.Append(" AND sinopse = @sinopse ");
            parameters.Add("sinopse", filtro.sinopse);  
        
        if (!string.IsNullOrWhiteSpace(filtro.genero))
            sb.Append(" AND genero = @genero ");
            parameters.Add("genero", filtro.genero);

            if (filtro.duracao != null)
            {
                sb.Append(" AND duracao = @duracao ");
                parameters.Add("duracao", filtro.duracao);
            }
    }
}