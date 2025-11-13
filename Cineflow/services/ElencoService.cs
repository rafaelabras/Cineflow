using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.repository;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class ElencoService : IElencoService
{
    private readonly IElencoRepository _elencoRepository;

    public ElencoService(IElencoRepository elencoRepository)
    {
        _elencoRepository = elencoRepository;
    }
    public async Task<Result<IEnumerable<Elenco>>> GetElencoAsync(ElencoFiltroDto filtro)
    {
        var query = await _elencoRepository.GetElencoAsync(filtro);
        
        if (!query.IsNullOrEmpty())
            return Result<IEnumerable<Elenco>>.Success(query);

        return Result<IEnumerable<Elenco>>.Failure("Não foi possível encontrar nenhum elenco.");
        
    }

    public async Task<Result<bool>> DeleteElencoAsync(int ID)
    {
        var query = await _elencoRepository.DeleteElencoAsync(ID);
        
        if (query == true)
            return Result<bool>.Success(true);
        
        return Result<bool>.Failure("Não foi possível excluir o elenco.");
    }

    public async Task<Result<Elenco>> CriarElencoAsync(CriarElencoDto criarElencoDto)
    {
        StringBuilder sb = new StringBuilder();
        var validation = ValidateElencoDto(sb, criarElencoDto);
        
        if (!validation)
            return Result<Elenco>.Failure($"Ocorreu erros ao criar o elenco: {sb.ToString()}");
        
        var createDb = await _elencoRepository.AddElencoAsync(criarElencoDto);
        
        if (createDb > 0)
            return Result<Elenco>.Success(FromElencoDtoToElenco(criarElencoDto, createDb));
        
        return Result<Elenco>.Failure("Ocorreu um erro ao criar o elenco.");
    }

    public async Task<Result<CriarElencoDto>> PutElencoAsync(int id, CriarElencoDto criarElenco)
    {
        StringBuilder sb = new StringBuilder();
        var validation = ValidateElencoDto(sb, criarElenco);

        if (!validation)
            return Result<CriarElencoDto>.Failure($"Ocorreu erros ao criar o elenco: {sb.ToString()}");

        var createDb = await _elencoRepository.PutElencoAsync(FromElencoDtoToElenco(criarElenco, id));
        
        if (createDb != 0)
            return Result<CriarElencoDto>.Success(criarElenco);
        
        return Result<CriarElencoDto>.Failure("Não foi possível criar o elenco.");
    }

    private static bool ValidateElencoDto(StringBuilder sb,CriarElencoDto criarElencoDto)
    {
        var validator = new ElencoModelValidator();
        var validating = validator.Validate(criarElencoDto);

        if (!validating.IsValid)
        {
            validating.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));
            return false;
        }
        return true;
    }

    private static Elenco FromElencoDtoToElenco(CriarElencoDto dto, int id)
    {
        return new Elenco
        {
            Id = id,
            nome = dto.nome,
            genero = dto.genero,
            data_nascimento = dto.data_nascimento,
            nacionalidade = dto.nacionalidade
        };
    }
    
}