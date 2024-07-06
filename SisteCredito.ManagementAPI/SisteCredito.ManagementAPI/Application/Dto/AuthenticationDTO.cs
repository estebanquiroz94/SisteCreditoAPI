namespace SisteCredito.ManagementAPI.Application.Dto
{
    public class AuthenticationDTO
    {
        public string Key { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public TokenConfigurationDto TokenConfiguration { get; set; }
    }
}
