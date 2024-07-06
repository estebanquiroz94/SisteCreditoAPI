using System;
using System.Collections.Generic;

namespace SisteCredito.ManagementAPI.Domain.Models
{
    public partial class Area
    {
        public int Id { get; set; }
        public int IdHumangestor { get; set; }
        public int IdSupervisor { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual Employee IdHumangestorNavigation { get; set; } = null!;
        public virtual Employee IdSupervisorNavigation { get; set; } = null!;
    }
}
