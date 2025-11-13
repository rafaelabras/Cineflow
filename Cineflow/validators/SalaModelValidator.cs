using Cineflow.dtos.cinema;
using FluentValidation;

namespace Cineflow.validators;

public class SalaModelValidator : AbstractValidator<CriarSalaDto>
{
    public SalaModelValidator()
    {
        RuleFor(s => s.tipo_sala)
            .NotEmpty()
            .WithMessage("A sala deve possuir um tipo definido (ex: 2D, 3D, IMAX, VIP).")
            .MaximumLength(50)
            .WithMessage("O tipo da sala deve conter no máximo 50 caracteres.");

        RuleFor(s => s.capacidade)
            .GreaterThan(0)
            .WithMessage("A capacidade da sala deve ser maior que zero.")
            .LessThanOrEqualTo(500)
            .WithMessage("A capacidade máxima da sala é de 500 lugares.");

        RuleFor(s => s.assentos_ocupados)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O número de assentos ocupados não pode ser negativo.")
            .Must((dto, ocupados) => ocupados <= dto.capacidade)
            .WithMessage("O número de assentos ocupados não pode ser maior que a capacidade total da sala.");
    }
}