using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Controllers
{
    public class DashboardController : Controller
    {

        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            ViewBag.user = HttpContext.Session.GetString("username");
            if(ViewBag.user != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Login");
            }

        }
       


        public IActionResult AddDoctor()
        {
           
            return RedirectToAction("GetAllDoctors", "Doctor");

        }
        public IActionResult AddPatient()
        {
           
            return RedirectToAction("GetAllPatients", "Patient");

        }
        public IActionResult Schedule()
        {
            
            return RedirectToAction("ViewAppointments", "Appointment");

        }

        //public IActionResult Cancel()
        //{

        //    return RedirectToAction("Delete", "Appointment");

        //}
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Logout()
        {
          

            HttpContext.Session.SetString("username", "");
            HttpContext.Session.Clear();
            Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("username");

            return RedirectToAction("Login", "Login");
            
           

        }

            
    }
}
