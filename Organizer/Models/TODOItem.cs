using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organizer.Models
{
    public class TODOItem
    {
        public int Id { get; set; }
        [ForeignKey("User")] public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}