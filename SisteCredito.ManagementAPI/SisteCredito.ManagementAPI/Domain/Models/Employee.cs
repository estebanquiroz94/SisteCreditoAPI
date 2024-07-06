using System;
using System.Collections.Generic;

namespace SisteCredito.ManagementAPI.Domain.Models
{
    public partial class Employee
    {
        public Employee()
        {
            AreaIdHumangestorNavigations = new HashSet<Area>();
            AreaIdSupervisorNavigations = new HashSet<Area>();
            InverseIdSupervisorNavigation = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public int? IdSupervisor { get; set; }
        public int IdCharge { get; set; }
        public string Name { get; set; } = null!;
        public string Document { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual Charge IdChargeNavigation { get; set; } = null!;
        public virtual Employee? IdSupervisorNavigation { get; set; }
        public virtual ICollection<Area> AreaIdHumangestorNavigations { get; set; }
        public virtual ICollection<Area> AreaIdSupervisorNavigations { get; set; }
        public virtual ICollection<Employee> InverseIdSupervisorNavigation { get; set; }
    }
}
