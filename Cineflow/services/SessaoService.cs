using System.Collections;
using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class SessaoService : ISessaoService
{
    private readonly  ISessaoRepository _sessaoRepository;

    public SessaoService(ISessaoRepository sessaoRepository)
    {
        _sessaoRepository = sessaoRepository;
    }
    public async Task<Result<IEnumerable<Sessão>>> GetSessaoAsync(SessaoFiltroDto filtro)
    {
        var sessao = await _sessaoRepository.GetSessaoAsync(filtro);

        if (!sessao.IsNullOrEmpty())
            return Result<IEnumerable<Sessão>>.Success(sessao);
        
        return Result<IEnumerable<Sessão>>.Failure("Não foi possível encontrar a(s) sala(s)");
    }

    public async Task<Result<bool>> DeleteSessaoAsync(Guid id)
    {
        var sessao = await _sessaoRepository.DeleteSessaoAsync(id);
        
        if (sessao == true)
            return Result<bool>.Success(true);

        return Result<bool>.Failure("Não foi possível excluir a sessaão");
    }

    public async Task<Result<Sessão>> CriarSessaoAsync(CriarSessaoDto criarSessaoDto)
    {
        StringBuilder sb = new StringBuilder();
        var validar = ValidateSessaoDto(sb, criarSessaoDto);
        
        if(!validar)
            return Result<Sessão>.Failure(sb.ToString());
        
        
        var sessao = FromSessaoDtoToSessao(criarSessaoDto);
        var createDb = await _sessaoRepository.AddSessaoAsync(sessao);

        if (createDb != 1)
            return Result<Sessão>.Failure("Não foi possível criar a sessão.");

        return Result<Sessão>.Success(sessao);

    }

    public async Task<Result<CriarSessaoDto>> PutSessaoAsync(Guid id, CriarSessaoDto criarSessaoDto)
    {
        StringBuilder sb = new StringBuilder();
        var validar = ValidateSessaoDto(sb, criarSessaoDto);
        
        if(!validar)
            return Result<CriarSessaoDto>.Failure(sb.ToString());
        
        var sessao = FromSessaoDtoToSessao(criarSessaoDto, id);
        var updateDb = await _sessaoRepository.PutSessaoAsync(sessao);

        if (updateDb != 1)
            return Result<CriarSessaoDto>.Failure("Não foi possível atualizar a sessão.");

        return Result<CriarSessaoDto>.Success(criarSessaoDto);   
    }

    private static bool ValidateSessaoDto(StringBuilder sb, CriarSessaoDto dto)
    {
        var validate = new SessaoModelValidator();
        var validando = validate.Validate(dto);

        if (!validando.IsValid)
        {
            validando.Errors.ForEach(error => sb.AppendLine(error.ErrorMessage));
            return false;
        }

        return true;

    }

    private static Sessão FromSessaoDtoToSessao(CriarSessaoDto dto, Guid? id = null)
    {
        return new Sessão
        {
            ID = id ?? Guid.NewGuid(),
            data_sessao = dto.data_sessao,
            Id_filme = dto.Id_filme,
            Id_sala = dto.Id_sala,
            horario_fim = dto.horario_fim,
            horario_inicio = dto.horario_inicio,
            idioma_legenda = dto.idioma_legenda,
            preco_sessao = dto.preco_sessao,
            idioma_audio = dto.idioma_audio,
        };

    }
}