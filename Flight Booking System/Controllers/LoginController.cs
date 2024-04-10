using Flight_Booking_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Flight_Booking_System.Controllers
{
    public class LoginController : Controller
    {
        ARSContext db = new ARSContext();
        // GET: Login
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserLogin user)
        {
            user.Role = "Passenger";
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = db.UserLogins.Any(x => x.Email == user.Email);
                if (!isValidEmail(user.Email))
                    ModelState.AddModelError("email", "Invalid Email!");
                if (isEmailAlreadyExists)
                    ModelState.AddModelError("email", "Email already in use!");
                if (ModelState.IsValid)
                {
                    db.UserLogins.Add(user);
                    var a = db.SaveChanges();

                    if (a > 0)
                    {
                        TempData["alert"] = "<script>alert('Registration Successfull')</script>";
                        return RedirectToAction("SignIn", "Login");
                    }

                    else
                    {
                        ViewBag.alert = "<script>alert('Registration not Successfull')</script>";
                        return View();
                    }
                }
            }
            return View();
        }


        static bool isValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {

                var addr = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(UserLogin user)
        {

            var passenger = db.UserLogins.Where(m => m.Email == user.Email && m.Password == user.Password).FirstOrDefault();

            if (passenger != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Session["uname"] = passenger.FirstName;
                Session["userid"] = passenger.UserID;
                Session.Add("Role", passenger.Role);

                if (passenger.Role == "Admin")
                {
                    return RedirectToAction("AdminHome", "Home");
                }

                if (passenger.Role == "Passenger")
                {
                    return RedirectToAction("UserHome", "Home");
                }

                TempData["alert"] = "<script>alert('Login Successfull')</script>";
                return RedirectToAction("Index", "Flight");
            }
            else
            {
                ViewBag.alert = "<script>alert('Login not Successfull')</script>";
                return View();
            }

            //}

            return View();
        }
        public ActionResult SignOut(UserLogin user)
        {
            FormsAuthentication.SignOut();
            Session["uname"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}