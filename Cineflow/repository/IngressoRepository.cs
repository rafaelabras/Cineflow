using System.Text;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Dapper;

namespace Cineflow.repository;

public class IngressoRepository : IIngressoRepository
{
    private readonly DatabaseService _databaseService;

    public IngressoRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    public async Task<IEnumerable<Ingresso>> GetIngressoAsync(IngressoFiltroDto filtro)
    {
        StringBuilder sb = new StringBuilder();
        DynamicParameters parameters = new DynamicParameters();

        var sql = @"
            SELECT
                i.id AS ingresso_id, i.sessao_id, i.reserva_id, i.assento_id, i.preco_pago, i.codigo_qr, i.data_gerado, i.data_validacao, i.utilizado,
                s.id AS sessao_id, s.filme_id, s.sala_id, s.horario_inicio, s.horario_fim, s.preco_sessao, s.idioma_audio, s.idioma_legenda,
                ass.id AS assento_id, ass.filme_id, ass.sala_id, ass.horario_inicio, ass.horario_fim, ass.preco_sessao, ass.idioma_audio, ass.idioma_legenda,
                r.id AS reserva_id, r.cliente_id, r.sessao_id,r.status, r.data_reserva
                f.id AS filme_id, f.nome_filme, f.sinopse, f.genero, f.duracao, f.classificacao_etaria, f.idioma, f.pais_origem, f.produtor, f.data_lancamento, f.diretor
                sa.id AS sala_id, sa.tipo_sala, sa.capacidade, sa.ocupado
            FROM ingresso AS i
            JOIN sessao AS s ON i.sessao_id = sessao_id
            JOIN assento AS ass ON i.assento_id = assento_id
            JOIN reserva AS r ON i.reserva_id = reserva_id
            JOIN filme AS f ON s.filme_id = filme_id
            JOIN sala sa ON s.sala_id = sala_id";

        sb.Append(sql);
        MontarFiltroIngresso(filtro, sb, parameters);

        return await _databaseService
            .QueryAsyncMultipleObjectsSixJoins<Ingresso, Sessão, Assento, Reserva, Filme, Sala, Ingresso>(sql,
                (ingresso, sessao, assento, reserva, filme, sala) =>
                {
                    ingresso.sala = sala;
                    ingresso.filme = filme;
                    ingresso.assento = assento;
                    ingresso.reserva = reserva;
                    ingresso.sessao = sessao;
                    return ingresso;
                }, "sessao_id, assento_id, reserva_id, filme_id, sala_id", parameters);

    }

    public async Task<bool> DeleteIngressoAsync(Guid ID)
    {
        var sql = @"DELETE FROM Ingresso WHERE Id = @Id";
        
        var db = await _databaseService.ExecuteAsync(sql, new { Id = ID });
        
        if (db == 1)
            return true;
        
        return false;
    }

    public async Task<int> AddIngressoAsync(Ingresso ingresso)
    {
        var sql = @"INSERT INTO ingresso (id, sessao_id, reserva_id, assento_id, perco_pago,
                      codigo_qr, data_gerado, data_validacao, utilizacao, filme_id, sala_id) VALUES 
                      (@ID, @Id_sessao, @Id_reserva, @Id_assento, @preco, @codigo_qr, @data_gerado, @data_validacao, @utilizado, @Id_filme, @Id_sala)";

        return await _databaseService.ExecuteAsync(sql, ingresso);
    }

    public async Task<int> PutIngressoAsync(Ingresso sessao)
    {
        var sql = @"UPDATE ingresso SET sessao_id = @Id_sessao,
        reserva_id =  @Id_reserva, assento_id = @Id_assento, preco_pago = @preco, codigo_qr = @codigo_qr, data_gerado = @data_gerado, 
        data_validacao = @data_validacao, utilizacao = @utilizado, filme_id = @Id_filme, sala_id = @Id_sala
        WHERE id = @ID";
        
        return await _databaseService.ExecuteAsync(sql, sessao);
    }

    private static void MontarFiltroIngresso(IngressoFiltroDto filtro, StringBuilder sb, DynamicParameters parameters)
    {
        if (filtro == null)
            return;

        if (filtro.ID != Guid.Empty)
        {
            sb.Append(" AND i.id = @Id");
            parameters.Add("Id", filtro.ID);
        }

        if (!string.IsNullOrWhiteSpace(filtro.Id_sessao))
        {
            sb.Append(" AND i.sessao_id = @Id_sessao");
            parameters.Add("Id_sessao", filtro.Id_sessao);
        }

        if (filtro.Id_assento != null)
        {
            sb.Append(" AND i.assento_id = @Id_assento");
            parameters.Add("Id_assento", filtro.Id_assento);
        }

        if (!string.IsNullOrWhiteSpace(filtro.Id_reserva))
        {
            sb.Append(" AND i.reserva_id = @Id_reserva");
            parameters.Add("Id_reserva", filtro.Id_reserva);
        }

        if (filtro.preco != null)
        {
            sb.Append(" AND i.preco_pago = @preco");
            parameters.Add("preco", filtro.preco);
        }

        if (!string.IsNullOrWhiteSpace(filtro.codigo_qr))
        {
            sb.Append(" AND i.codigo_qr = @codigo_qr");
            parameters.Add("codigo_qr", filtro.codigo_qr);
        }

        if (filtro.data_gerado != null)
        {
            sb.Append(" AND i.data_gerado = @data_gerado");
            parameters.Add("data_gerado", filtro.data_gerado);
        }

        if (filtro.data_validacao != null)
        {
            sb.Append(" AND i.data_validacao = @data_validacao");
            parameters.Add("data_validacao", filtro.data_validacao);
        }

        if (filtro.utilizado != null)
        {
            sb.Append(" AND i.utilizado = @utilizado");
            parameters.Add("utilizado", filtro.utilizado);
        }
    }

    
}