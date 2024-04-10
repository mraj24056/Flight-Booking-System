using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flight_Booking_System.Controllers
{
    public class AirportController : Controller

    {
        private ARSContext db = new ARSContext();

        // GET: Airport
        public ActionResult Index()
        {
            var airports = db.Airports.ToList();
            return View(airports);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Airport airports)
        {
            if (ModelState.IsValid)
            {
                db.Airports.Add(airports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airports);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var airports = db.Airports.Find(id);

            if (airports == null)
            {
                return HttpNotFound();
            }

            return View(airports);
        }

        [HttpPost]
        public ActionResult Edit(Airport airports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airports);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var airports = db.Airports.Find(id);

            if (airports == null)
            {
                return HttpNotFound();
            }

            return View(airports);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var airports = db.Airports.Find(id);

            if (airports == null)
            {
                return HttpNotFound();
            }

            db.Airports.Remove(airports);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}