using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Organizer.Data;
using Organizer.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System;
using Organizer.ViewModels;

namespace Organizer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var notes = db.Notes.Include(n => n.User).Where(x => x.UserId == userId).ToList();
            var tODOItems = db.TODOItems.Include(t => t.User).ToList();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindById(User.Identity.GetUserId());
            var events = user.Events.Where(e => e.EndDate > DateTime.Now).ToList();
            double done = user.TodosDoneInTime;
            double all = user.TodosTotal == 0 ? 1 : user.TodosTotal;
            double perf = (done/all)*100;
            return View(new SummaryViewModel(notes,tODOItems,events, Math.Round(perf,0)));
        }

    }
}