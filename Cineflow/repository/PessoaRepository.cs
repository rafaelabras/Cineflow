using Cineflow.@interface.IPessoaRepository;
using Cineflow.models.pessoas;

namespace Cineflow.repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DatabaseService _databaseService;
        public PessoaRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<int> AddAsyncCliente(Cliente cliente)
        {
            var sql = @"INSERT INTO PESSOA (id, cpf, nome, email, genero, password_hash, data_nascimento, telefone)
                       VALUES (@ID, @CPF, @name, @email, @genero, @senhaHash, @data_nascimento, @telefone);";

            return await _databaseService.ExecuteAsync(sql, cliente);
        }
    }
}
