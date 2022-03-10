using CmsClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Controllers
{
    public class ChooseDoctorController : Controller
    {
        //Getting Specialization
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Choosedoctor()
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost]
        public IActionResult Choosedoctor(ChooseDoctors s)
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Create", "Appointment", new { d = s.SelectSpeciality });
        }
    }
}
