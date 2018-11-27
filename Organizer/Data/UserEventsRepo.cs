using Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.Data
{
    public class UserEventsRepo : GenericRepo<ApplicationDbContext, UserEvent>, IUserEventsRepo
    {
    }
}