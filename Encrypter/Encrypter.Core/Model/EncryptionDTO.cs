namespace Encrypter.Core
{
    public class EncryptionDTO : ICredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string InputFileName { get; set; }
        public string OutputFileName { get; set; }
    }
}
