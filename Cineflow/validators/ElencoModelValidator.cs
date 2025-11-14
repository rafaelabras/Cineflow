using Cineflow.dtos.cinema;
using FluentValidation;
using System.Globalization;

namespace Cineflow.validators
{
    public class ElencoModelValidator : AbstractValidator<CriarElencoDto>
    {
        public ElencoModelValidator()
        {
            RuleFor(e => e.nome)
                .NotEmpty().WithMessage("O elenco precisa ter um nome.")
                .MinimumLength(3).WithMessage("O nome deve possuir pelo menos 3 caracteres.");
            
            RuleFor(e => e.genero)
                .NotEmpty().WithMessage("O gênero deve ser informado.");

            RuleFor(e => e.data_nascimento)
                .NotEmpty().WithMessage("A data de nascimento deve ser informada.")
                .Must(DataNascimentoValida).WithMessage("A data de nascimento não pode ser no futuro.");

            RuleFor(e => e.nacionalidade)
                .NotEmpty().WithMessage("A nacionalidade deve ser informada.")
                .MinimumLength(3).WithMessage("A nacionalidade deve possuir pelo menos 3 caracteres.");
        }

        private bool DataNascimentoValida(DateTime? data)
        {
            if (!data.HasValue) return false;
            return data.Value <= DateTime.Today;
        }
    }
}