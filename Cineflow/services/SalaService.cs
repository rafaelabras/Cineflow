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
        StringBuilder sb = new StringBuilder();
        var validate = Validate(criarSalaDto, sb);

     if (!validate)
         return Result<Sala>.Failure($"Não foi possível criar a sala, verifique os dados, {sb.ToString()}");
     
     var create = await _salaRepository.CreateSalaAsync(criarSalaDto);

     if (create > 0)
     {
         return Result<Sala>.Success(FromSalaDtoToSala(create, criarSalaDto));
     }
     
     return Result<Sala>.Failure("A sala não foi criada.");
    }

    public async Task<Result<Sala>> PutSalaAsync(int id, CriarSalaDto criarSalaDto)
    {
        StringBuilder sb = new StringBuilder();
        var validate = Validate(criarSalaDto, sb);
        
        if (!validate)
            return Result<Sala>.Failure($"Não foi possível criar a sala, verifique os dados, {sb.ToString()} ");

        var sala = FromSalaDtoToSala(id, criarSalaDto);

        var bd = await _salaRepository.PutSalaAsync(sala);

        if (bd == 1)
        {
            return Result<Sala>.Success(sala);
        }
        
        return Result<Sala>.Failure("A sala não foi criada.");
    }

    private Sala FromSalaDtoToSala(int id, CriarSalaDto criarSalaDto)
    {
        return new Sala
        {
            Id = id,
            tipo_sala = criarSalaDto.tipo_sala,
            assentos_ocupados = criarSalaDto.assentos_ocupados,
            capacidade = criarSalaDto.capacidade,
        };
    }
    private bool Validate(CriarSalaDto criarSalaDto, StringBuilder sb) // refatorar
    {
        SalaModelValidator salaValidator = new SalaModelValidator();
        var result = salaValidator.Validate(criarSalaDto);

        if (!result.IsValid)
        {
            result.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));
            return false;
        }

        return true;
    }
}