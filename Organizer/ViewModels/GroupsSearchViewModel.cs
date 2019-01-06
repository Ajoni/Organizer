using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class GroupsSearchViewModel
    {
        public string Query { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}