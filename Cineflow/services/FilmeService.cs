using System.Text;
using Cineflow.commons;
using Cineflow.dtos.cinema;
using Cineflow.@interface.CinemaInterfaces;
using Cineflow.@interface.IClienteRepository;
using Cineflow.models.cinema;
using Cineflow.validators;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services;

public class FilmeService : IFilmeService
{
   private readonly IFilmeRepository _filmeRepository;
   public FilmeService(IFilmeRepository filmeRepository)
   {
      _filmeRepository = filmeRepository;
   }

   public async Task<Result<CriarFilmeDto>> CriarFilmeAsync(CriarFilmeDto criarFilmeDto)
   {
      FilmeModelValidator filmeModelValidator = new FilmeModelValidator();
      var result = filmeModelValidator.Validate(criarFilmeDto);

      if (!result.IsValid)
      {
         StringBuilder sb = new StringBuilder();
         result.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));

         return Result<CriarFilmeDto>.Failure(sb.ToString());
      }

      var create = await _filmeRepository.AddFilmeAsync(criarFilmeDto);

      if (create == 1)
      {
       return Result<CriarFilmeDto>.Success(criarFilmeDto);  
      }
      
      return Result<CriarFilmeDto>.Failure("Houve um erro ao criar novo filme");
}

   public async Task<Result<IEnumerable<Filme>>> GetFilmesAsync()
   {
      var filmes = await _filmeRepository.GetFilmesAsync();

      if (filmes.IsNullOrEmpty() == false)
      {
         return Result<IEnumerable<Filme>>.Failure("Houve um erro ao retornar os filmes.");
      }
      
      return Result<IEnumerable<Filme>>.Success(filmes);
}
}