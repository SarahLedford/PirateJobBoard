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
    public class OpenAssignmentsController : Controller
    {
        private PirateJobBoardEntities db = new PirateJobBoardEntities();

        // GET: OpenAssignments
        public ActionResult Index()
        {
            var openAssignments = db.OpenAssignments.Include(o => o.Assignment).Include(o => o.Ship);
            return View(openAssignments.ToList());
        }

        // GET: OpenAssignments/Details/5
        public ActionResult Details(int? id)
        {
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
        public ActionResult Create()
        {
            ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName");
            ViewBag.ShipID = new SelectList(db.Ships, "ShipID", "ShipName");
            return View();
        }

        // POST: OpenAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

            ViewBag.AssignmentID = new SelectList(db.Assignments, "AssignmentID", "AssignmentName", openAssignment.AssignmentID);
            ViewBag.ShipID = new SelectList(db.Ships, "ShipID", "ShipName", openAssignment.ShipID);
            return View(openAssignment);
        }

        // GET: OpenAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
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
            return View(openAssignment);
        }

        // POST: OpenAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Delete(int? id)
        {
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

        // POST: OpenAssignments/Delete/5
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
