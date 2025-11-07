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
     var result = Validate(criarFilmeDto);

     if (!result.IsSuccess)
         return result;
     
      var create = await _filmeRepository.AddFilmeAsync(criarFilmeDto);

      if (create == 1)
         return result;
      
      return Result<CriarFilmeDto>.Failure("Houve um erro ao criar novo filme");
      
   }

   public async Task<Result<IEnumerable<Filme>>> GetFilmeByIdAsync(int ID)
   {
      var query = await _filmeRepository.GetFilmesByIDAsync(ID);

      if (query.IsNullOrEmpty())
      {
         return Result<IEnumerable<Filme>>.Failure("Nenhum filme foi encontrado");
      }
      return Result<IEnumerable<Filme>>.Success(query);
}

   public async Task<Result<bool>> DeleteFilmeAsync(int ID)
   {
      var query = await _filmeRepository.DeleteFilmeAsync(ID);
      
      if (!query)
         return Result<bool>.Failure("Não foi possível deletar o filme");
      
      return Result<bool>.Success(true);
   }

   public async Task<Result<CriarFilmeDto>> PutFilmeAsync(CriarFilmeDto criarFilmeDto)
   {
      var result = Validate(criarFilmeDto);

      if (!result.IsSuccess)
         return result;
     
      var create = await _filmeRepository.PutFilmeAsync(criarFilmeDto);

      if (create == 1)
         return result;
      
      return Result<CriarFilmeDto>.Failure("Houve um erro ao atualizar o filme");
   }

   public async Task<Result<IEnumerable<Filme>>> GetFilmesAsync()
   {
      var filmes = await _filmeRepository.GetFilmesAsync();

      if (filmes.IsNullOrEmpty())
      {
         return Result<IEnumerable<Filme>>.Failure("Nenhum filme foi encontrado");
      }
      
      return Result<IEnumerable<Filme>>.Success(filmes);
}

   private Result<CriarFilmeDto> Validate(CriarFilmeDto criarFilmeDto)
   {
      FilmeModelValidator filmeModelValidator = new FilmeModelValidator();
      var result = filmeModelValidator.Validate(criarFilmeDto);

      if (!result.IsValid)
      {
         StringBuilder sb = new StringBuilder();
         result.Errors.ForEach(x => sb.AppendLine(x.ErrorMessage));

         return Result<CriarFilmeDto>.Failure(sb.ToString());
      }
      return Result<CriarFilmeDto>.Success(criarFilmeDto);
   }
}