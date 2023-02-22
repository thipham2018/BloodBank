using System;
using System.Collections.Generic;

#nullable disable

namespace BloodBank.Models
{
    public partial class RegisterDonor
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public int BloodGroupId { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }

        public virtual Account Account { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
    }
}
