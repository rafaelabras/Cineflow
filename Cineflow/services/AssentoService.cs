using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.repository;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class AssentoService : IAssentoService
{
    private readonly AssentoRepository _assentoRepository;
    public AssentoService(AssentoRepository assentoRepository)
    {
        _assentoRepository = assentoRepository;
    }
    
    public async Task<Result<IEnumerable<Assento>>> GetAssentoAsync(AssentoFiltroDto dto)
    {
        var sql = await _assentoRepository.GetAssentoAync(dto);
        
        if (sql.IsNullOrEmpty())
            return Result<IEnumerable<Assento>>.Failure("Não foi possível encontrar ou retornar o(s) assento(s)");
        
        return Result<IEnumerable<Assento>>.Success(sql);
    }

    public async Task<Result<bool>> DeleteAssentoAsync(int ID)
    {
        var deleteBd = await _assentoRepository.DeleteAssentoAsync(ID);
        
        if(!deleteBd)
            return Result<bool>.Failure("Não foi possível deletar o assento.");
        
        return Result<bool>.Success(true);
    }

    public async Task<Result<Assento>> CriarAssentoAsync(CriarAssentoDto dto)
    {
        StringBuilder sb = new StringBuilder();
        var validateResult =  validarAssento(dto, sb);
        
        if (!validateResult)
            return Result<Assento>.Failure("Houve um erro na validação dos campos.");

        var executeDb = await _assentoRepository.AddAssentoAsync(dto);
        var assento = fromAsssentoDtoToAssento(executeDb, dto);
        
        if (executeDb == 1)
            return Result<Assento>.Success(assento);
        
        return  Result<Assento>.Failure("Não foi possível criar o assento.");
    }

    public async Task<Result<CriarAssentoDto>> PutAssentoAsync(int id, CriarAssentoDto dto)
    {
        StringBuilder sb = new StringBuilder();
        var validateResult =  validarAssento(dto, sb);
        
        if (!validateResult)
            return Result<CriarAssentoDto>.Failure("Houve um erro na validação dos campos.");

        
        var executeDb = await _assentoRepository.PutAssentoAsync(fromAsssentoDtoToAssento(id, dto));
        
        if (executeDb == 1)
            return Result<CriarAssentoDto>.Success(dto);
        
        return  Result<CriarAssentoDto>.Failure("Não foi possível atualizar o assento.");
    }

    private Assento fromAsssentoDtoToAssento(int id, CriarAssentoDto criarAssentoDto)
    {
        return new Assento
        {
            Id = id,
            Id_sala = criarAssentoDto.Id_sala,
            fila = criarAssentoDto.fila,
            numero = criarAssentoDto.numero,
            status = criarAssentoDto.status
        };
    }

    private static bool validarAssento(CriarAssentoDto criarAssentodto, StringBuilder sb)
    {
        AssentoModelValidator validate = new AssentoModelValidator();
        var validating = validate.Validate(criarAssentodto);

        if (!validating.IsValid)
        {
            validating.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));
            return false;   
        }
        
        return true;
    }
}