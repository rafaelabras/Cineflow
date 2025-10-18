using Cineflow.dtos.pessoas;
using Cineflow.@interface;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Cineflow.validators;
using Cineflow.commons;
using System.Text;
using Cineflow.models.pessoas;
using Cineflow.utils;
using Cineflow.helpers;
using Cineflow.@interface.IPessoaRepository;

namespace Cineflow.services
{
    public class PessoaService : IPessoaService
    {
        private readonly AESCryptoHelper aesCryptoHelper;
        private readonly BCryptHelper bcryptHelper;
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaService(IPessoaRepository pessoaRepository, BCryptHelper _bcrypthelper, AESCryptoHelper _aesCryptoHelper)
        {
            aesCryptoHelper = _aesCryptoHelper;
            bcryptHelper = _bcrypthelper;
            _pessoaRepository = pessoaRepository; 
        }
        public async Task<Result<RetornarClienteDto>> AddClienteAsync(CriarClienteDto dto)
        {
            ClienteModelValidator validator = new ClienteModelValidator();
            var result = validator.Validate(dto);

            if (!result.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                result.Errors.ForEach(error =>
                {
                    sb.AppendLine(error.ErrorMessage);
                });


                return Result<RetornarClienteDto>.Failure(sb.ToString());
            }

            var senhaHash = bcryptHelper.HashPassword(dto.senha);
            var cpfCriptografado = aesCryptoHelper.EncryptAesCpf(dto.CPF);

            var cliente = new Cliente
            {
                ID = Guid.NewGuid(),
                name = dto.name,
                CPF = cpfCriptografado,
                genero = dto.genero,
                senhaHash = senhaHash,
                email = dto.email,
                data_nascimento = dto.data_nascimento,
                telefone = dto.telefone
            };

            var addCliente = await _pessoaRepository.AddAsyncCliente(cliente);

            return Result<RetornarClienteDto>.Success(new RetornarClienteDto
            {
                ID = cliente.ID,
                name = dto.name,
                email = dto.email,
                genero = dto.genero,
                data_nascimento = dto.data_nascimento,
                telefone = dto.telefone
            }
            );
        }



    }
}
