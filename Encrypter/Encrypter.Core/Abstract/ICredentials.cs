namespace Encrypter.Core
{
    public interface ICredentials
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}
