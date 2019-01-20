using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.ViewModels
{
    public class SummaryViewModel
    {
        public List<Note> Notes { get; set; }
        public List<TODOItem> TODOs { get; set; }
        public List<UserEvent> UserEvents { get; set; }

        public SummaryViewModel(List<Note> notes, List<TODOItem> tODOItems, List<UserEvent> userEvents)
        {
            Notes = notes;
            TODOs = tODOItems;
            UserEvents = userEvents;
        }
    }
}