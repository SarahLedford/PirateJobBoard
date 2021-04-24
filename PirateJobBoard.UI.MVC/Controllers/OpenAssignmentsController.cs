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
    [Authorize(Roles = "PirateLord, Captain, Crewmate")]
    public class OpenAssignmentsController : Controller
    {
        private PirateJobBoardEntities db = new PirateJobBoardEntities();

        // GET: OpenAssignments
        public ActionResult Index(string assignmentName)
        {
            string userID = User.Identity.GetUserId();
            if (User.IsInRole("Captain"))
            {
                //var openAssignmentsLoc = db.OpenAssignments.Where(x => x.Ship.CaptainID == userID).Include(o => o.Assignment).Include(o => o.Ship);
                var openAssignmentsLoc = assignmentName == null ? db.OpenAssignments.Where(x => x.Ship.CaptainID == userID).Include(o => o.Assignment).Include(o => o.Ship) : db.OpenAssignments.Where(x => x.Ship.CaptainID == userID && x.Assignment.AssignmentName == assignmentName).Include(o => o.Assignment).Include(o => o.Ship);

                return View(openAssignmentsLoc.ToList());
            }
            var openAssignments = assignmentName == null ? db.OpenAssignments.Include(o => o.Assignment).Include(o => o.Ship) : db.OpenAssignments.Where(x => x.Assignment.AssignmentName == assignmentName).Include(o => o.Assignment).Include(o => o.Ship);
            return View(openAssignments.ToList());
        }

        // GET: OpenAssignments/Details/5
        public ActionResult Details(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenAssignment openAssignment = db.OpenAssignments.Find(id);
            if (openAssignment == null)
            {
                return HttpNotFound();
            }
            return View(openAssignment);
        }

        // GET: OpenAssignments/Create
        [Authorize(Roles = "PirateLord, Captain")]
        public ActionResult Create()
        {
            string userID = User.Identity.GetUserId();
            if (User.IsInRole("Captain"))
            {
                var captainsShip = db.Ships.Where(x => x.CaptainID == userID).ToList();
                ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName");
                ViewBag.ShipID = new SelectList(captainsShip, "ShipID", "ShipName");
                return View();
            }
            ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName");
            ViewBag.ShipID = new SelectList(db.Ships, "ShipID", "ShipName");


            return View();
        }

        // POST: OpenAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PirateLord, Captain")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenAssignmentID,AssignmentID,ShipID")] OpenAssignment openAssignment)
        {
            if (ModelState.IsValid)
            {
                db.OpenAssignments.Add(openAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string userID = User.Identity.GetUserId();
            if (User.IsInRole("Captain"))
            {
                var captainsShip = db.Ships.Where(x => x.CaptainID == userID).ToList();
                ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName");
                ViewBag.ShipID = new SelectList(captainsShip, "ShipID", "ShipName");
                return View(openAssignment);
            }

            ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName", openAssignment.AssignmentID);
            ViewBag.ShipID = new SelectList(db.Ships, "ShipID", "ShipName", openAssignment.ShipID);
            return View(openAssignment);
        }

        public ActionResult OneClickApply(int id)
        {

            string userID = User.Identity.GetUserId();
            string resume = db.PirateDetails.Find(userID).ResumeFilename;
            Application existingApplication = db.Applications.Where(x => x.PirateID == userID && x.OpenAssignmentID == id).FirstOrDefault();
            if (existingApplication != null)
            {
                ViewBag.Message = "Ye have already applied to this position, matey. Nice try!";
                return RedirectToAction("Index");
            }

            Application app = new Application() { PirateID = userID, ApplicationDate = DateTime.Now, ApplicationStatus = 1, ResumeFilename = resume, CaptainNotes = "", OpenAssignmentID = id };

            db.Applications.Add(app);
            db.SaveChanges();
            return RedirectToAction("Index", "Applications");
        }

        // GET: OpenAssignments/Edit/5
        [Authorize(Roles = "PirateLord, Captain")]
        public ActionResult Edit(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenAssignment openAssignment = db.OpenAssignments.Find(id);
            if (openAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName", openAssignment.AssignmentID);
            ViewBag.ShipID = new SelectList(db.Ships, "ShipID", "ShipName", openAssignment.ShipID);
            if (User.IsInRole("Captain"))
            {
                return openAssignment.Ship.CaptainID == userID ? View(openAssignment) : View("Index");
            }

            return View(openAssignment);
        }

        // POST: OpenAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "PirateLord, Captain")]
        public ActionResult Edit([Bind(Include = "OpenAssignmentID,AssignmentID,ShipID")] OpenAssignment openAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName", openAssignment.AssignmentID);
            ViewBag.ShipID = new SelectList(db.Ships, "ShipID", "ShipName", openAssignment.ShipID);
            return View(openAssignment);
        }

        // GET: OpenAssignments/Delete/5
        [Authorize(Roles = "PirateLord, Captain")]
        public ActionResult Delete(int? id)
        {
            string userID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenAssignment openAssignment = db.OpenAssignments.Find(id);
            if (openAssignment == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Captain"))
            {
                return openAssignment.Ship.CaptainID == userID ? View(openAssignment) : View("Index");
            }
            return View(openAssignment);
        }

        // POST: OpenAssignments/Delete/5
        [Authorize(Roles = "PirateLord, Captain")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenAssignment openAssignment = db.OpenAssignments.Find(id);
            db.OpenAssignments.Remove(openAssignment);
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
