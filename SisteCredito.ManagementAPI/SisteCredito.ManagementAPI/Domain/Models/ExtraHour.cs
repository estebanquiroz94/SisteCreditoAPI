using System;
using System.Collections.Generic;

namespace SisteCredito.ManagementAPI.Domain.Models
{
    public partial class ExtraHour
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public string? Status { get; set; }
        public string Observations { get; set; } = null!;
        public int QuantityHours { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
