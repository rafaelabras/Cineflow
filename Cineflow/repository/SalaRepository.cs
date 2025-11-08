using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;

namespace Cineflow.repository;

public class SalaRepository : ISalaRepository
{
    
    private readonly DatabaseService _databaseService;

    public SalaRepository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    
    public async Task<int> CreateSalaAsync(CriarSalaDto criarSalaDto)
    {
        var sql = @"INSERT INTO sala(tipo_sala, capacidade, ocupado) VALUES (@nomeSala, @capacidade, @assentos_ocupados) RETURNING id";

        return await _databaseService.ExecuteAsync(sql, criarSalaDto);
    }

    public async Task<int> DeleteSalaAsync(int id)
    {
        var sql = @"DELETE FROM sala WHERE id = @id";
        
        return await _databaseService.ExecuteAsync(sql, id);
    }

    public async Task<IEnumerable<Sala>> GetSalasAsync()
    {
        var sql = @"select * from sala";
        
        return await _databaseService.QueryAsync<Sala>(sql);
    }

    public async Task<int> PutSalaAsync(Sala sala)
    {
        var sql = @"UPDATE sala (tipo_sala, capacidade, ocupada) VALUES (@tipo_sala, @capacidade, @assentos_ocupados) WHERE id = @id";

        return await _databaseService.ExecuteAsync(sql, sala);
    }

    public async Task<IEnumerable<Sala>> GetSalaByIDAsync(int id)
    {
        var sql = @"SELECT * FROM sala WHERE id = @id";
        
        return await _databaseService.QueryAsync<Sala>(sql, id);
        
    }
    
}