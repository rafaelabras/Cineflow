using System.Data;
using Cineflow.dtos.cinema;
using Cineflow.models.enums;
using FluentValidation;

namespace Cineflow.validators;

public class FilmeModelValidator : AbstractValidator<CriarFilmeDto>
{
    public FilmeModelValidator()
    {
        RuleFor(f => f.nome_filme).NotEmpty().WithMessage("Para um filme ser registrado ele precisa ter um nome.")
            .Length(10).WithMessage("O filme deve possuir pelo menos 10 caracteres.");

        RuleFor(f => f.classificacao_indicativa).NotEmpty()
            .WithMessage("O filme deve possuir uma classificação indicativa");

        RuleFor(f => f.data_lancamento).NotEmpty()
            .WithMessage("O filme deve possuir uma data de lançamento");

        RuleFor(f => f.diretor).NotEmpty()
            .WithMessage("O filme deve possuir um diretor");

        RuleFor(f => f.duracao).NotEmpty()
            .WithMessage("O filme deve possuir uma duração");

        RuleFor(f => f.genero).NotEmpty()
            .WithMessage("O filme deve possuir um genero");

        RuleFor(f => f.idioma).NotEmpty()
            .WithMessage("O filme deve possuir um idioma").Must(IdiomaValido).WithMessage("O filme deve possuir um idioma valido"); 

        RuleFor(f => f.produtora).NotEmpty()
            .WithMessage("O filme deve possuir uma produtora")
            .Length(200).WithMessage("O filme deve possuir 200 caracteres");

        RuleFor(f => f.produtora).NotEmpty()
            .WithMessage("O filme deve possuir uma sinopse");


    }

    private static bool IdiomaValido(string idioma)
    {
        Idioma idiomaEnum = new Idioma();

        foreach (Idioma i in Enum.GetValues(typeof(Idioma)))
        {
            if (i.ToString().Equals(idioma))
            {
                return true;
            }
        }

        return false;
    }

}