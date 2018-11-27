using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Data
{
    public interface IDbContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<TODOItem> TODOItems { get; set; }
        DbSet<GroupEvent> GroupEvents { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<UserEvent> UserEvents { get; set; }
    }
}
