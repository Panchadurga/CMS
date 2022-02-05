using CmsClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data;
using CMS.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace CmsClient.Controllers
{
    //for login
    public class LoginController : Controller
    {
        //To access the user's data from sql database with the help of context class
        private readonly CMSContext _db;
        private readonly INotyfService _notyf;    
        public LoginController(INotyfService notyf, CMSContext db)
        {
            _db = db;
            _notyf = notyf;
        }

        //Get - login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        // Post(Hits the login button)
        [HttpPost]
        public IActionResult Login(CmsClient.Models.Login l)
        {
            CMS.Models.UserSetup obj = (from i in _db.UserSetup
                                        where i.Username == l.Username && i.Password == l.Password && i.Status == true
                                        select i).FirstOrDefault();
            
         
            if (obj != null )
            {

                string username = obj.Username;
                HttpContext.Session.SetString("username", username);
                //ViewBag.user = username;
                //_notyf.Information("Welcome " + HttpContext.Session.GetString("username"), 60);
                return RedirectToAction("Index", "Dashboard");



            }
            else
            {
                ModelState.AddModelError(nameof(l.ErrorMessage),"Incorrect Username Or Password!!!");
                //ViewBag.errormsg = "You have entered wrong username/password";
                return View();

            }
        }

    }
}