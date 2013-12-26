using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RoadTrip_4.Data
{
    public class RoadTripContext : DbContext 
    {
        public RoadTripContext()
            : base("DefaultConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RoadTrip> RoadTrips { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<PersonToRoadTripMap> PersonToRoadTripMaps { get; set; }

    }
}