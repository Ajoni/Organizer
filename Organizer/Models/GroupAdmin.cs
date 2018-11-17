using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class GroupAdmin
    {
        [ForeignKey("User")] public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Group")] public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}