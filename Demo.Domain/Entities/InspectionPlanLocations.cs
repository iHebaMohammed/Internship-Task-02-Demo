using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class InspectionPlanLocations
    {
        public int Id { get; set; }
        public InspectionPlan InspectionPlan { get; set; }
        [ForeignKey("InspectionPlan")]
        public Guid InspectionPlanId { get; set; }
        public Location Location { get; set; }
        [ForeignKey("Location")]
        public Guid LocationId { get; set; }

    }
}
