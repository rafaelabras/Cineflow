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

        public async Task<IEnumerable<RetornarClienteDto>> ReturnAsyncClienteById(Guid id)
        {
            var sql = @"SELECT id, nome, genero, email, data_nascimento, telefone FROM PESSOA
             WHERE id = @ID";

            return await _databaseService.QueryAsync<RetornarClienteDto>(sql, new { ID = id });
        }

        public async Task<bool> RemoveCliente(string Id)
        {
            var sql = @"DELETE FROM pessoa WHERE id = @ID";

            var execute = await _databaseService.ExecuteAsync(sql, new { ID = Id });
            if (execute == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PutAsyncCliente(Cliente cliente)
        {
            var verify = await VerificarExistencia(cliente.ID);
            
            if (!verify)
                return false;
            
            var sql = @"UPDATE ONLY pessoa SET cpf = @CPF, nome = @nome, email = @email
               ,genero = @genero, password_hash = @senha, data_nascimento = @data_nascimento, telefone = @telefone
               WHERE id = @ID";
            
            var parameters = new
            {
                CPF = cliente.CPF,
                nome = cliente.nome,
                email = cliente.email,
                genero = cliente.genero,
                senha = cliente.senhaHash,
                data_nascimento = cliente.data_nascimento,
                telefone = cliente.telefone,
                ID = cliente.ID
            };
            
            var result = await _databaseService.ExecuteAsync(sql, parameters);
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> VerificarExistencia(Guid ID)
        {
            var sql = "SELECT id FROM pessoa WHERE id = @id";
        
            var result = await _databaseService.QueryAsync<RetornarClienteDto>(sql, new { id = ID });

            if (result.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
    
}
