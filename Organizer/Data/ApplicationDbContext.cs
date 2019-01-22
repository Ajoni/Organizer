using Microsoft.AspNet.Identity.EntityFramework;
using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Organizer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<TODOItem> TODOItems { get; set; }
        public DbSet<GroupEvent> GroupEvents { get; set; }
        public DbSet<Group> Groups { get; set; } 
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                        .HasMany<Group>(g => g.GroupObservations)
                        .WithMany(c => c.Observers)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("UserRefId");
                            cs.MapRightKey("GroupRefId");
                            cs.ToTable("UserGroupObservation");
                        });

            modelBuilder.Entity<ApplicationUser>()
            .HasMany<Group>(g => g.AdministratedGroups)
            .WithMany(c => c.Administrators)
            .Map(cs =>
            {
                cs.MapLeftKey("UserRefId");
                cs.MapRightKey("GroupRefId");
                cs.ToTable("UserGroupAdministration");
            });

            modelBuilder.Entity<ApplicationUser>()
            .HasMany<ApplicationUser>(g => g.UserObservations)
            .WithMany(c => c.ObservingUsers)
            .Map(cs =>
            {
                cs.MapLeftKey("ObservingUserRefId");
                cs.MapRightKey("ObservedUserRefId");
                cs.ToTable("UserToUserObservation");
            });

        }

    }
}