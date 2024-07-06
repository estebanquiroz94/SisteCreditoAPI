using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.Application.Dto
{
    public class ChargeDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public List<EmployeeDTO> Employees { get; set; }
    }
}
