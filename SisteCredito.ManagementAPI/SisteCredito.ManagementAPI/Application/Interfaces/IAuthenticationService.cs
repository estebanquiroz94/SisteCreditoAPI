using Microsoft.AspNetCore.Identity;

namespace SisteCredito.ManagementAPI.Application.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateToken(string username, string password);
        Task<SignInResult> Authenticate(string username, string password);
    }
}
