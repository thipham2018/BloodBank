using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public partial class GroupBloodBank
    {
        public GroupBloodBank() { }

        public GroupBloodBank(int id, string group, int count)
        {
            Id = id;
            Group = group;
            Count = count;
        }

        public int Id { get; set; }
        public String Group { get; set; }
        public int Count { get; set; }
    }
}
