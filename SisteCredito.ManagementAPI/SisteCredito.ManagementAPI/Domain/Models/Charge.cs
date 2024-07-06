using System;
using System.Collections.Generic;

namespace SisteCredito.ManagementAPI.Domain.Models
{
    public partial class Charge
    {
        public Charge()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
