using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class Event
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}