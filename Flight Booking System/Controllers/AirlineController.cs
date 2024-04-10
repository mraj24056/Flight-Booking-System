using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flight_Booking_System.Controllers
{
    public class AirlineController : Controller

    {
        private ARSContext db = new ARSContext();

        // GET: Airline
        public ActionResult Index()
        {
            var airlines = db.Airlines.ToList();
            return View(airlines);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Airline airlines)
        {
            if (ModelState.IsValid)
            {
                db.Airlines.Add(airlines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airlines);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var airlines = db.Airlines.Find(id);

            if (airlines == null)
            {
                return HttpNotFound();
            }

            return View(airlines);
        }

        [HttpPost]
        public ActionResult Edit(Airline airlines)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airlines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airlines);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var airlines = db.Airlines.Find(id);

            if (airlines == null)
            {
                return HttpNotFound();
            }

            return View(airlines);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var airlines = db.Airlines.Find(id);

            if (airlines == null)
            {
                return HttpNotFound();
            }

            db.Airlines.Remove(airlines);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}