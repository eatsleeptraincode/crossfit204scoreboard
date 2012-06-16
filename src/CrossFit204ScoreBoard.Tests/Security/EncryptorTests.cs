using CrossFit204ScoreBoard.Web.Security;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Security
{
    [TestFixture]
    public class EncryptorTests
    {
        private const string StuffToEncrypt = "Stuff to Encrypt";
        private IEncryptor encryptor;
        private EncryptionSettings settings = new EncryptionSettings
                                                  {
                                                      HashAlgorithm = "SHA1",
                                                      InitVector = "InitVector123456",
                                                      KeySize = 256,
                                                      PassPhrase = "PassPhrase",
                                                      PasswordIterations = 2,
                                                      SaltValue = "SaltValue"
                                                  };
        [TestFixtureSetUp]
        public void SetUp()
        {
            encryptor = new Encryptor(settings);
        }

        [Test]
        public void Encrypts()
        {
            var result = encryptor.Encrypt(StuffToEncrypt);
            result.ShouldNotEqual(StuffToEncrypt);
            result.ShouldEqual(encryptor.Encrypt(StuffToEncrypt));
        }

        [Test]
        public void Decrypts()
        {
            encryptor.Decrypt(encryptor.Encrypt(StuffToEncrypt)).ShouldEqual(StuffToEncrypt);
        }
    }
}