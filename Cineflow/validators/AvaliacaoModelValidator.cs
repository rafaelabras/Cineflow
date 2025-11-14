using Cineflow.dtos.cinema;
using FluentValidation;

namespace Cineflow.validators;

public class AvaliacaoModelValidator : AbstractValidator<CriarAvaliacaoDto>
{
    public AvaliacaoModelValidator()
    {
        RuleFor(a => a.Id_cliente)
            .NotEmpty()
            .WithMessage("O id do cliente deve ser informado.");

        RuleFor(a => a.Id_reserva)
            .NotEmpty()
            .WithMessage("O id da reserva deve ser informado.");

        RuleFor(a => a.Id_filme)
            .NotEmpty()
            .WithMessage("O id do filme deve ser informado.");

        RuleFor(a => a.nota)
            .InclusiveBetween(1, 5)
            .WithMessage("A nota deve estar entre 1 e 5.");

        RuleFor(a => a.comentario)
            .NotEmpty()
            .WithMessage("O comentário deve ser informado.")
            .MaximumLength(500)
            .WithMessage("O comentário deve conter no máximo 500 caracteres.");

        RuleFor(a => a.data_avaliacao)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("A data da avaliação não pode estar no futuro.");
    }
}