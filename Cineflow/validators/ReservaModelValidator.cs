using Cineflow.dtos.cinema;
using Cineflow.models.enums;
using FluentValidation;

namespace Cineflow.validators
{
    public class ReservaModelValidator : AbstractValidator<CriarReservaDto>
    {
        public ReservaModelValidator()
        {
            RuleFor(r => r.Id_cliente)
                .NotEmpty().WithMessage("A reserva deve estar vinculada a um cliente válido.");

            RuleFor(r => r.Id_sessao)
                .NotEmpty().WithMessage("A reserva deve estar vinculada a uma sessão válida.");

            RuleFor(r => r.status)
                .NotNull().WithMessage("O status da reserva deve ser informado.")
                .Must(StatusValido).WithMessage("O status informado não é válido.");

            RuleFor(r => r.data_reserva)
                .NotEmpty().WithMessage("A data da reserva deve ser informada.")
                .Must(DataReservaValida).WithMessage("A data da reserva não pode estar no futuro.");
        }

        private static bool StatusValido(StatusReserva? status)
        {
            if (status == null) return false;
            return Enum.IsDefined(typeof(StatusReserva), status);
        }

        private static bool DataReservaValida(DateTime? data)
        {
            if (!data.HasValue) return false;
            return data.Value <= DateTime.Now;
        }
    }
}