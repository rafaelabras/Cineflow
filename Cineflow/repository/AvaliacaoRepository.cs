using System.Text;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.models.pessoas;
using Dapper;

namespace Cineflow.repository;

public class AvaliacaoRepository : IAvaliacaoRepository
{
    private readonly DatabaseService _databaseService;

    public AvaliacaoRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    public async Task<IEnumerable<Avaliação>> GetAvaliacaoAync(AvaliacaoFiltroDto dto)
    {
        StringBuilder sb = new StringBuilder();
        
        var sql = @"
        SELECT
            a.id AS id_avaliacao, a.filme_id, a.cliente_id, a.reserva_id, a.nota, a.comentario, a.data_avaliacao,
            c.id AS id_cliente, c.nome, c.email, c.genero, c.telefone, c.data_nascimento,
            r.id as id_reserva, r.cliente_id, r.sessao_id,, r.status, r.data_reserva,
            f.id as id_filme, f.nome_filme, f.sinopse, f.genero, f.duracao, f.classificacao_etaria, f.idioma,
            f.pais_origem, f.produtor, f.data_lancamento, f.diretor
        FROM avaliacao a 
        JOIN cliente c ON a.cliente_id = id_cliente
        JOIN reserva r ON a.reserva_id = id_reserva
        JOIN filme f ON a.filme_id = id_filme
        WHERE 1=1 ";

        sb.Append(sql);
        DynamicParameters parameters = new DynamicParameters();
        MontarFiltroAvaliacao(dto, sb, parameters);
        
        return await _databaseService.QueryAsyncMultipleObjectsFourJoins<Avaliação, Cliente, Reserva, Filme, Avaliação>(sb.ToString(),
            ((avaliacao, cliente, reserva, filme) =>
            {
                avaliacao.cliente = cliente;
                avaliacao.reserva = reserva;
                avaliacao.filme = filme;
                return avaliacao;
            }), "id_cliente, id_reserva, id_filme", parameters);


    }

    public async Task<bool> DeleteAvaliacaoAsync(int Id)
    {
        var sql = @"DELETE FROM avaliacao WHERE id = @id";
        
        var execute = await _databaseService.ExecuteAsync(sql, new { id = Id });
        
        if (execute == 1)
            return true;
        
        return false;
    }

    public async Task<int> AddAvaliacaoAsync(CriarAvaliacaoDto dto)
    {
        var sql = @"INSERT INTO avaliacao (filme_id, cliente_id, reserva_id, nota, comentario, data_avaliacao)
            VALUES (@Id_filme, @Id_cliente, @Id_reserva, @nota, @comentario, @data_avaliacao)";
        
        return await _databaseService.ExecuteAsync(sql, dto);
    }

    public async Task<int> PutAvaliacaoAsync(Avaliação avaliacao)
    {
        var sql =
            @"UPDATE avaliacao SET filme_id = @Id_filme, cliente_id = @Id_cliente, reserva_id = @Id_reserva, nota = @nota
            comentario = @comentario, data_avaliacao = @data_avaliacao WHERE id = @id";
        
        return await _databaseService.ExecuteAsync(sql, avaliacao);
    }
    
    public static void MontarFiltroAvaliacao(AvaliacaoFiltroDto filtro, StringBuilder sb, DynamicParameters parameters)
    {
        if (filtro.Id > 0)
        {
            sb.Append(" AND a.id = @id ");
            parameters.Add("id", filtro.Id);
        }

        if (!string.IsNullOrWhiteSpace(filtro.Id_cliente))
        {
            sb.Append(" AND a.cliente_id = @cliente_id ");
            parameters.Add("cliente_id", filtro.Id_cliente);
        }

        if (!string.IsNullOrWhiteSpace(filtro.Id_reserva))
        {
            sb.Append(" AND a.reserva_id = @reserva_id ");
            parameters.Add("reserva_id", filtro.Id_reserva);
        }

        if (!string.IsNullOrWhiteSpace(filtro.id_filme))
        {
            sb.Append(" AND a.filme_id = @filme_id ");
            parameters.Add("filme_id", filtro.id_filme);
        }

        if (filtro.nota > 0) // válido apenas se a nota veio filtrada
        {
            sb.Append(" AND a.nota = @nota ");
            parameters.Add("nota", filtro.nota);
        }

        if (!string.IsNullOrWhiteSpace(filtro.comentario))
        {
            sb.Append(" AND a.comentario ILIKE @comentario ");
            parameters.Add("comentario", $"%{filtro.comentario}%");
        }

        if (filtro.data_avaliacao != DateTime.MinValue)
        {
            sb.Append(" AND DATE(a.data_avaliacao) = @data_avaliacao ");
            parameters.Add("data_avaliacao", filtro.data_avaliacao.Date);
        }
    }

}