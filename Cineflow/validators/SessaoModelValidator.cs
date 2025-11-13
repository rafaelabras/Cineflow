using Cineflow.dtos.cinema;
using FluentValidation;

namespace Cineflow.validators;

public class SessaoModelValidator : AbstractValidator<CriarSessaoDto>
{
    public SessaoModelValidator()
    {
        RuleFor(s => s.Id_filme)
            .NotNull().WithMessage("O campo Id_filme é obrigatório.")
            .GreaterThan(0).WithMessage("O Id_filme deve ser maior que zero.");

        RuleFor(s => s.Id_sala)
            .NotNull().WithMessage("O campo Id_sala é obrigatório.")
            .GreaterThan(0).WithMessage("O Id_sala deve ser maior que zero.");

        RuleFor(s => s.data_sessao)
            .NotNull().WithMessage("A data da sessão é obrigatória.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("A data da sessão não pode ser no passado.");

        RuleFor(s => s.horario_inicio)
            .NotNull().WithMessage("O horário de início é obrigatório.");

        RuleFor(s => s.horario_fim)
            .NotNull().WithMessage("O horário de fim é obrigatório.")
            .GreaterThan(s => s.horario_inicio)
            .WithMessage("O horário de fim deve ser posterior ao horário de início.");

        RuleFor(s => s.preco_sessao)
            .NotNull().WithMessage("O preço da sessão é obrigatório.")
            .GreaterThan(0).WithMessage("O preço da sessão deve ser maior que zero.");

        RuleFor(s => s.idioma_audio)
            .IsInEnum().When(s => s.idioma_audio.HasValue)
            .WithMessage("O idioma de áudio informado é inválido.");

        RuleFor(s => s.idioma_legenda)
            .IsInEnum().When(s => s.idioma_legenda.HasValue)
            .WithMessage("O idioma de legenda informado é inválido.");
    }
}