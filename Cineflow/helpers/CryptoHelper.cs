using Cineflow.dtos.pessoas;
using System.Security.Cryptography;
using System.Text;

namespace Cineflow.utils
{
    // Criptografia AES Simetrica para o CPF, para senha sera utilizado o bcrypt *não simetrica*
    public class CryptoHelper
    {
        string aesKey = Environment.GetEnvironmentVariable("AES_KEY");
        string aesIV = Environment.GetEnvironmentVariable("AES_IV");
        public string EncryptAesCpf(CriarPessoaDto Pessoa)
        {
            var encryptedCpf = Encrypt(Pessoa.Id, Encoding.UTF8.GetBytes(aesKey), Encoding.UTF8.GetBytes(aesIV));
            return encryptedCpf;
        }

        public string DecryptAesCpf(string Cpf)
        {
            var decryptedCpf = Decrypt(Cpf, Encoding.UTF8.GetBytes(aesKey), Encoding.UTF8.GetBytes(aesIV));
            return decryptedCpf;
        }

        // implementar criptografia AES com IV e key
        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            using var memoryStream = new MemoryStream();
            memoryStream.Write(iv, 0, iv.Length);

            using (var encryptor = aes.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(plainText);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string Decrypt(string cypherText, byte[] key, byte[] IV)
        {
            try
            {
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao descriptografar: " + ex.Message);
            }

        }
}
}