using Cineflow.dtos.cinema;
using FluentValidation;

namespace Cineflow.validators;

public class AssentoModelValidator : AbstractValidator<CriarAssentoDto>
{
    public AssentoModelValidator()
    {
        RuleFor(a => a.Id_sala)
            .GreaterThan(0)
            .WithMessage("O assento precisa estar vinculado a uma sala válida.");

        RuleFor(a => a.fila)
            .NotNull().WithMessage("O assento deve conter uma fila (A, B, C...).")
            .Must(f => char.IsLetter(f.Value))
            .WithMessage("A fila deve ser representada por uma letra.");

        RuleFor(a => a.numero)
            .NotNull().WithMessage("O assento deve conter um número.")
            .GreaterThan(0).WithMessage("O número do assento deve ser maior que zero.");

        RuleFor(a => a.status)
            .NotNull()
            .WithMessage("O status do assento deve ser informado.");
    }
}