using Cineflow.dtos.pessoas;
using Cineflow.utils;

namespace Cineflow.helpers
{
    public class BCryptHelper
    {
        // o bcrypt é uma função de hash adaptativa para senhas, projetada para ser computacionalmente intensiva para proteger contra ataques de força bruta e rainbow tables através do uso de sal (salt) e um fator de custo ajustável.
        // o salt é gerado internamente pela biblioteca abstraindo a complexidade

        private readonly ILogger<BCryptHelper> _logger;
        public BCryptHelper(ILogger<BCryptHelper> logger)
        {
            _logger = logger;
        }

        public string EncryptPassword(CriarPessoaDto dto)
        {
            try
            {
                var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.senha, 13);
                return hashedPassword;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro no processo de hash: ");
                throw new Exception("Erro no processo de hash:");
            }

        }   

        public void VerifyPassword(CriarPessoaDto dto)
        {
            try
            {
            bool SenhaCorreta = BCrypt.Net.BCrypt.EnhancedVerify(dto.Id, "hash senha");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro no processo de verificação: ");
                throw new Exception("Erro no processo de verificação:");
            }
        }

    }
}
