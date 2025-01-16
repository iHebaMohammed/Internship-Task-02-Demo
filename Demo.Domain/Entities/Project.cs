using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateOnly StartDate { get; set; } 
    }
}
