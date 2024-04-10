using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flight_Booking_System.Controllers
{

    public class FlightController : Controller
    {
        private ARSContext db = new ARSContext();

        // GET: Flight
        [AllowAnonymous]
        public ActionResult Index()
        {
            var flights = db.Flights.ToList();
            return View(flights);
        }

        [Authorize(Roles = "Admin")]
        // GET: Flight/Create
        public ActionResult Create()
        {
            ViewBag.Source = new SelectList(db.Airports, "AirportId", "AirportName");
            ViewBag.Destination = new SelectList(db.Airports, "AirportId", "AirportName");
            ViewBag.AirLines = new SelectList(db.Airlines,"AirlineId","AirlineName");
            ViewBag.Airports = new SelectList(db.Airports, "AirportId", "AirportName");
            return View();
        }

        // POST: Flight/Create
        [HttpPost]
        public ActionResult Create(FormCollection flight)
        {
         
           
            try
            {
                Flight fl = new Flight();
                int Source = int.Parse(flight["AirportId"]);
                int Destination = int.Parse(flight["AirportId1"]);
              
                Airport arp = db.Airports.Where(ar=>ar.AirportId == Source).FirstOrDefault();
                String SourceAirport = arp.AirportName;
                Airport arp1 = db.Airports.Where(ar1 => ar1.AirportId == Destination).FirstOrDefault();
                string DestinationAirport = arp1.AirportName;

                fl.AirlineId = int.Parse(flight["AirlineId"]);
                Airline a = db.Airlines.Find(fl.AirlineId);
                fl.TotalSeatsCapacity = 0;
                fl.SourceCode = SourceAirport;
                fl.DestinationCode = DestinationAirport;
                fl.AirportId = int.Parse(flight["AirportId"]);
                

                fl.Source = arp.City;
                fl.Destination = arp1.City;
                
                string Atime = null;
               
                string Dtime = null;
                 
                fl.ArrivalTime = "0";
                fl.DepartureTime = "0";
                
                db.Flights.Add(fl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        // GET: Flight/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var flight = db.Flights.Find(id);

            if (flight == null)
            {
                return HttpNotFound();
            }

            return View(flight);
        }

        // POST: Flight/Edit/5
        [HttpPost]
        public ActionResult Edit(Flight flight)
        {
            if (ModelState.IsValid)
            {
                //return Content(flight.TotalSeatsCapacity + "-1");
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flight/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var flight = db.Flights.Find(id);

            if (flight == null)
            {
                return HttpNotFound();
            }

            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var flight = db.Flights.Find(id);

            if (flight == null)
            {
                return HttpNotFound();
            }

            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
