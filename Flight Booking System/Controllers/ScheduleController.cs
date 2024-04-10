using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flight_Booking_System.Controllers
{
    public class ScheduleController : Controller
    {
        private ARSContext db = new ARSContext();

        // GET: Schedule
        public ActionResult Index()
        {
            var schedules = db.Schedules.ToList();
            return View(schedules);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Schedule schedules = new Schedule();
            schedules.Source = "";
            schedules.Destination = "";
            schedules.AirlineId = 0;
            return View(schedules);
        }

        [HttpPost]
        public ActionResult Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                Flight fl = db.Flights.Find(schedule.FlightId);
                if(fl==null)
                {
                    ViewBag.Message = "FlighId is invalid";
                    return View("Error");
                }
                schedule.Source = fl.Source;
                schedule.Destination = fl.Destination;
                schedule.AirlineId = fl.AirlineId;
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index","Schedule");
            }

            return View(schedule);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var schedules = db.Schedules.Find(id);

            if (schedules == null)
            {
                return HttpNotFound();
            }

            return View(schedules);
        }

        [HttpPost]
        public ActionResult Edit(Schedule schedules)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedules).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedules);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var schedules = db.Schedules.Find(id);

            if (schedules == null)
            {
                return HttpNotFound();
            }

            return View(schedules);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var schedules = db.Schedules.Find(id);

            if (schedules == null)
            {
                return HttpNotFound();
            }

            db.Schedules.Remove(schedules);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SearchSchedule(FormCollection f)
        {
            int SourceId = int.Parse(f["source"]);
            string Source = db.Airports.Find(SourceId).City;
            int DestinationId = int.Parse(f["destination"]);
            string Destination = db.Airports.Find(DestinationId).City;   

            DateTime FlightDate = DateTime.ParseExact(f["date"],"yyyy-MM-dd",null);
            //return Content(Source + "-" + Destination + "-" + FlightDate);
            List<Schedule> schedules = db.Schedules.Where(fl => fl.Source == Source && fl.Destination == Destination && fl.FlightDate == FlightDate).ToList();
            return View(schedules);
        }

    }
}
