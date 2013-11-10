using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SQLSAT257DataViewModels;

namespace SQLSAT257CRMWeb.Controllers
{
    public class ActivitiesController : Controller
    {
        private CRMDataViewModel db = new CRMDataViewModel();

        // GET: /Activities/
        public ActionResult Index()
        {
            return View(db.Activities.ToList());
        }
        // GET: /Activities/
        public JsonResult Json()
        {
            return Json(db.Activities.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: /Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityViewModel activityviewmodel = db.Activities.Find(id);
            if (activityviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(activityviewmodel);
        }

        // GET: /Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ActivityId,CustomerName,CustomerFirstName,CustomerTaxCode,CustomerVatCode,DateTime,Duration,ActivityTypeDescription,Details,Username")] ActivityViewModel activityviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activityviewmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityviewmodel);
        }

        // GET: /Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityViewModel activityviewmodel = db.Activities.Find(id);
            if (activityviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(activityviewmodel);
        }

        // POST: /Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ActivityId,CustomerName,CustomerFirstName,CustomerTaxCode,CustomerVatCode,DateTime,Duration,ActivityTypeDescription,Details,Username")] ActivityViewModel activityviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityviewmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityviewmodel);
        }

        // GET: /Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityViewModel activityviewmodel = db.Activities.Find(id);
            if (activityviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(activityviewmodel);
        }

        // POST: /Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityViewModel activityviewmodel = db.Activities.Find(id);
            db.Activities.Remove(activityviewmodel);
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
