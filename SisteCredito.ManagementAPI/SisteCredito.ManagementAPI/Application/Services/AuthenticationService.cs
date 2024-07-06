using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SisteCredito.ManagementAPI.Application.Dto;
using SisteCredito.ManagementAPI.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SisteCredito.ManagementAPI.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private AuthenticationDTO _authentication;
        private IEncryptDecryptService _encryptDecryptService;

        public AuthenticationService(
            IEncryptDecryptService encryptDecryptService, IOptionsMonitor<AuthenticationDTO> options)
        {
            _encryptDecryptService = encryptDecryptService;
            _authentication = options.CurrentValue;
        }

        public async Task<SignInResult> Authenticate(string userName, string pass)
        {
            var decryptPass = _encryptDecryptService.Decrypt(pass);

            if (userName == _authentication.Username && decryptPass == _authentication.Password)
            {
                return SignInResult.Success;
            }
            else
            { return SignInResult.Failed; }
        }

        public string GenerateToken(string userName, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_authentication.TokenConfiguration.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),

                Expires = DateTime.UtcNow.AddMinutes(_authentication.TokenConfiguration.Expire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
