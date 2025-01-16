using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class InspectionPlan : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Kpi> Kpis { get; set; }
    }
}
