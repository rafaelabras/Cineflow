using Cineflow.dtos.pessoas;
using Cineflow.@interface;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Cineflow.validators;
using Cineflow.commons;
using System.Text;
using Cineflow.models.pessoas;
using Cineflow.utils;
using Cineflow.helpers;
using System.Net;
using System.Collections;
using Cineflow.@interface.IClienteRepository;
using Microsoft.IdentityModel.Tokens;

namespace Cineflow.services
{
    public class ClienteService : IClienteService
    {
        private readonly AESCryptoHelper aesCryptoHelper;
        private readonly BCryptHelper bcryptHelper;
        private readonly IClienteRepository _pessoaRepository;
        public ClienteService(IClienteRepository pessoaRepository, BCryptHelper _bcrypthelper, AESCryptoHelper _aesCryptoHelper)
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
                nome = dto.nome,
                CPF = cpfCriptografado,
                genero = dto.genero,
                senhaHash = senhaHash,
                email = dto.email,
                data_nascimento = dto.data_nascimento,
                telefone = dto.telefone
            };

            var addCliente = await _pessoaRepository.AddAsyncCliente(cliente);

            if (addCliente == 0)
            {
                return Result<RetornarClienteDto>.Failure("Não foi possível criar o cliente");
            }

            return Result<RetornarClienteDto>.Success(new RetornarClienteDto
            {
                ID = cliente.ID,
                nome = dto.nome,
                email = dto.email,
                genero = dto.genero,
                data_nascimento = dto.data_nascimento,
                telefone = dto.telefone
            }
            );
        }

        public async Task<Result<IEnumerable<RetornarClienteDto>>> ReturnAllClientesAsync()
        {
            var clientes = await _pessoaRepository.ReturnAsyncAllClientes();
            if(clientes.IsNullOrEmpty())
            {
                return Result<IEnumerable<RetornarClienteDto>>.Failure("Nenhum cliente foi Encontrado");
            }
            return Result<IEnumerable<RetornarClienteDto>>.Success(clientes);
        }

        public async Task<Result<IEnumerable<RetornarClienteDto>>> ReturnClienteByIdAsync(Guid id)
        {
            var cliente = await _pessoaRepository.ReturnAsyncClienteById(id);
            
            if(cliente.IsNullOrEmpty())
            {
                return Result<IEnumerable<RetornarClienteDto>>.Failure("Nenhum cliente foi Encontrado");
            }
            return Result<IEnumerable<RetornarClienteDto>>.Success(cliente);
            
        }

        public async Task<Result<bool>> DeleteClienteAsync(string id)
        {
            var cliente = await _pessoaRepository.RemoveCliente(id);

            if (!cliente == true)
            {
                return Result<bool>.Failure("Não foi possível deletar o cliente.");
            }
            
            return Result<bool>.Success(true);
        }

        public async Task<Result<RetornarClienteDto>> PutClienteAsync(Guid ID, CriarClienteDto dto)
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
                ID = ID,
                nome = dto.nome,
                CPF = cpfCriptografado,
                genero = dto.genero,
                senhaHash = senhaHash,
                email = dto.email,
                data_nascimento = dto.data_nascimento,
                telefone = dto.telefone
            };
            
            bool resultDatabase = await _pessoaRepository.PutAsyncCliente(cliente);

            if (!resultDatabase == true)
            {
                return Result<RetornarClienteDto>.Failure("Não foi possível salvar " +
                                                          "a atualização de dados do cliente.");
            }

            return Result<RetornarClienteDto>.Success(new RetornarClienteDto
            {
                ID = cliente.ID,
                nome = dto.nome,
                email = dto.email,
                data_nascimento = dto.data_nascimento,
                genero = dto.genero,
            });

        }
    }
}
