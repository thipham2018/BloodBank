using System;
using System.Collections.Generic;

#nullable disable

namespace BloodBank.Models
{
    public partial class Account
    {
        public Account()
        {
            RegisterDonors = new HashSet<RegisterDonor>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<RegisterDonor> RegisterDonors { get; set; }
    }
}
