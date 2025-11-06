using Cineflow.dtos.pessoas;
using Cineflow.@interface;
using Cineflow.@interface.IClienteRepository;
using Cineflow.models.pessoas;

namespace Cineflow.repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DatabaseService _databaseService;
        public ClienteRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<int> AddAsyncCliente(Cliente cliente)
        {
            var sql = @"INSERT INTO PESSOA (id, cpf, nome, email, genero, password_hash, data_nascimento, telefone)
                       VALUES (@ID, @CPF, @nome, @email, @genero, @senhaHash, @data_nascimento, @telefone);";

            return await _databaseService.ExecuteAsync(sql, cliente);
        }

        public async Task<IEnumerable<RetornarClienteDto>> ReturnAsyncAllClientes()
        {
            var sql = @"SELECT id, nome, genero, email, data_nascimento, telefone FROM PESSOA";

            return await _databaseService.QueryAsync<RetornarClienteDto>(sql);
        }

        public async Task<bool> RemoveCliente(string ID)
        {
            var sql = @"DELETE FROM pessoa WHERE id = @ID";

            var execute = _databaseService.ExecuteAsync(sql, ID);
            if (execute.Result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
