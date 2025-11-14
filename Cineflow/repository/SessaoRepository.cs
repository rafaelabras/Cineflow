using System.Text;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Dapper;

namespace Cineflow.repository;

public class SessaoRepository : ISessaoRepository
{
    private readonly DatabaseService _databaseService;
    
    public SessaoRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    public async Task<IEnumerable<Sessão>> GetSessaoAsync(SessaoFiltroDto filtro)
    {
        var sb = new StringBuilder();
        
        var sql = @"
            SELECT 
                s.id AS id_sessao, s.horario_inicio, s.horario_fim, s.preco_sessao, s.idioma_audio, s.idioma_legenda,
                f.id AS id_filme, f.nome_filme, f.sinopse, f.genero, f.duracao, f.classificacao_etaria, f.idioma, f.pais_origem,
                f.produtor, f.data_lancamento, f.diretor,
                sa.id AS id_sala, sa.tipo_sala, sa.capacidade, sa.ocupado
            FROM sessao s
            JOIN filme f ON f.id = s.filme_id
            JOIN sala sa ON sa.id = s.sala_id
            WHERE 1=1
    ";

        sb.Append(sql);
        var parameters = new DynamicParameters();
        MontarFiltroSessao(filtro, sb, parameters);

        var sessao = await _databaseService.QueryAsyncMultipleObjectsThreeJoins<Sessão, Filme, Sala, Sessão>(sb.ToString(),(
            (sessão, filme, sala) =>
            {
                sessão.filme = filme;
                sessão.sala = sala;
                return sessão;
            }), "id_filme,id_sala",
            parameters);
        
        return sessao;
    }

    public async Task<bool> DeleteSessaoAsync(Guid ID)
    {
        var sql = @"DELETE FROM sessao WHERE id = @id";
        
        var excluido = await _databaseService.ExecuteAsync(sql, new { id = ID });
        
        if (excluido == 1)
            return true;
        
        return false;
    }

    public async Task<int> AddSessaoAsync(Sessão sessao)
    {
        var sql = @"INSERT into sessao (id, filme_id, sala_id, horario_inicio, horario_fim,
                    preco_sessao, idioma_audio, idioma_legenda) VALUES (@ID, @Id_filme, @Id_sala, @horario_inicio, @horario_fim,
                    @preco_sessao, @idioma_audio, @idioma_legenda";
        
        return await _databaseService.ExecuteAsync(sql, sessao);
    }

    public async Task<int> PutSessaoAsync(Sessão sessao)
    {
        var sql = @"UPDATE sessao SET filme_id = @Id_filme, sala_id = @Id_sala, horario_inicio = @horario_inicio,
                  horario_fim = @horario_fim, preco_sessao = @preco_sessao, idioma_audio = @idioma_audio, idioma_legenda = @idioma_legenda
            WHERE id = @id";
        
        return await _databaseService.ExecuteAsync(sql, sessao);
    }
    
    public static void MontarFiltroSessao(SessaoFiltroDto filtro, StringBuilder sb, DynamicParameters parameters)
    {
        if (filtro == null)
            return;

        if (filtro.ID != null)
        {
            sb.Append(" AND s.id = @ID");
            parameters.Add("ID", filtro.ID);
        }

        if (filtro.Id_filme != null)
        {
            sb.Append(" AND s.id_filme = @Id_filme");
            parameters.Add("Id_filme", filtro.Id_filme);
        }

        if (filtro.Id_sala != null)
        {
            sb.Append(" AND s.id_sala = @Id_sala");
            parameters.Add("Id_sala", filtro.Id_sala);
        }

        if (filtro.data_sessao != null)
        {
            sb.Append(" AND s.data_sessao = @data_sessao");
            parameters.Add("data_sessao", filtro.data_sessao);
        }

        if (filtro.horario_inicio != null)
        {
            sb.Append(" AND s.horario_inicio = @horario_inicio");
            parameters.Add("horario_inicio", filtro.horario_inicio);
        }

        if (filtro.horario_fim != null)
        {
            sb.Append(" AND s.horario_fim = @horario_fim");
            parameters.Add("horario_fim", filtro.horario_fim);
        }

        if (filtro.preco_sessao != null)
        {
            sb.Append(" AND s.preco_sessao = @preco_sessao");
            parameters.Add("preco_sessao", filtro.preco_sessao);
        }

        if (filtro.idioma_audio != null)
        {
            sb.Append(" AND s.idioma_audio = @idioma_audio");
            parameters.Add("idioma_audio", filtro.idioma_audio);
        }

        if (filtro.idioma_legenda != null)
        {
            sb.Append(" AND s.idioma_legenda = @idioma_legenda");
            parameters.Add("idioma_legenda", filtro.idioma_legenda);
        }
    }
}