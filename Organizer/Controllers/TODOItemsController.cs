﻿using System;
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
    [Authorize]
    public class TODOItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var tODOItems = db.TODOItems.Include(t => t.User).Where(x => x.UserId == userId);
            return View(tODOItems.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TODOItem tODOItem = db.TODOItems.Find(id);
            if (tODOItem == null)
            {
                return HttpNotFound();
            }
            if (tODOItem.UserId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(tODOItem);
        }

        public ActionResult Create()
        {
            return View(new TODOItem { StartDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,StartDate,EndDate")] TODOItem tODOItem)
        {
            if (ModelState.IsValid)
            {
                if (tODOItem.EndDate < tODOItem.StartDate)
                {
                    ModelState.AddModelError(string.Empty, "End date can't be before start date.");
                    return View(tODOItem);
                }
                var userId = User.Identity.GetUserId();
                tODOItem.UserId = userId;
                db.TODOItems.Add(tODOItem);
                var user = db.Users.Find(userId);
                user.TodosTotal++;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tODOItem);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TODOItem tODOItem = db.TODOItems.Find(id);
            if (tODOItem == null)
            {
                return HttpNotFound();
            }
            if (tODOItem.UserId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(tODOItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,StartDate,EndDate,UserId")] TODOItem tODOItem)
        {
            if (ModelState.IsValid)
            {
                if (tODOItem.EndDate < tODOItem.StartDate)
                {
                    ModelState.AddModelError(string.Empty, "End date can't be before start date.");
                    return View(tODOItem);
                }
                db.Entry(tODOItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tODOItem);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TODOItem tODOItem = db.TODOItems.Find(id);
            if (tODOItem == null)
            {
                return HttpNotFound();
            }
            if (tODOItem.UserId != User.Identity.GetUserId())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(tODOItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TODOItem tODOItem = db.TODOItems.Find(id);
            db.TODOItems.Remove(tODOItem);
            if(DateTime.Now < tODOItem.EndDate)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                user.TodosDoneInTime++;
            }
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
