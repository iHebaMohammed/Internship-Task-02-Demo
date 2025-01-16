using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class ProjectLocations 
    {
        public int Id { get; set; }
        public Project Project { get; set; }
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public Location Location { get; set; }
        [ForeignKey("Location")]
        public Guid LocationId { get; set; }
    }
}
