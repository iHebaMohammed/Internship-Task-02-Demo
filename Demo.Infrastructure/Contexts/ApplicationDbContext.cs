using Demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<InspectionPlan> InspectionPlans { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Kpi> Kpis { get; set; }
        public DbSet<InspectionPlanKpis> InspectionPlanKpis { get; set; }
        public DbSet<InspectionPlanLocations> InspectionPlanLocations { get; set; }
        public DbSet<ProjectLocations> ProjectLocations { get; set; }
    }
}
