using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Organizer.Data;
using Organizer.Models;
using Organizer.ViewModels;

namespace Organizer.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());

            var model = new GroupsIndexViewModel()
            {
                OwnGroups = db.Groups.Include(g => g.Owner).Where(g => g.Owner.Id == user.Id).ToList(),
                AdministeredGroups = user.AdministratedGroups.ToList(),
                ObservedGroups = user.GroupObservations.ToList()
            };
            return View(model);
        }

        public ActionResult GroupEvents(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Include("Events").Where( g => g.Id == id).FirstOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var model = new GroupEventsViewModel()
            {
                GroupId = id,
                GroupEvents = group.Events,
                UserIsAdmin = group.Administrators.Contains(user)
            };
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        public ActionResult ListAdmins(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Include("Administrators").Where(g => g.Id == id).FirstOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group.Administrators);
        }

        public ActionResult ListObservers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Include("Observers").Where(g => g.Id == id).FirstOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group.Observers);
        }

        public ActionResult Observe(int? id)
        {
            var userId = User.Identity.GetUserId();
            #region errors
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #endregion
            Group group = db.Groups.Include("Observers").Where(g => g.Id == id).FirstOrDefault();
            var user = db.Users.Find(userId);
            group.Observers.Add(user);
            db.SaveChanges();
            #region errors
            if (group == null)
            {
                return HttpNotFound();
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            #endregion

            return RedirectToAction("Index");
        }


        //move to user controller?
        public ActionResult AdministratedGroups()
        {
            var userId = User.Identity.GetUserId();
            #region errors
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #endregion
            var user = db.Users.Find(userId);
            return View(user.AdministratedGroups);
        }

        public ActionResult GroupObservations()
        {
            var userId = User.Identity.GetUserId();
            #region errors
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #endregion
            var user = db.Users.Find(userId);
            return View(user.GroupObservations);
        }
        //



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Tags")] Group group)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                group.OwnerId = userId;
                var user = db.Users.Find(userId);
                user.AdministratedGroups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }


        public ActionResult CreateEvent(int? id)
        {
            return View(new GroupEvent());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent([Bind(Include = "Id,Title,Tags")] GroupEvent  groupEvent, int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = db.Groups.Find(id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                var userId = User.Identity.GetUserId();
                group.Events.Add(groupEvent);
                db.SaveChanges();
                return RedirectToAction("GroupEvents", new { id = id });
            }

            return View(groupEvent);
        }

        public ActionResult DeleteEvent(int? id, int? groupId)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GroupEvent Event = db.GroupEvents.Find(id);
                if (Event == null)
                {
                    return HttpNotFound();
                }
                if (isOwner(Event))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                db.GroupEvents.Remove(Event);
                db.SaveChanges();
                return RedirectToAction("GroupEvents", new { id = groupId });
            }
            return RedirectToAction("GroupEvents", new { id = groupId });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (group.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Tags")] Group group)
        {
            if (group.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (group.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            if (group.OwnerId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View(new GroupsSearchViewModel());
        }

        [HttpPost]
        public ActionResult Search(GroupsSearchViewModel viewModel)
        {
            viewModel.Groups = db.Groups.Where(g => g.Tags.Contains(viewModel.Query) || g.Title.Contains(viewModel.Query)).ToList();
            return View(viewModel);
        }

        private bool isOwner(GroupEvent groupEvent)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var e = db.UserEvents.Find(groupEvent.Id);
            return e != null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
