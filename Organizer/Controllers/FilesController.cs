using Microsoft.AspNet.Identity;
using Organizer.Data;
using Organizer.Models;
using Organizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Organizer.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Files
        // List all files of the user and how much space he already used.
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var model = new FilesIndexViewModel();
            var userFiles = user.Files;

            model.SpaceUsedOverAvailable = UserFile.GetSpaceUsedOverAvailable(userFiles);

            var fileViewModels = new List<UserFileIndexViewModel>();
            foreach(UserFile file in userFiles)
            {
                fileViewModels.Add(file.GetIndexViewModel());
            }
            model.Files = fileViewModels;

            return View(model);
        }

        public ActionResult Upload()
        {
            return View(new FilesUploadViewModel());
        }

        [HttpPost]
        public ActionResult Upload(FilesUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.File.ContentLength > UserFile.MAX_FILESIZE)
            {
                ViewBag.Error = "File size can not be bigger than 1 MB";
                return View(model);
            }
            UserFile file = new UserFile(model.File);

            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            user.Files.Add(file);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Download(int Id)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            UserFile file = db.UserFiles.Find(Id);

            if (!user.Files.Contains(file))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return File(file.Bytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name);
        }

        public ActionResult Delete(int Id)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            UserFile file = db.UserFiles.Find(Id);

            if (!user.Files.Contains(file))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.UserFiles.Remove(file);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}