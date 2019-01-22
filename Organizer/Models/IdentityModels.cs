using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Organizer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class,
    // please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<UserEvent> Events { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<TODOItem> TODOItems { get; set; }

        public virtual ICollection<ApplicationUser> UserObservations { get; set; }
        public virtual ICollection<ApplicationUser> ObservingUsers { get; set; }
        public virtual ICollection<Group> GroupObservations { get; set; }
        public virtual ICollection<Group> AdministratedGroups { get; set; }

        public virtual ICollection<UserFile> Files { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}