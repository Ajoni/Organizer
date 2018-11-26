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
    [Authorize]
    public class TODOItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UnitOfWork unit= new UnitOfWork();

        public ActionResult Index()
        {
            var tODOItems = db.TODOItems.Include(t => t.User);
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
            return View(tODOItem);
        }

        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: TODOItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Title,Description,StartDate,EndDate")] TODOItem tODOItem)
        {
            if (ModelState.IsValid)
            {
                db.TODOItems.Add(tODOItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tODOItem.UserId);
            return View(tODOItem);
        }

        // GET: TODOItems/Edit/5
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tODOItem.UserId);
            return View(tODOItem);
        }

        // POST: TODOItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Title,Description,StartDate,EndDate")] TODOItem tODOItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tODOItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tODOItem.UserId);
            return View(tODOItem);
        }

        // GET: TODOItems/Delete/5
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
            return View(tODOItem);
        }

        // POST: TODOItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TODOItem tODOItem = db.TODOItems.Find(id);
            db.TODOItems.Remove(tODOItem);
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
