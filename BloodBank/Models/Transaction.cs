using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace BloodBank.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderNo { get; set; }
        public string OrderBy { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public DateTime ShippingDate { get; set; }
        public bool Type { get; set; }
    }
}
