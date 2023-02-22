using System;
using System.Collections.Generic;

#nullable disable

namespace BloodBank.Models
{
    public partial class BloodGroup
    {
        public BloodGroup()
        {
            RegisterDonors = new HashSet<RegisterDonor>();
        }

        public int Id { get; set; }
        public string Bloodname { get; set; }

        public virtual ICollection<RegisterDonor> RegisterDonors { get; set; }
    }
}
