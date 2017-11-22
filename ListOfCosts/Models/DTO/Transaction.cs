using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.DTO
{
    public class Transaction
    {
        public int Id { get; set; }
        public Resource Resource { get; set; }
        public Cost Cost { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public Transaction(Resource _r, Cost _c, double _a, DateTime _d, string _com)
        {
            Resource = _r;
            Cost = _c;
            Amount = _a;
            Date = _d;
            Comment = _com;
        }

        public Transaction()
            : this(new Resource(), new Cost(), 0.00, DateTime.Now, "")
        {
        }
    }
}
