using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class SeatSelectionsController : Controller
    {
        private TestEntities db = new TestEntities();

        // GET: SeatSelections
        public ActionResult Index()
        {
            return View(db.SeatSelections.ToList());
        }

        // GET: SeatSelections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatSelection seatSelection = db.SeatSelections.Find(id);
            if (seatSelection == null)
            {
                return HttpNotFound();
            }
            return View(seatSelection);
        }

        // GET: SeatSelections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeatSelections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,seat,fill,seatno")] SeatSelection seatSelection)
        {
            if (ModelState.IsValid)
            {
                db.SeatSelections.Add(seatSelection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seatSelection);
        }

        // GET: SeatSelections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatSelection seatSelection = db.SeatSelections.Find(id);
            if (seatSelection == null)
            {
                return HttpNotFound();
            }
            return View(seatSelection);
        }

        // POST: SeatSelections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,seat,fill,seatno")] SeatSelection seatSelection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seatSelection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seatSelection);
        }

        // GET: SeatSelections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatSelection seatSelection = db.SeatSelections.Find(id);
            if (seatSelection == null)
            {
                return HttpNotFound();
            }
            return View(seatSelection);
        }

        // POST: SeatSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SeatSelection seatSelection = db.SeatSelections.Find(id);
            db.SeatSelections.Remove(seatSelection);
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
