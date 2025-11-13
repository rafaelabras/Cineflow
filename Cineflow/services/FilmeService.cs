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

   public async Task<Result<Filme>> CriarFilmeAsync(CriarFilmeDto criarFilmeDto)
   {
     var result = Validate(criarFilmeDto);

     if (!result.IsSuccess)
        return Result<Filme>.Failure("Não foi possível criar o filme.");
     
      var create = await _filmeRepository.AddFilmeAsync(criarFilmeDto);

      if (create > 1)
         return Result<Filme>.Success(fromDtoToFilme(create, criarFilmeDto));
      
      return Result<Filme>.Failure("Houve um erro ao criar novo filme");
      
   }

   public async Task<Result<bool>> DeleteFilmeAsync(int ID)
   {
      var query = await _filmeRepository.DeleteFilmeAsync(ID);
      
      if (!query)
         return Result<bool>.Failure("Não foi possível deletar o filme");
      
      return Result<bool>.Success(true);
   }

   public async Task<Result<CriarFilmeDto>> PutFilmeAsync(int id,CriarFilmeDto filme)
   {
      var result = Validate(filme);

      if (!result.IsSuccess)
         return result;
      
      var filmeCreate = fromDtoToFilme(id, filme);
      
     
      var create = await _filmeRepository.PutFilmeAsync(filmeCreate);

      if (create == 1)
         return result;
      
      return Result<CriarFilmeDto>.Failure("Houve um erro ao atualizar o filme");
   }

   public async Task<Result<IEnumerable<Filme>>> GetFilmesAsync(FilmeFiltroDto filtro)
   {
      var filmes = await _filmeRepository.GetFilmesAsync(filtro);

      if (filmes.IsNullOrEmpty())
      {
         return Result<IEnumerable<Filme>>.Failure("Nenhum filme foi encontrado");
      }
      
      return Result<IEnumerable<Filme>>.Success(filmes);
}

   private Filme fromDtoToFilme(int id, CriarFilmeDto filme)
   {
      return new Filme
         { ID = id,
            nome_filme = filme.nome_filme,
            sinopse = filme.sinopse,
            genero = filme.genero,
            duracao = filme.duracao,
            classificacao_indicativa = filme.classificacao_indicativa,
            idioma = filme.idioma,
            pais_origem = filme.pais_origem,
            produtora = filme.produtora,
            data_lancamento = filme.data_lancamento,
            diretor = filme.diretor,
            media_avaliacoes = filme.media_avaliacoes,
            numero_avaliacoes = filme.numero_avaliacoes };
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