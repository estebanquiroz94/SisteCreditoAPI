namespace SisteCredito.ManagementAPI.Application.Interfaces
{
    public interface IEncryptDecryptService
    {
        string Encrypt(string pass);
        string Decrypt(string pass);
    }
}
