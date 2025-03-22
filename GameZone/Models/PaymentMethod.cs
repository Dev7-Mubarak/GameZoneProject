using System;
using System.Collections.Generic;

namespace GameZone.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
