using Microsoft.Extensions.Options;
using SisteCredito.ManagementAPI.Application.Dto;
using SisteCredito.ManagementAPI.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SisteCredito.ManagementAPI.Application.Services
{
    public class EncryptDecryptService : IEncryptDecryptService
    {
        private AuthenticationDTO Authentication;

        public EncryptDecryptService(IOptionsMonitor<AuthenticationDTO> options)
        {
            Authentication = options.CurrentValue;
        }
        public string Encrypt(string pass)
        {
            byte[] keyArray;
            byte[] encriptar = Encoding.UTF8.GetBytes(pass);

            keyArray = Encoding.UTF8.GetBytes(Authentication.Key);
            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultado = cTransform.TransformFinalBlock(encriptar, 0, encriptar.Length);

            return Convert.ToBase64String(resultado, 0, resultado.Length);
        }

        public string Decrypt(string pass)
        {
            byte[] keyArray;
            byte[] decriptar = Convert.FromBase64String(pass);

            keyArray = Encoding.UTF8.GetBytes(Authentication.Key);
            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultado = cTransform.TransformFinalBlock(decriptar, 0, decriptar.Length);

            return Encoding.UTF8.GetString(resultado);
        }
    }
}
