using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Organizer.Data;
using Organizer.Models;

namespace Organizer.Controllers
{
    public class GroupEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GroupEvents
        public ActionResult Index()
        {
            return View(db.GroupEvents.ToList());
        }

        // GET: GroupEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupEvent groupEvent = db.GroupEvents.Find(id);
            if (groupEvent == null)
            {
                return HttpNotFound();
            }
            return View(groupEvent);
        }

        // GET: GroupEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,StartDate,EndDate")] GroupEvent groupEvent)
        {
            if (ModelState.IsValid)
            {
                db.GroupEvents.Add(groupEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupEvent);
        }

        // GET: GroupEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupEvent groupEvent = db.GroupEvents.Find(id);
            if (groupEvent == null)
            {
                return HttpNotFound();
            }
            return View(groupEvent);
        }

        // POST: GroupEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,StartDate,EndDate")] GroupEvent groupEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupEvent);
        }

        // GET: GroupEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupEvent groupEvent = db.GroupEvents.Find(id);
            if (groupEvent == null)
            {
                return HttpNotFound();
            }
            return View(groupEvent);
        }

        // POST: GroupEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupEvent groupEvent = db.GroupEvents.Find(id);
            db.GroupEvents.Remove(groupEvent);
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
