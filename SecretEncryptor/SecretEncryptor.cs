using Hope.Random;
using Hope.Security.SymmetricEncryption.CrossPlatform;
using Hope.Security.SymmetricEncryption.DotNetSymmetric;

namespace SecretEncryptor
{
    public sealed class SecretEncryptor : CrossPlatformEncryptor<AesEncryptor, AesEncryptor>
    {
        protected override bool IsEphemeral => false;

        public SecretEncryptor(params object[] encryptors) : base(encryptors)
        {
        }

        public SecretEncryptor(AdvancedSecureRandom secureRandom) : base(secureRandom)
        {
        }
    }
}