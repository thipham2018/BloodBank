using System;
using System.Collections.Generic;

#nullable disable

namespace BloodBank.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
