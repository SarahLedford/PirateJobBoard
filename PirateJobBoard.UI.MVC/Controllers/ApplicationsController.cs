using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
            string userID = User.Identity.GetUserId();
            if (User.IsInRole("Captain"))
            {
                var applicationsCaptain = db.Applications.Where(a => a.OpenAssignment.Ship.CaptainID == userID).Include(a => a.ApplicationStatu).Include(a => a.OpenAssignment).Include(a => a.PirateDetail);
                return View(applicationsCaptain.ToList());
            }
            else if (User.IsInRole("Crewmate"))
            {
                var applicationsCrew = db.Applications.Where(a => a.PirateID == userID).Include(a => a.ApplicationStatu).Include(a => a.OpenAssignment).Include(a => a.PirateDetail);
                return View(applicationsCrew.ToList());
            }

            var applications = db.Applications.Include(a => a.ApplicationStatu).Include(a => a.OpenAssignment).Include(a => a.PirateDetail);
            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Captain"))
            {
                return application.OpenAssignment.Ship.CaptainID == userID ? View(application) : View("Index");
            }
            if (User.IsInRole("Crewmate"))
            {
                return application.PirateID == userID ? View(application) : View("Index");
            }
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
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            //ViewBag.OpenAssignmentID = new SelectList(db.OpenAssignments, "OpenAssignmentID", "OpenAssignmentID", application.OpenAssignmentID);
            //ViewBag.PirateID = new SelectList(db.PirateDetails, "PirateID", "FirstName", application.PirateID);
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
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            //ViewBag.OpenAssignmentID = new SelectList(db.OpenAssignments, "OpenAssignmentID", "OpenAssignmentID", application.OpenAssignmentID);
            //ViewBag.PirateID = new SelectList(db.PirateDetails, "PirateID", "FirstName", application.PirateID);
            return View(application);
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
