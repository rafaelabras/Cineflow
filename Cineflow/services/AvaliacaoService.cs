using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class AvaliacaoService : IAvaliacaoService
{
    private readonly IAvaliacaoRepository _repository;

    public AvaliacaoService(IAvaliacaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Avaliação>>> GetAvaliacaoAsync(AvaliacaoFiltroDto dto)
    {
        var get = await _repository.GetAvaliacaoAync(dto);

        if (get.IsNullOrEmpty())
            return Result<IEnumerable<Avaliação>>.Failure("Não foi possível encontrar a(s) avaliaçõe(s).");
        
        return Result<IEnumerable<Avaliação>>.Success(get);
    }

    public async Task<Result<bool>> DeleteAvaliacaoAsync(int id)
    {
        var delete = await _repository.DeleteAvaliacaoAsync(id);
        
        if (!delete)
            return Result<bool>.Failure("Não foi possível excluir a avaliação.");
        
        return Result<bool>.Success(true);
    }

    public async Task<Result<Avaliação>> AddAvaliacaoAsync(CriarAvaliacaoDto dto)
    {
        StringBuilder sb = new StringBuilder();
        var validate = ValidarAvaliacaoDto(sb, dto);
        
        if (!validate)
            return Result<Avaliação>.Failure($"Verifique os campos, a validação não foi aprovada:  {sb.ToString()}");

        var create = await _repository.AddAvaliacaoAsync(dto);
        
        if (create == 1)
            return Result<Avaliação>.Success(FromAvaliacaoDtoToAvaliacao(create, dto));
        
        return Result<Avaliação>.Failure("Não foi possível criar a avaliação.");
    }

    public async Task<Result<CriarAvaliacaoDto>> PutAvaliacaoAsync(int id, CriarAvaliacaoDto dto)
    {
        StringBuilder sb = new StringBuilder();
        var validate = ValidarAvaliacaoDto(sb, dto);
        
        if (!validate)
            return Result<CriarAvaliacaoDto>.Failure($"Verifique os campos, a validação não foi aprovada: {sb.ToString()}");

        var create = await _repository.PutAvaliacaoAsync(FromAvaliacaoDtoToAvaliacao(id, dto));
        
        if (create == 1)
            return Result<CriarAvaliacaoDto>.Success(dto);
        
        return Result<CriarAvaliacaoDto>.Failure("Não foi possível atualizar a avaliação.");
    }

    private Avaliação FromAvaliacaoDtoToAvaliacao(int id,CriarAvaliacaoDto dto)
    {
        return new Avaliação
        {
            Id = id,
            Id_cliente = dto.Id_cliente,
            Id_reserva = dto.Id_reserva,
            id_filme = dto.Id_filme,
            nota = dto.nota,
            comentario = dto.comentario,
            data_avaliacao = dto.data_avaliacao,
        };
    }
    private bool ValidarAvaliacaoDto(StringBuilder sb, CriarAvaliacaoDto dto)
    {
        AvaliacaoModelValidator validator = new AvaliacaoModelValidator();
        var validating = validator.Validate(dto);

        if (!validating.IsValid)
        {
            validating.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));
            return false;    
        }
        
        return true;
    }
}