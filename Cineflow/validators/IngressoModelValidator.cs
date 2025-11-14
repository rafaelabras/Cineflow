using Cineflow.dtos.cinema;
using FluentValidation;

namespace Cineflow.validators;

public class IngressoModelValidator : AbstractValidator<CriarIngressoDto>
{
    public IngressoModelValidator()
    {
        RuleFor(i => i.Id_sessao)
            .NotEmpty()
            .WithMessage("O ingresso precisa estar vinculado a uma sessão válida.");

        RuleFor(i => i.Id_assento)
            .NotNull()
            .WithMessage("O ingresso precisa ter um assento associado.")
            .GreaterThan(0)
            .WithMessage("O id do assento deve ser maior que zero.");

        RuleFor(i => i.Id_reserva)
            .NotEmpty()
            .WithMessage("O ingresso precisa estar vinculado a uma reserva.");

        RuleFor(i => i.preco)
            .GreaterThan(0)
            .WithMessage("O preço do ingresso deve ser maior que zero.");

        RuleFor(i => i.codigo_qr)
            .NotEmpty()
            .WithMessage("O ingresso deve possuir um código QR.");

        RuleFor(i => i.data_gerado)
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("A data de geração não pode estar no futuro.");

        RuleFor(i => i.data_validacao)
            .NotNull()
            .GreaterThanOrEqualTo(i => i.data_gerado)
            .WithMessage("A data de validação não pode ser anterior à data de geração.");

        RuleFor(i => i.utilizado)
            .NotNull()
            .WithMessage("O status de utilização do ingresso deve ser informado.");
    }
}