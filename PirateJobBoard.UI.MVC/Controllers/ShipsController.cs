using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PirateJobBoard.DATA.EF;
using PirateJobBoard.UI.MVC.Models;

namespace PirateJobBoard.UI.MVC.Controllers
{
    [Authorize(Roles = "PirateLord")]
    public class ShipsController : Controller
    {
        private PirateJobBoardEntities db = new PirateJobBoardEntities();

        // GET: Ships
        public ActionResult Index()
        {
            var ships = db.Ships.Include(s => s.PirateDetail);
            return View(ships.ToList());
        }

        // GET: Ships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ship ship = db.Ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            return View(ship);
        }

        // GET: Ships/Create
        public ActionResult Create()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var pirates = db.PirateDetails.ToList();
            List<PirateDetail> captains = new List<PirateDetail>();
            foreach (var pirate in pirates)
            {
                if (userManager.IsInRole(pirate.PirateID, "Captain"))
                {
                    captains.Add(pirate);
                }
            }

            //var userID = db.AspNetUsers.Find()
            //List<string> roles = new List<string>();
            //foreach (var item in Roles.GetAllRoles())
            //{
            //    roles.Add(item);
            //}
            //var captainRole = roles.Find("Captain"); User.IsInRole("Captain")
            //var userRoles = userManager.GetRoles()
            ViewBag.CaptainID = new SelectList(captains, "PirateID", "FullName");
            return View();
        }

        // POST: Ships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShipID,ShipName,HomePort,CaptainID")] Ship ship)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var pirates = db.PirateDetails.ToList();
            List<PirateDetail> captains = new List<PirateDetail>();
            foreach (var pirate in pirates)
            {
                if (userManager.IsInRole(pirate.PirateID, "Captain"))
                {
                    captains.Add(pirate);
                }
            }
            if (ModelState.IsValid)
            {
                db.Ships.Add(ship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CaptainID = new SelectList(captains, "PirateID", "FullName");
            return View(ship);
        }

        // GET: Ships/Edit/5
        public ActionResult Edit(int? id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var pirates = db.PirateDetails.ToList();
            List<PirateDetail> captains = new List<PirateDetail>();
            foreach (var pirate in pirates)
            {
                if (userManager.IsInRole(pirate.PirateID, "Captain"))
                {
                    captains.Add(pirate);
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ship ship = db.Ships.Find(id);
            if (ship == null)
            {
                return HttpNotFound();
            }
            ViewBag.CaptainID = new SelectList(captains, "PirateID", "FullName", ship.CaptainID);
            return View(ship);
        }

        // POST: Ships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShipID,ShipName,HomePort,CaptainID")] Ship ship)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var pirates = db.PirateDetails.ToList();
            List<PirateDetail> captains = new List<PirateDetail>();
            foreach (var pirate in pirates)
            {
                if (userManager.IsInRole(pirate.PirateID, "Captain"))
                {
                    captains.Add(pirate);
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(ship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CaptainID = new SelectList(captains, "PirateID", "FullName");
            return View(ship);
        }

        // GET: Ships/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ship ship = db.Ships.Find(id);
        //    if (ship == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ship);
        //}

        // POST: Ships/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Ship ship = db.Ships.Find(id);
        //    db.Ships.Remove(ship);
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
