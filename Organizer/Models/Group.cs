using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class Group
    {
        [Key] public int Id { get; set; }
        [ForeignKey("Owner")] public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        [Required] public string Title { get; set; }
        public string Tags { get; set; }
        
        public virtual ICollection<GroupEvent> Events { get; set; }

        public virtual ICollection<ApplicationUser> Administrators { get; set; }

        public virtual ICollection<ApplicationUser> Observers { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}