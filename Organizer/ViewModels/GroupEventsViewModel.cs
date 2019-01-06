using System.Collections.Generic;
using Organizer.Models;

namespace Organizer.ViewModels
{
    public class GroupEventsViewModel
    {
        public GroupEventsViewModel()
        {
        }

        public int? GroupId { get; set; }
        public ICollection<GroupEvent> GroupEvents { get; set; }
        public bool UserIsAdmin { get; set; }
    }
}