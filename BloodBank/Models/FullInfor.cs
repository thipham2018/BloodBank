using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public partial class FullInfor
    {
        public FullInfor() { }
        public FullInfor(string fullName, string contactNo, string address, string image, string bloodname)
        {
            FullName = fullName;
            ContactNo = contactNo;
            Address = address;
            Image = image;
            Bloodname = bloodname;
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public int BloodGroupId { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string Bloodname { get; set; }



    }
}
