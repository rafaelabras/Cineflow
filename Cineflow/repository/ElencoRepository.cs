using System.Text;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Dapper;

namespace Cineflow.repository;

public class ElencoRepository : IElencoRepository
{
    private readonly DatabaseService _databaseService;
    public ElencoRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    public async Task<IEnumerable<Elenco>> GetElencoAsync(ElencoFiltroDto filtro)
    {
        var sb = new StringBuilder();
        var parameters = new DynamicParameters();
        
        var sql = @"SELECT id, nome, genero, data_nascimento, nacionalidade FROM elenco WHERE 1 = 1";
        sb.AppendLine(sql);
        AdicionarFiltrosQuery(filtro, sb, parameters);

        return await _databaseService.QueryAsync<Elenco>(sb.ToString(), parameters);
    }

    public async Task<bool> DeleteElencoAsync(int ID)
    {
        var sql = @"DELETE FROM elenco WHERE id = @id";
        
        var command = await _databaseService.ExecuteAsync(sql, new { id = ID });

        if (command == 1)
            return true;
        
        return false;
    }

    public async Task<int> AddElencoAsync(CriarElencoDto dto)
    {
        var sql = @"INSERT INTO elenco(nome, genero, data_nascimento, nacionalidade)
                  VALUES (@nome, @genero, @data_nascimento, @nacionalidade)";
        
        return await _databaseService.ExecuteAsync(sql, dto);
    }

    public async Task<int> PutElencoAsync(Elenco elenco)
    {
        var sql = @"UPDATE elenco SET nome = @nome, genero = @genero,
                  data_nascimento = @data_nascimento, nacionalidade = @nacionalidade WHERE id = @id";
        
        return await _databaseService.ExecuteAsync(sql, elenco);
    }

    private void AdicionarFiltrosQuery(ElencoFiltroDto filtro, StringBuilder sb, DynamicParameters parameters)
    {
        if (filtro.Id != null)
            sb.AppendLine(@" AND id = @id ");
            parameters.Add("id", filtro.Id);
            
        if (filtro.nome != null)
            sb.AppendLine(@" AND nome = @nome ");
            parameters.Add("nome", filtro.nome);
            
        if (filtro.genero != null)
            sb.AppendLine(@" AND genero = @genero ");
            parameters.Add("genero", filtro.genero);
            
        if (filtro.data_nascimento != null)
            sb.AppendLine(@" AND data_nascimento = @data_nascimento ");
            parameters.Add("data_nascimento", filtro.data_nascimento);
            
        if (filtro.nacionalidade != null)
            sb.AppendLine(@" AND nacionalidade = @nacionalidade ");
            parameters.Add("nacionalidade", filtro.nacionalidade);
    }
    
}