using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class UsersFindViewModel
    {
        public string Query { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}