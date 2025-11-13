using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class ReservaServices :IReservaService
{
    private readonly IReservaRepository _reservaRepository;

    public ReservaServices(IReservaRepository reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }
    public async Task<Result<IEnumerable<Reserva>>> GetReservaAsync(ReservaFiltroDto dto)
    {
        var query = await _reservaRepository.GetReservaAsync(dto);
        
        if (query.IsNullOrEmpty())
            return Result<IEnumerable<Reserva>>.Failure("Não foi possível encontrar nenhuma reserva");
        
        return Result<IEnumerable<Reserva>>.Success(query);
    }

    public async Task<Result<bool>> DeleteReservaAsync(Guid ID)
    {
        var result = await _reservaRepository.DeleteReservaAsync(ID);
        
        if (!result)
            return Result<bool>.Failure("Não foi possível excluir a reserva");
        
        return Result<bool>.Success(true);
    }

    public async Task<Result<Reserva>> CriarReservaAsync(CriarReservaDto criarReservaDto)
    {
        StringBuilder sb = new StringBuilder();
        var resultValidation = ValidarReservaModel(criarReservaDto, sb);
        if (!resultValidation)
            return Result<Reserva>.Failure($"Houve um erro ao validar os campos: {sb.ToString()}");

        var reserva = fromReservaDtoToReserva(criarReservaDto);
        
        var createDb = await _reservaRepository.AddReservaAsync(reserva);
        
        if(createDb == 1)
            return Result<Reserva>.Success(reserva);
        
        return Result<Reserva>.Failure("Não foi possível criar a reserva");
    }

    public async Task<Result<CriarReservaDto>> PutReservaAsync(Guid id, CriarReservaDto criarReservaDto)
    {
        StringBuilder sb = new StringBuilder();
        var resultValidation = ValidarReservaModel(criarReservaDto, sb);
        
        if (!resultValidation)
            return Result<CriarReservaDto>.Failure($"Houve um erro ao validar os campos {sb.ToString()}");
        
        var reserva =  fromReservaDtoToReserva(criarReservaDto, id);
        
        var putDb = await _reservaRepository.PutReservaAsync(reserva);
        
        if(putDb == 1)
            return Result<CriarReservaDto>.Success(criarReservaDto);
        
        return Result<CriarReservaDto>.Failure("Não foi possível atualizar a reserva.");
    }

    private Reserva fromReservaDtoToReserva(CriarReservaDto criarReservaDto, Guid? id = null)
    {
        return new Reserva
        {
            Id = id ?? new Guid() ,
            Id_cliente = criarReservaDto.Id_cliente,
            Id_sessao = criarReservaDto.Id_sessao,
            status = criarReservaDto.status,
            data_reserva = criarReservaDto.data_reserva
        };

    }

    private bool ValidarReservaModel(CriarReservaDto criarReservaDto, StringBuilder sb)
    {
        ReservaModelValidator validate = new ReservaModelValidator();
        var resultado = validate.Validate(criarReservaDto);

        if (!resultado.IsValid)
        {
            resultado.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));
            return false;   
        }

        return true;
    }
}