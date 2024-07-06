namespace SisteCredito.ManagementAPI.Application.Dto
{
    public class ExtraHourDTO
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public string? Status { get; set; }
        public string Observations { get; set; } = null!;
        public int QuantityHours { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
