using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.models.cinema;
using Cineflow.repository;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class IngressoService : IIngressoService
{
    private readonly IngressoRepository _ingressoRepository;

    public IngressoService(IngressoRepository ingressoRepository)
    {
        _ingressoRepository = ingressoRepository;
    }
    public async Task<Result<IEnumerable<Ingresso>>> GetIngressoAsync(IngressoFiltroDto filtro)
    {
        var query = await _ingressoRepository.GetIngressoAsync(filtro);

        if (query.IsNullOrEmpty())
            return Result<IEnumerable<Ingresso>>.Failure("Não foi possível encontrar nenhum ingresso.");

        return Result<IEnumerable<Ingresso>>.Success(query);
    }

    public async Task<Result<bool>> DeleteIngressoAsync(Guid ID)
    {
        var delete = await _ingressoRepository.DeleteIngressoAsync(ID);
        
        if (delete == true)
            return Result<bool>.Success(delete);
        
        return Result<bool>.Failure("Não foi possível deletar o ingresso");
    }

    public async Task<Result<Ingresso>> CriarIngressoAsync(CriarIngressoDto ingresso)
    {
        StringBuilder sb = new StringBuilder();
        var validate = ValidateIngressoAndErrors(ingresso, sb);

        if (validate == false)
        {
            return Result<Ingresso>.Failure($"Não foi possível criar o ingresso, verifique os campos: {sb.ToString()}");
        }
        
        var ingressoModel = FromIngressoDtoToIngresso(ingresso);
        var create = await _ingressoRepository.AddIngressoAsync(ingressoModel);
        
        if (create == 1)
            return Result<Ingresso>.Success(ingressoModel);
        
        return Result<Ingresso>.Failure("Não foi possível criar o ingresso");
    }

    public async Task<Result<CriarIngressoDto>> PutIngressoAsync(Guid id, CriarIngressoDto ingresso)
    {
        StringBuilder sb = new StringBuilder();
        var validate = ValidateIngressoAndErrors(ingresso, sb);

        if (validate == false)
        {
            return Result<CriarIngressoDto>.Failure($"Não foi possível atualizar o ingresso, verifique os campos: {sb.ToString()}");
        }
        
        var ingressoModel = FromIngressoDtoToIngresso(ingresso, id);
        var update = await _ingressoRepository.PutIngressoAsync(ingressoModel);
        
        if (update == 1)
            return Result<CriarIngressoDto>.Success(ingresso);
        
        return Result<CriarIngressoDto>.Failure("Não foi possível atualizar o ingresso");
    }

    private Ingresso FromIngressoDtoToIngresso(CriarIngressoDto criarIngressoDto, Guid? id = null)
    {
        return new Ingresso
        {
            ID = id ?? Guid.NewGuid(),
            Id_sala = criarIngressoDto.Id_sala,
            Id_assento = criarIngressoDto.Id_assento,
            Id_reserva = criarIngressoDto.Id_reserva,
            Id_filme = criarIngressoDto.Id_filme,
            preco = criarIngressoDto.preco,
            codigo_qr = criarIngressoDto.codigo_qr,
            data_gerado = criarIngressoDto.data_gerado,
            data_validacao = criarIngressoDto.data_validacao,
            utilizado = criarIngressoDto.utilizado
        };

    }

    private bool ValidateIngressoAndErrors(CriarIngressoDto ingressoDto, StringBuilder sb)
    {
        var validator = new IngressoModelValidator();
        
        var validate = validator.Validate(ingressoDto);

        if (!validate.IsValid)
        {
            validate.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));
            return false;
        }
        return true;
    }
    
    
}