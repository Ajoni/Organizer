using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class Note
    {
        [Key] public int Nr { get; set; }
        [ForeignKey("User")] public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Content { get; set; }
        [Required] public bool Visibility { get; set; }
    }
}