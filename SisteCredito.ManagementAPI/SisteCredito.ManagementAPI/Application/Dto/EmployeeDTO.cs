using SisteCredito.ManagementAPI.Domain.Models;

namespace SisteCredito.ManagementAPI.Application.Dto
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public int? IdSupervisor { get; set; }
        public int IdHumanGestor { get; set; }
        public int IdCharge { get; set; }
        public string Name { get; set; } = null!;
        public string Document { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string SupervisorDocument { get; set; }

        public virtual ChargeDTO Charge { get; set; } = null!;
        public virtual EmployeeDTO HumanGestor { get; set; } = null!;
        public virtual EmployeeDTO Supervisor { get; set; } = null!;
    }
}
