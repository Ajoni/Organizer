using Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class GroupsIndexViewModel
    {
        [DisplayName("Groups you created")]
        public List<Group> OwnGroups { get; set; }

        [DisplayName("Groups you're observing")]
        public List<Group> ObservedGroups { get; set; }

        [DisplayName("Groups you're administering")]
        public List<Group> AdministeredGroups { get; set; }
    }
}