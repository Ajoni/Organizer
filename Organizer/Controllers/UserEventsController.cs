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

namespace Organizer.Controllers
{
    public class UserEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserEvents
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            return View(user.Events);
        }

        // GET: UserEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEvent userEvent = db.UserEvents.Find(id);
            if (userEvent == null)
            {
                return HttpNotFound();
            }
            if (userEvent.Visibility)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                var e = db.UserEvents.Find(userEvent.Id);
                if (e == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            return View(userEvent);
        }

        // GET: UserEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserEvents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Visibility,Title,StartDate,EndDate")] UserEvent userEvent)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
                db.UserEvents.Add(userEvent);
                user.Events.Add(userEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userEvent);
        }

        // GET: UserEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEvent userEvent = db.UserEvents.Find(id);
            if (userEvent == null)
            {
                return HttpNotFound();
            }
            if (!isOwner(userEvent))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(userEvent);
        }

        // POST: UserEvents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Visibility,Title,StartDate,EndDate")] UserEvent userEvent)
        {
            if (!isOwner(userEvent))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (ModelState.IsValid)
            {
                db.Entry(userEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userEvent);
        }

        // GET: UserEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEvent userEvent = db.UserEvents.Find(id);
            if (userEvent == null)
            {
                return HttpNotFound();
            }
            if (!isOwner(userEvent))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(userEvent);
        }

        // POST: UserEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserEvent userEvent = db.UserEvents.Find(id);
            if (!isOwner(userEvent))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            db.UserEvents.Remove(userEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool isOwner(UserEvent userEvent)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            var e = db.UserEvents.Find(userEvent.Id);
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
