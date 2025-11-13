using System.Text;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.models.pessoas;
using Dapper;

namespace Cineflow.repository;

public class ReservaRepository : IReservaRepository
{
    private readonly DatabaseService _databaseService;
    public ReservaRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    
    public async Task<IEnumerable<Reserva>> GetReservaAsync(ReservaFiltroDto filtro)
    {
        StringBuilder sb = new StringBuilder();
        var sql = @"
        SELECT
           r.id AS id_reserva, r.cliente_id, r.sessao_id, r.status, r.data_reserva 
           c.id AS id_cliente, c.cpf, c.nome, c.email, c.genero, c.telefone, c.data_nascimento,
           s.id AS id_sessao, s.filme_id, s.sala_id, s.horario_inicio, s.horario_fim,
           s.preco_sessao, s.idioma_audio, s.idioma_lengeda
        FROM reserva r
        JOIN cliente c ON c.id = r.cliente_id
        JOIN sessao s ON s.id = r.sessao_id
        WHERE 1=1 ";
        sb.Append(sql);
        var parameters = new DynamicParameters();
        MontarFiltroReserva(filtro, sb, parameters);
        
        return await _databaseService.QueryAsyncMultipleObjectsThreeJoins<Reserva, Cliente, Sessão, Reserva>(sb.ToString(), (
            (reserva, cliente, sessao) =>
            {
                reserva.cliente = cliente;
                reserva.sessao = sessao;    
                return reserva;
            }), "id_cliente,id_sessao", parameters);
    }

    public async Task<bool> DeleteReservaAsync(Guid id)
    {
        var sql = @"DELETE FROM reserva WHERE id = @Id";
        
        var reserva = await _databaseService.ExecuteAsync(sql, new { Id = id });
        
        if (reserva == 1)
            return true;
        
        return false;
    }

    public async Task<int> AddReservaAsync(Reserva reserva)
    {
        var sql = @"INSERT INTO reserva (id, cliente_id, sessao_id, status, data_reserva)
                    VALUES(@Id, @Id_cliente, @Id_sessao, @status, @data_reserva)";
        
        return await _databaseService.ExecuteAsync(sql, reserva);
    }

    public async Task<int> PutReservaAsync(Reserva reserva)
    {
        var sql = @"UPDATE reserva SET cliente_id = @Id_cliente, sessao_id = @Id_sessao,
                   status = @status, data_reserva = @data_reseva WHERE @id = @id";
        
        return await _databaseService.ExecuteAsync(sql, reserva);
    }

    private static void MontarFiltroReserva(ReservaFiltroDto filtro,StringBuilder sb, DynamicParameters parameters)
    {
        if (filtro == null)
            return;

        if (filtro.Id != Guid.Empty)
        {
            sb.Append(" AND r.id = @Id");
            parameters.Add("Id", filtro.Id);
        }

        if (!string.IsNullOrWhiteSpace(filtro.Id_cliente))
        {
            sb.Append(" AND r.cliente_id = @Id_cliente");
            parameters.Add("Id_cliente", filtro.Id_cliente);
        }

        if (!string.IsNullOrWhiteSpace(filtro.Id_sessao))
        {
            sb.Append(" AND r.sessao_id = @Id_sessao");
            parameters.Add("Id_sessao", filtro.Id_sessao);
        }

        if (filtro.status != null)
        {
            sb.Append(" AND r.status = @status");
            parameters.Add("status", filtro.status);
        }

        if (filtro.data_reserva != null)
        {
            sb.Append(" AND r.data_reserva = @data_reserva");
            parameters.Add("data_reserva", filtro.data_reserva);
        }
    }
}