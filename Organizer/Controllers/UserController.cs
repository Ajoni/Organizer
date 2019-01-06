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
using Organizer.ViewModels;

namespace Organizer.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ObservingUsers()
        {
            var userId = User.Identity.GetUserId();
            #region errors
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #endregion
            var user = db.Users.Find(userId);
            return View(user.ObservingUsers);
        }

        public ActionResult UserObservations()
        {
            var userId = User.Identity.GetUserId();
            #region errors
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #endregion
            var user = db.Users.Find(userId);
            return View(user.UserObservations);
        }

        public ActionResult Observe(string id)
        {
            var userId = User.Identity.GetUserId();
            #region errors
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #endregion
            var userToObserve = db.Users.Find(id);
            var user = db.Users.Find(userId);
            #region errors
            if (userToObserve == null)
            {
                return HttpNotFound();
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            #endregion
            user.UserObservations.Add(userToObserve);
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        public ActionResult DeleteObservation(string id)
        {
            var userId = User.Identity.GetUserId();
            #region errors
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            #endregion
            var userToObserve = db.Users.Find(id);
            var user = db.Users.Find(userId);
            #region errors
            if (userToObserve == null)
            {
                return HttpNotFound();
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            #endregion
            user.UserObservations.Remove(userToObserve);
            db.SaveChanges();
            return RedirectToAction("Users");
        }

        #region auto gen CRUD
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Description,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Description,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult Users()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Include(u => u.ObservingUsers).Include(u => u.UserObservations).FirstOrDefault(u => u.Id == userId);
            return View(user);
        }

        public ActionResult Find()
        {
            return View(new UsersFindViewModel());
        }

        [HttpPost]
        public ActionResult Find(UsersFindViewModel model)
        {
            model.Users = db.Users.Where(u => u.Email.Contains(model.Query)).ToList();
            return View(model);
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
