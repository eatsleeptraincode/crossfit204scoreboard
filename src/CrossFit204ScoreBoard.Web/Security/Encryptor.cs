using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CrossFit204ScoreBoard.Web.Security
{
    public interface IEncryptor
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }

    public class Encryptor : IEncryptor
    {
        private readonly ICryptoTransform encryptor;
        private readonly ICryptoTransform decryptor;

        public Encryptor(EncryptionSettings settings)
        {
            var symmetricKey = CreateSymmetricKey();
            var keyBytes = GetKeyBytes(settings);
            var initVectorBytes = Encoding.ASCII.GetBytes(settings.InitVector);

            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        }

        public string Encrypt(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            var cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        public string Decrypt(string cipherText)
        {
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            var plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }

        static byte[] GetKeyBytes(EncryptionSettings settings)
        {
            var saltBytes = Encoding.ASCII.GetBytes(settings.SaltValue);
            var password = new Rfc2898DeriveBytes(settings.PassPhrase, saltBytes, settings.PasswordIterations);
            var keyBytes = password.GetBytes(settings.KeySize/8);
            return keyBytes;
        }

        static RijndaelManaged CreateSymmetricKey()
        {
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            return symmetricKey;
        }
    }

    public class EncryptionSettings
    {
        public string InitVector { get; set; }
        public string SaltValue { get; set; }
        public string PassPhrase { get; set; }
        public string HashAlgorithm { get; set; }
        public int PasswordIterations { get; set; }
        public int KeySize { get; set; }
    }
}