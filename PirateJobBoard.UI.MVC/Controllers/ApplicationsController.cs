using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PirateJobBoard.DATA.EF;

namespace PirateJobBoard.UI.MVC.Controllers
{
    [Authorize(Roles ="PirateLord, Captain, Crewmate")]
    public class ApplicationsController : Controller
    {
        private PirateJobBoardEntities db = new PirateJobBoardEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var applications = db.Applications.Include(a => a.ApplicationStatu).Include(a => a.OpenAssignment).Include(a => a.PirateDetail);
            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        [Authorize(Roles = "Crewmate")]
        public ActionResult Create()
        {
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName");
            ViewBag.OpenAssignmentID = new SelectList(db.OpenAssignments, "OpenAssignmentID", "OpenAssignmentID");
            ViewBag.PirateID = new SelectList(db.PirateDetails, "PirateID", "FirstName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Crewmate")]//TODO -- MAKE ONE-CLICK APPLY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationID,OpenAssignmentID,PirateID,ApplicationDate,CaptainNotes,ApplicationStatus,ResumeFilename")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            ViewBag.OpenAssignmentID = new SelectList(db.OpenAssignments, "OpenAssignmentID", "OpenAssignmentID", application.OpenAssignmentID);
            ViewBag.PirateID = new SelectList(db.PirateDetails, "PirateID", "FirstName", application.PirateID);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "PirateLord, Captain")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            ViewBag.OpenAssignmentID = new SelectList(db.OpenAssignments, "OpenAssignmentID", "OpenAssignmentID", application.OpenAssignmentID);
            ViewBag.PirateID = new SelectList(db.PirateDetails, "PirateID", "FirstName", application.PirateID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PirateLord, Captain")]
        public ActionResult Edit([Bind(Include = "ApplicationID,OpenAssignmentID,PirateID,ApplicationDate,CaptainNotes,ApplicationStatus,ResumeFilename")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            ViewBag.OpenAssignmentID = new SelectList(db.OpenAssignments, "OpenAssignmentID", "OpenAssignmentID", application.OpenAssignmentID);
            ViewBag.PirateID = new SelectList(db.PirateDetails, "PirateID", "FirstName", application.PirateID);
            return View(application);
        }

        //// GET: Applications/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Application application = db.Applications.Find(id);
        //    if (application == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(application);
        //}

        //// POST: Applications/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Application application = db.Applications.Find(id);
        //    db.Applications.Remove(application);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
