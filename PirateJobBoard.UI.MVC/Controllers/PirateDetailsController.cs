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
    [Authorize(Roles = "Crewmate")]
    public class PirateDetailsController : Controller
    {
        private PirateJobBoardEntities db = new PirateJobBoardEntities();

        // GET: PirateDetails
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
            return View(db.PirateDetails.Where(x => x.PirateID == userID).ToList());
        }

        // GET: PirateDetails/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PirateDetail pirateDetail = db.PirateDetails.Find(id);
        //    if (pirateDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pirateDetail);
        //}

        //// GET: PirateDetails/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: PirateDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PirateID,FirstName,LastName,ResumeFilename,HasScurvy")] PirateDetail pirateDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PirateDetails.Add(pirateDetail);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(pirateDetail);
        //}

        // GET: PirateDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PirateDetail pirateDetail = db.PirateDetails.Find(id);
            if (pirateDetail == null)
            {
                return HttpNotFound();
            }
            return View(pirateDetail);
        }

        // POST: PirateDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PirateID,FirstName,LastName,ResumeFilename,HasScurvy")] PirateDetail pirateDetail, HttpPostedFileBase resume)
        {
            

            if (ModelState.IsValid)
            {
                string resumeName = "";
                if (resume != null)
                {
                    resumeName = resume.FileName;

                    string ext = resumeName.Substring(resumeName.LastIndexOf('.'));
                    string[] goodExts = { ".doc", ".docx", ".pdf", ".rtf", ".txt" };

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        resumeName = Guid.NewGuid() + ext;
                        resume.SaveAs(Server.MapPath("~/Content/Resumes/" + resumeName));
                    }
                    else
                    {
                        resumeName = "No Resume Uploaded";
                    }

                    pirateDetail.ResumeFilename = resumeName;
                }

                db.Entry(pirateDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pirateDetail);
        }

        // GET: PirateDetails/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PirateDetail pirateDetail = db.PirateDetails.Find(id);
        //    if (pirateDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pirateDetail);
        //}

        //// POST: PirateDetails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    PirateDetail pirateDetail = db.PirateDetails.Find(id);
        //    db.PirateDetails.Remove(pirateDetail);
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
