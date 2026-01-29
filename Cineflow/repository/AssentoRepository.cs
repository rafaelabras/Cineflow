using System.Data.Common;
using System.Text;
using Cineflow.dtos.cinema;
using Cineflow.helpers;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.repository;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Cineflow.services;

public class AssentoRepository: IAssentoRepository
{
    private readonly DatabaseService _databaseService;
    private readonly LogResultHelper<AssentoRepository> _logger;

    public AssentoRepository(DatabaseService databaseService, LogResultHelper<AssentoRepository> logger)
    {
        _logger = logger;
        _databaseService = databaseService;
    }
    public async Task<IEnumerable<Assento>> GetAssentoAync(AssentoFiltroDto dto)
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            var sql = @"
                SELECT 
                    a.id, a.sala_id, a.fila, a.numero, a.status
                    s.id, s.tipo_sala, s.capacidade, s.ocupado
                FROM assento a
                JOIN SALA s ON a.sala_id = s.sala_id
                WHERE 1 = 1";
            sb.Append(sql);

            var parameters = new DynamicParameters();
            FiltroGetAssento(dto, sb, parameters);


            return await _databaseService.QueryAsyncMultipleObjectsOneJoin<Assento, Sala, Assento>(sb.ToString(),
                ((assento, sala) =>
                {
                    assento.sala = sala;
                    return assento;
                }), "s.id", parameters);

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<bool> DeleteAssentoAsync(int id)
    {
            var sql = @"DELETE FROM assento WHERE id = @id";
            var sb = new StringBuilder(sql);
            
            var delete = await _databaseService.ExecuteAsync(sql, new { id = id });
            if (delete == 1)
            {
                _logger.LogResultSqlOperations(sb.Append(sql), new { id = id }, delete, LogLevel.Information,
                    "Deletado com sucesso");
                return true;
            }

            if (delete != 1 && delete != 0)
            {
                _logger.LogResultSqlOperations(sb.Append(sql), new { id = id }, delete, LogLevel.Warning,
                    "As linhas que o SQL afetou foram diferentes de 0 e 1.");
                return false;
            }


            _logger.LogResultSqlOperations(sb.Append(sql), new { id = id }, delete, LogLevel.Information,
                "Não foi possível deletar");
            return false;
    }

    public async Task<int> AddAssentoAsync(CriarAssentoDto dto)
    {
            var sql = @"INSERT INTO assento (sala_id, fila, numero, status)
                  VALUES (@Id_sala, @fila, @numero, @status)";

            return await _databaseService.ExecuteAsync(sql, dto);
    }

    public async Task<int> PutAssentoAsync(Assento assento)
    {
            var sql = @"UPDATE assento SET sala_id = @Id_sala, fila = @fila, numero = @numero, status = @status
                  WHERE  id = @Id";

            return await _databaseService.ExecuteAsync(sql, assento);
    }

    private void FiltroGetAssento(AssentoFiltroDto dto, StringBuilder sb, DynamicParameters parameters)
    {
        if (dto.Id is not null)
        {
            sb.Append(" AND a.id = @Id ");
            parameters.Add("Id", dto.Id);
        }

        if (dto.Id_sala is not null)
        {
            sb.Append(" AND a.sala_id = @Id_sala ");
            parameters.Add("Id_sala", dto.Id_sala);
        }

        if (dto.fila is not null)
        {
            sb.Append(" AND a.fila = @fila ");
            parameters.Add("fila", dto.fila);
        }

        if (dto.numero is not null)
        {
            sb.Append(" AND a.numero = @numero ");
            parameters.Add("numero", dto.numero);
        }

        if (dto.status is not null)
        {
            sb.Append(" AND a.status = @status ");
            parameters.Add("status", dto.status);
        }
        
    }
}