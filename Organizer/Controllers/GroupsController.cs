using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Organizer.Data;
using Organizer.Models;

namespace Organizer.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var groups = db.Groups.Include(g => g.Owner);
            return View(groups.ToList());
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
            return View(group.Events);
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

        public ActionResult ObserveGroup(int? id)
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
            
            return View(group.Observers);
        }
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
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }


        public ActionResult CreateEvent(int? id)
        {
            return View();
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
                return RedirectToAction("GroupEvents", id);
            }

            return View(groupEvent);
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
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Tags")] Group group)
        {
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
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
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
