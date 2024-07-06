using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.Application.Dto
{
    public class AreaDTO
    {
        public int Id { get; set; }
        public int? IdHumangestor { get; set; }
        public int? IdSupervisor { get; set; }
        public string HumanGestorDocument { get; set; }
        public string SupervisorDocument { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public EmployeeDTO HumanGestor { get; set; } = null!;
    }
}
