using Cineflow.dtos.pessoas;
using FluentValidation;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Data;

namespace Cineflow.validators
{
    public class ClienteModelValidator : AbstractValidator<CriarClienteDto>
    {
        public ClienteModelValidator()
        {
            RuleFor(c => c.CPF).NotEmpty().WithMessage("É necessário inserir um CPF")
                .Length(11).WithMessage("O CPF deve ter 11 caracteres")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve conter apenas números, não inclua ponto nem hífen ");

            RuleFor(c => c.name).NotEmpty().WithMessage("É necessário inserir seu nome").MinimumLength(40)
                .WithMessage("O nome deve ter no mínimo 10 caracteres").MaximumLength(40)
                .WithMessage("O nome não pode ter mais de 50 caracteres");


            RuleFor(c => c.email).NotEmpty().WithMessage("É necessário inserir um email")
                .EmailAddress().WithMessage("O email inserido não é válido");

            RuleFor(c => c.senha).NotEmpty().WithMessage("É necessário inserir uma senha")
                .MinimumLength(10).WithMessage("A senha deve ter no mínimo 10 caracteres")
                .MaximumLength(50).WithMessage("A senha não pode ter mais de 50 caracteres")
                .Matches(@"[A-Z]+").WithMessage("A senha deve conter ao menos uma letra maiúscula");

            RuleFor(c => c.telefone)
                .NotEmpty().WithMessage("É necessário inserir um telefone")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("O telefone inserido não é válido");

            RuleFor(c => c.data_nascimento)
                .NotEmpty().WithMessage("É necessário inserir sua data de nascimento")
                .Must(SerMaiorDeIdade).WithMessage("É necessário ser maior de 18 anos");

        }

        private static bool SerMaiorDeIdade(DateTime? dataNascimento)
        {
            if (!dataNascimento.HasValue)
                return false;
            var idadeMinima = DateTime.Now.AddYears(-18);
            return dataNascimento.Value <= idadeMinima;
        }
    }
}

