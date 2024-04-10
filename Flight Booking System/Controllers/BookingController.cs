using Flight_Booking_System.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Flight_Booking_System.Controllers
{
    public class BookingController : Controller
    {
        private ARSContext db = new ARSContext();

        public ActionResult Index()
        {
            int userId = (int)Session["userid"];
            var bookings = db.Bookings.Where(b=>b.UserID == userId).ToList();
            return View(bookings);
        }

        // GET: Booking/Create
        public ActionResult Create(int Id)
        {
            int UserId = (int)Session["userid"];
            ViewBag.UserId = UserId;
            Schedule schedule = db.Schedules.Find(Id);

            if (schedule == null)
            {
                return HttpNotFound();
            }

            Booking booking = new Booking()
            {
                From = schedule.Source,
                To = schedule.Destination,
                BookingDate = DateTime.Now,
                FlightId = schedule.FlightId,
                UserID = UserId,
                ScheduleId = schedule.ScheduleId

            };
            return View(booking);
        }

        public ActionResult PayNow(Booking booking)
        {
            Payment pay = new Payment();
            pay.PaymentDate = DateTime.Now;
            pay.UserID = booking.UserID;
            UserLogin u = db.UserLogins.Find(booking.UserID);
            pay.UserName = u.FirstName;
            pay.BookingId = booking.BookingId;
            pay.ScheduleId = booking.ScheduleId;
            pay.PaymentMode = "Online";
            var s = db.Schedules.Find(booking.ScheduleId);
            pay.Amount = (decimal)(booking.NumberOfPassengers * s.Price);
            Session.Add("Booking", booking);
            return View(pay);

        }

        [HttpPost]
        public ActionResult PayNow(Payment pay)
        {
            //WriteRestAPICode
            
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Payment Process Completed";
                Booking booking = (Booking)Session["Booking"];
                return RedirectToAction("Confirmation", new { id = booking.BookingId });
            }
            ViewBag.Message = "Payment Failed";
            return View("Error");

        }

            // POST: Booking/Create
            [HttpPost]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            { 
                booking.NumberOfPassengers = 1;
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("PayNow", booking);
 
            }

             
            return View(booking);
        }

        // GET: Booking/Confirmation
        public ActionResult Confirmation(string id)
        {
            int userId = (int)Session["userid"];
            int maxBookId = db.Bookings.Where(b => b.UserID == userId).Select(b => b.BookingId).Max();
            Booking booking = null;
            if (id != null)
            {
                int Bid = Int32.Parse(id);
                booking = db.Bookings.Find(Bid);

            }
            else
            {
                booking = db.Bookings.Find(maxBookId);
            }
            if (booking == null)
            {
                return HttpNotFound();
            }

            var s = db.Schedules.Find(booking.ScheduleId);

            Flight f = db.Flights.Find(booking.FlightId);
            UserLogin u = db.UserLogins.Find(booking.UserID);
            float totalPrice = booking.NumberOfPassengers * s.Price;
            Airline al = db.Airlines.Find(s.AirlineId);
            ViewBag.FirstName = u.FirstName;
            ViewBag.Source = booking.From;
            ViewBag.Destination = booking.To;
            ViewBag.Airline = al.AirlineName;
            ViewBag.ArrivalTime = s.ArrivalTime;
            ViewBag.DepartureTime = s.DepartureTime;
            ViewBag.BookingDate = booking.BookingDate.Date;
            ViewBag.TotalPrice = totalPrice;
            ViewBag.BookingId = booking.BookingId;

            return View();
        }

        // POST: Booking/Cancel/5
        [HttpGet]
        public ActionResult Cancel(int id)
        {
            Booking booking = db.Bookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            // Delete the booking record from the database
            db.Bookings.Remove(booking);
            db.SaveChanges();

            return RedirectToAction("CancellationConfirmation");
        }

        // GET: Booking/CancellationConfirmation
        public ActionResult CancellationConfirmation()
        {
            return View();
        }

    }
}