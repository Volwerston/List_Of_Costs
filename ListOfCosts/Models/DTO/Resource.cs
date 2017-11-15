using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCosts.Models.DTO
{
    public class Resource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Amount { get; set; }

        public Category ResourceType { get; set; }
    }
}
