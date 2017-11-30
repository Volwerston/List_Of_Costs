using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.DTO
{
    public class Cost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double CurrentWaste { get; set; }
        public Category CostsType { get; set; }
        public int OwnerId { get; set; }
    }
}
