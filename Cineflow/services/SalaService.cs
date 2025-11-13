using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.@interface.IClienteRepository;
using Cineflow.models.cinema;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class SalaService : ISalaService
{
    private readonly ISalaRepository _salaRepository;

    public SalaService(ISalaRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }
    
    public async Task<Result<IEnumerable<Sala>>> GetSalaAsync()
    {
        var salas = await _salaRepository.GetSalasAsync();
        
        if (!salas.IsNullOrEmpty())
        {
            return Result<IEnumerable<Sala>>.Success(salas);
        }
        
        return Result<IEnumerable<Sala>>.Failure("Nenhuma sala encontrada.");
        
    }

    public async Task<Result<IEnumerable<Sala>>> GetSalaByIDAsync(int id)
    {
        var salas = await _salaRepository.GetSalaByIDAsync(id);
        
        if (!salas.IsNullOrEmpty())
            return Result<IEnumerable<Sala>>.Success(salas);
        
        
        return Result<IEnumerable<Sala>>.Failure("nenhuma sala encontrada.");
    }

    public async Task<Result<bool>> DeleteSalaAsync(int ID)
    {
        var deleted = await _salaRepository.DeleteSalaAsync(ID);

        if (deleted == 1)
        {
            return Result<bool>.Success(true);
        }
        
        return Result<bool>.Failure("Não foi possível deletar a sala.");
    }

    public async Task<Result<Sala>> CriarSalaAsync(CriarSalaDto criarSalaDto)
    {
     var validate = Validate(criarSalaDto);

     if (!validate.IsSuccess)
         return Result<Sala>.Failure(validate.Error);
     
     var create = await _salaRepository.CreateSalaAsync(criarSalaDto);

     if (create > 0)
     {
         return Result<Sala>.Success(new Sala
         {
             Id = create,
             tipo_sala = criarSalaDto.tipo_sala,
             assentos_ocupados = criarSalaDto.assentos_ocupados,
             capacidade = criarSalaDto.capacidade,
         });
     }
     
     return Result<Sala>.Failure("A sala não foi criada.");
    }

    public async Task<Result<Sala>> PutSalaAsync(int id, CriarSalaDto criarSalaDto)
    {
        var validate = Validate(criarSalaDto);
        
        if (!validate.IsSuccess)
            return Result<Sala>.Failure(validate.Error);

        var sala = new Sala
        { Id = id,
            tipo_sala = criarSalaDto.tipo_sala,
            assentos_ocupados = criarSalaDto.assentos_ocupados,
            capacidade = criarSalaDto.capacidade };

        var bd = await _salaRepository.PutSalaAsync(sala);

        if (bd == 1)
        {
            return Result<Sala>.Success(sala);
        }
        
        return Result<Sala>.Failure("A sala não foi criada.");
    }
    
    private Result<CriarSalaDto> Validate(CriarSalaDto criarSalaDto)
    {
        SalaModelValidator salaValidator = new SalaModelValidator();
        var result = salaValidator.Validate(criarSalaDto);

        if (!result.IsValid)
        {
            StringBuilder sb = new StringBuilder();
            result.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));

            return Result<CriarSalaDto>.Failure(sb.ToString());
        }
        return Result<CriarSalaDto>.Success(criarSalaDto);
    }
}