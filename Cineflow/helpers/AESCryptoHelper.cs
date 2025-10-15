using Cineflow.dtos.pessoas;
using System.Security.Cryptography;
using System.Text;

namespace Cineflow.utils
{
    // Criptografia AES Simetrica para o CPF, para senha sera utilizado o bcrypt *não simetrica*
    public class AESCryptoHelper
    {
        string aesKey = Environment.GetEnvironmentVariable("AES_KEY");
        const int IVLength = 16; // AES block size em bytes
        private readonly ILogger<AESCryptoHelper> _logger;
        public AESCryptoHelper(ILogger<AESCryptoHelper> logger)
        {
            _logger = logger;
        }

        public string EncryptAesCpf(CriarPessoaDto Pessoa)
        {
            var encryptedCpf = Encrypt(Pessoa.Id, Encoding.UTF8.GetBytes(aesKey));
            return encryptedCpf;
        }

        public string DecryptAesCpf(string Cpf)
        {
            var decryptedCpf = Decrypt(Cpf, Encoding.UTF8.GetBytes(aesKey));
            return decryptedCpf;
        }

        // implementar criptografia AES com IV e key
        public string Encrypt(string plainText, byte[] key)
        { 
            try
            {
                using var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = RandomNumberGenerator.GetBytes(IVLength);

                using var memoryStream = new MemoryStream();
                memoryStream.Write(aes.IV, 0, aes.IV.Length);

                using (var encryptor = aes.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (CryptographicException ex)
            {
                _logger.LogError(ex, "Erro ao criptografar: ");
                throw new Exception("Erro ao criptografar:");
            }
        }

        public string Decrypt(string cypherText, byte[] key)
        {
            try
            {
                byte[] IV = new byte[IVLength];
                byte[] cipherData = Convert.FromBase64String(cypherText);

                if (cipherData.Length < IV.Length)
                    throw new ArgumentException("Ciphertext is too short.");

                byte[] iv = new byte[IV.Length];
                byte[] encryptedData = new byte[cipherData.Length - IV.Length];

                Buffer.BlockCopy(cipherData, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(cipherData, iv.Length, encryptedData, 0, encryptedData.Length);

                using var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using MemoryStream memoryStream = new MemoryStream(encryptedData);
                using var decryptor = aes.CreateDecryptor();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader reader = new StreamReader(cryptoStream);

                return reader.ReadToEnd();

            }
            catch (CryptographicException ex)
            {
                _logger.LogError(ex, "Erro ao descriptografar: ");
                throw new Exception("Erro ao descriptografar: ");
            }

        }
}
}