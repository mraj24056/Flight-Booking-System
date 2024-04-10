using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flight_Booking_System.Controllers
{
    public class HomeController : Controller
    {
        private ARSContext db = new ARSContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult UserHome()
        {
            ViewBag.Source = new SelectList(db.Airports,"AirportId", "AirportName");
            ViewBag.Destination = new SelectList(db.Airports, "AirportId", "AirportName");
            return View();
        }

        [HttpPost]
        public ActionResult UserHome(FormCollection f)
        {
            string Source = f["AirportId"];
            string Destination = f["AirportId1"];
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details()
        {
            int userId = (int)Session["userid"];
            UserLogin user = db.UserLogins.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


    }
}