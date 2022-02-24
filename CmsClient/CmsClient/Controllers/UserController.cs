using AspNetCoreHero.ToastNotification.Abstractions;
using CmsClient.Helpers;
using CmsClient.Models;
using CmsClient.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CmsClient.Controllers
{

    public class UserController : Controller
    {
        private IConfiguration _configuration;

        private readonly INotyfService _notyf;
        public UserController(INotyfService notyf, IConfiguration configuration)
        {
            _notyf = notyf;
            _configuration = configuration;
        }
        // Accessing the data from API using this base Url
        string Baseurl = "https://localhost:44305/";

        //Get the List of users
        public async Task<IActionResult> GetAllUsers()
        {
            List<UserRegister> UserSetupInfo = new List<UserRegister>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Registers");

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserSetupInfo = JsonConvert.DeserializeObject<List<UserRegister>>(UserSetupResponse);

                }
                return View(UserSetupInfo);
            }
        }

        //Creating  a new user;
        [HttpGet]
        public IActionResult Create()
        {
            //generating random numbers for captcha
            Random rnd = new Random();
            ViewBag.captcha1 = rnd.Next(0, 20);// returns random integers >= 10 and < 19
            ViewBag.captcha2 = rnd.Next(10, 20);
            ViewBag.resultCaptcha = ViewBag.captcha1 + ViewBag.captcha2;
            return View("RegisterForm");
        }
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)] // Avoid to store session cache.
        [HttpPost]
        public async Task<IActionResult> Create(RegisterForm reg)
        {
            //validating username
            string name = reg.Username;
            while (CheckUserAvailablity(name).Result == false)
            {
                Random rnd = new Random();
                string autosuggestion = reg.Username + rnd.Next(1, 1000);
                if (CheckUserAvailablity(autosuggestion).Result == true)
                {

                    ViewBag.captcha1 = rnd.Next(0, 20);// returns random integers >= 10 and < 19
                    ViewBag.captcha2 = rnd.Next(10, 20);
                    ViewBag.resultCaptcha = ViewBag.captcha1 + ViewBag.captcha2;

                    ViewBag.autosuggestion = "Username is already taken. Try: " + reg.Username + rnd.Next(1, 1000);
                    return View("RegisterForm");
                }

            }

            MainHelper main = new MainHelper(_configuration);

            UserRegister Uobj = new UserRegister();
            //Assign the value for this fields
            Uobj.Firstname = reg.Firstname;
            Uobj.Lastname = reg.Lastname;
            Uobj.Username = reg.Username;
            Uobj.Email = reg.Email;
            Uobj.Password = Helpers.PasswordHasher.Encrypt(reg.Password);
            Uobj.ConfirmPassword = Uobj.Password;
            Uobj.SecurityQuestion = reg.SecurityQuestion;
            Uobj.Answer = reg.Answer;
            Uobj.MobileNo = "9887051246";
            Uobj.Status = false;
            Uobj.CreationDate = DateTime.Now;
            Uobj.SecurityCode = Helpers.RandomHelper.RandomString(6);


            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(Uobj), Encoding.UTF8, "application/json");
                if (reg.Captcha == reg.resultCaptcha)
                {
                    using (var response = await httpClient.PostAsync("api/Registers", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            Uobj = JsonConvert.DeserializeObject<UserRegister>(apiResponse);
                            try
                            {

                                //sending email to user
                                string body = "<!DOCTYPE html>" +
                                                "<html> " +
                                                    "<body> " +
                                                    "<h4>Welcome </h4>" + Uobj.Username +
                                                    "<h4>Thanks for your registration!\nSecurity Code: </h4>" + Uobj.SecurityCode +
                                                    "<h4>Please click on the following link to verify your account.</h4>" +
                                                    "<a href='https://localhost:44338/User/Activate'>Verify here</a>" +
                                                    "</body> " +
                                                "</html>";
                                //string body = "Welcome " + Uobj.Username + "\nThanks for your registration!\nPlease verify your account using code\nSecurity Code: " + Uobj.SecurityCode;
                                main.Send(_configuration["Gmail:Username"], Uobj.Email, "Successfully Registered!", body);

                                //Notification message displays on the top of the view page

                                //we can store any data in session for particular time period i.e browser
                                HttpContext.Session.SetString("username", Uobj.Username);
                                HttpContext.Session.SetString("purpose", "login");
                                //return RedirectToAction("Activate");
                                //ViewBag.EmailSentMessage = "Email sent successfully!";
                                _notyf.Success("Verification code sent to your registered email!", 10);
                                return View("RegisterForm");


                            }
                            catch
                            {
                                _notyf.Error("Invalid Email Address!", 60);
                                Random rnd = new Random();
                                ViewBag.captcha1 = rnd.Next(0, 20);// returns random integers >= 10 and < 19
                                ViewBag.captcha2 = rnd.Next(10, 20);
                                ViewBag.resultCaptcha = ViewBag.captcha1 + ViewBag.captcha2;
                                ViewBag.EmailError = "Invalid Email Address";
                                return View("RegisterForm", reg);
                            }

                        }
                        else
                        {
                            _notyf.Warning("Something went wrong!", 3);
                            Random rnd = new Random();
                            ViewBag.captcha1 = rnd.Next(0, 20);// returns random integers >= 10 and < 19
                            ViewBag.captcha2 = rnd.Next(10, 20);
                            ViewBag.resultCaptcha = ViewBag.captcha1 + ViewBag.captcha2;
                            return View("RegisterForm", reg);
                        }

                    }
                }
                else
                {
                    Random rnd = new Random();
                    ViewBag.captcha1 = rnd.Next(0, 20);// returns random integers >= 10 and < 19
                    ViewBag.captcha2 = rnd.Next(10, 20);
                    ViewBag.resultCaptcha = ViewBag.captcha1 + ViewBag.captcha2;
                    ViewBag.captchaError = "Invalid Captcha";
                    return View("RegisterForm", reg);
                }
            }

        }

        public static async Task<bool> CheckUserAvailablity(string user)
        {
            List<UserRegister> UserSetupInfo = new List<UserRegister>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44305/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Registers");

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserSetupInfo = JsonConvert.DeserializeObject<List<UserRegister>>(UserSetupResponse);

                }
            }
            foreach (var i in UserSetupInfo)
            {
                if (i.Username == user)
                {
                    return false;//username is not available
                }

            }
            return true;

        }


        [HttpGet]
        public IActionResult Activate()
        {

            //ViewBag.username = HttpContext.Session.GetString("username");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Activate(string securitycode)
        {
            List<UserRegister> UserSetupInfo = new List<UserRegister>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Registers");

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserSetupInfo = JsonConvert.DeserializeObject<List<UserRegister>>(UserSetupResponse);

                }
            }
            UserRegister obj = null;


            foreach (var i in UserSetupInfo)
            {

                if (i.SecurityCode == securitycode)
                {
                    int seconds = DateTime.Now.Subtract((DateTime)i.CreationDate).Seconds;
                    if (seconds < 60)
                    {
                        obj = i;
                    }


                }
            }
            if (obj != null)
            {

                if (HttpContext.Session.GetString("purpose") == "login")
                {
                    obj.Status = true;
                    using (var httpClient = new HttpClient())
                    {
                        string username = obj.Username;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:44305/api/Registers/" + username, content1))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                try
                                {
                                    string body = "Dear " + obj.Username + "\nYour account is activated!\nNow you can use your user credentials to log in to your account";
                                    MainHelper main = new MainHelper(_configuration);
                                    main.Send(_configuration["Gmail:username"], obj.Email, "Verification Process Done!", body);
                                    _notyf.Success("Your account is activated!", 3);
                                    return RedirectToAction("Login", "Login");
                                }
                                catch
                                {
                                    _notyf.Error("Invalid Email Address!", 60);
                                    return View();
                                }

                            }
                            else
                            {

                                return View();
                            }
                        }

                    }
                }
                else if (HttpContext.Session.GetString("purpose") == "reset")
                {
                    using (var httpClient = new HttpClient())
                    {
                        string username = obj.Username;
                        StringContent content1 = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync("https://localhost:44305/api/Registers/" + username, content1))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                try
                                {
                                    string body = "You have requested to reset your password\nNow you can update your password";
                                    MainHelper main = new MainHelper(_configuration);
                                    main.Send(_configuration["Gmail:username"], obj.Email, "Reset-Password Approved!", body);
                                    _notyf.Success("Successfully verified", 3);
                                    HttpContext.Session.SetString("username", obj.Username);
                                    return RedirectToAction("ResetPassword", "User");

                                }
                                catch
                                {
                                    _notyf.Error("Invalid Email Address!", 60);
                                    return View();
                                }

                            }
                            else
                            {
                                return View();
                            }
                        }

                    }

                }

            }
            else
            {
                ViewBag.invalidCode = "Invalid Security code!";
                return View();
            }
            return View();
        }
        public async Task<IActionResult> Resend()
        {
            UserRegister u = new UserRegister();
            string username = HttpContext.Session.GetString("username");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44305/api/Registers/GetUserSetup/" + username))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    u = JsonConvert.DeserializeObject<UserRegister>(apiResponse);
                }
            }
            u.SecurityCode = Helpers.RandomHelper.RandomString(6);
            u.CreationDate = DateTime.Now;

            using (var httpClient = new HttpClient())
            {

                StringContent content1 = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:44305/api/Registers/" + username, content1))
                {

                }
            }
            try
            {
                string body = "Security code: " + u.SecurityCode;
                MainHelper main = new MainHelper(_configuration);
                main.Send(_configuration["Gmail:username"], u.Email, "New Security Code", body);
                _notyf.Success("Code has been sent to your registered mail", 10);
                return RedirectToAction("Activate");
            }
            catch
            {
                _notyf.Error("Invalid Email Address!", 60);
                return View();
            }




        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            UserRegister UserSetupInfo = new UserRegister();
            UserSetupInfo = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Registers/GetUserSetup/" + username);

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserSetupInfo = JsonConvert.DeserializeObject<UserRegister>(UserSetupResponse);

                }
                if (UserSetupInfo == null)
                {
                    ViewBag.userNotFound = "User name not exists";
                    return View();
                }
                UserSetupInfo.SecurityCode = Helpers.RandomHelper.RandomString(6);
                UserSetupInfo.CreationDate = DateTime.Now;

                using (var httpClient = new HttpClient())
                {

                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(UserSetupInfo), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:44305/api/Registers/" + UserSetupInfo.Username, content1))
                    {

                    }
                }
                try
                {
                    string body = "<!DOCTYPE html>" +
                                               "<html> " +
                                                   "<body> " +
                                                    "<h4>Welcome </h4>" + UserSetupInfo.Username +
                                                    "<h4>You are requested to reset your password</h4> " +
                                                    "<h4>Use Security Code: </h4>" + UserSetupInfo.SecurityCode +
                                                    "<h4>Please click on the following link to verify your account.</h4>" +
                                                    "<a href='https://localhost:44338/User/Activate'>Verify here</a>" +
                                                   "</body> " +
                                               "</html>";
                    //string body = "Hi " + UserSetupInfo.Username + "\nSecurity Code: " + UserSetupInfo.SecurityCode;
                    MainHelper main = new MainHelper(_configuration);
                    main.Send(_configuration["Gmail:username"], UserSetupInfo.Email, "Verify Account", body);
                    HttpContext.Session.SetString("purpose", "reset");
                    HttpContext.Session.SetString("username", UserSetupInfo.Username);
                    //ViewBag.EmailSentMessage = "Reset link has been sent to your registered mail";
                    _notyf.Success("Please check your email to verify your account", 10);

                }
                catch
                {
                    _notyf.Error("Invalid Email Address!", 10);
                    return View();
                }


                //return View("Activate");
                return View();

            }


        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(Resetpassword obj)
        {
            UserRegister UserSetupInfo = new UserRegister();

            using (var client = new HttpClient())
            {
                string username = obj.Username;
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Registers/GetUserSetup/" + username);

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserSetupInfo = JsonConvert.DeserializeObject<UserRegister>(UserSetupResponse);

                }

                //update your new password with old password
                UserSetupInfo.Password = Helpers.PasswordHasher.Encrypt(obj.Currentpassword);
                UserSetupInfo.ConfirmPassword = UserSetupInfo.Password;


                using (var httpClient = new HttpClient())
                {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(UserSetupInfo), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:44305/api/Registers/" + username, content))
                    {

                    }
                }
                try
                {
                    string body = "Your password has been changed successfully!\nUse your new password to login";
                    MainHelper main = new MainHelper(_configuration);
                    main.Send(_configuration["Gmail:username"], UserSetupInfo.Email, "Password Changed!", body);
                    _notyf.Success("Password Changed", 3);
                    ViewBag.UpdateMessage = "Your password has been changed successfully";
                    return RedirectToAction("Login", "Login");

                }
                catch
                {
                    _notyf.Error("Something went wrong!", 10);
                    return View();
                }


            }


        }
        [HttpGet]
        public IActionResult ForgotUsername()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ForgotUsername(RegisterForm reg)
        {
            List<UserRegister> UserList = new List<UserRegister>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Registers");

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserList = JsonConvert.DeserializeObject<List<UserRegister>>(UserSetupResponse);

                }

            }
            ////validating Email address- available ore not 
            //foreach (var i in UserList)
            //{
            //    if (i.Email != reg.Email)
            //    {
            //        ViewBag.invalidEmail = "Invalid Email address";
            //        return View();
            //    }
            //}


            //Getting username from database using security Q&A and Email
            var user = (from db in UserList
                        where db.SecurityQuestion == reg.SecurityQuestion && db.Answer == reg.Answer && db.Email == reg.Email
                        select db.Username).FirstOrDefault();


            //If user is not found in database returns an error message in view
            if (user == null)
            {
                ViewBag.userNotFound = "User not found";
                return View();
            }

            UserRegister u = new UserRegister();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Registers/GetUserSetup/" + user);

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    u = JsonConvert.DeserializeObject<UserRegister>(UserSetupResponse);

                }
                //u.SecurityCode = Helpers.RandomHelper.RandomString(6);
                //u.CreationDate = DateTime.Now;
                using (var httpClient = new HttpClient())
                {

                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:44305/api/Registers/" + user, content1))
                    {

                    }
                }

                try
                {

                    MainHelper main = new MainHelper(_configuration);
                    //string body = "Hi " + u.Username + "\nSecurity Code: " + u.SecurityCode;
                    string body = "<!DOCTYPE html>" +
                                           "<html> " +
                                               "<body> " + "<h2>Verification done!</h2> " +
                                               "<h3>Now, You can reset your password here!!</h3>" +
                                               "<a href='https://localhost:44338/User/ResetPassword'>Reset Link</a>" +
                                               "</body> " +
                                           "</html>";

                    main.Send(_configuration["Gmail:username"], u.Email, "Reset Password", body);
                    HttpContext.Session.SetString("purpose", "reset");
                    HttpContext.Session.SetString("username", u.Username);
                    //ViewBag.EmailSentMessage = "Reset link has been sent to your registered mail";
                    _notyf.Success("Please check your email to reset your password", 10);
                   


                }
                catch
                {
                    _notyf.Error("Something went wrong!", 60);
                    return View();
                }


            }
            return View();
        }
         
    }
    

    //Edit the user details;
    //[HttpGet]
    //public async Task<IActionResult> Edit(string username)
    //{
    //    UserRegister u = new UserRegister();
    //    using (var httpClient = new HttpClient())
    //    {
    //        using (var response = await httpClient.GetAsync("https://localhost:44305/api/Registers/" + username))
    //        {
    //            string apiResponse = await response.Content.ReadAsStringAsync();
    //            u = JsonConvert.DeserializeObject<UserRegister>(apiResponse);
    //        }
    //    }
    //    return View(u);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Edit(UserRegister u)
    //{
    //    UserRegister u1 = new UserRegister();
    //    using (var httpClient = new HttpClient())
    //    {
    //        string username = u.Username;
    //        StringContent content1 = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
    //        using (var response = await httpClient.PutAsync("https://localhost:44305/api/Registers/" + username, content1))
    //        {
    //            string apiResponse = await response.Content.ReadAsStringAsync();
    //            ViewBag.Result = "Success";
    //            u1 = JsonConvert.DeserializeObject<UserRegister>(apiResponse);
    //        }
    //    }
    //    _notyf.Success("Successfully Updated!", 3);
    //    return RedirectToAction("GetAllUsers");
    //}

    ////Delete a user
    //[HttpGet]
    //public async Task<ActionResult> Delete(string username)
    //{
    //    TempData["Username"] = username;
    //    UserRegister e = new UserRegister();
    //    using (var httpClient = new HttpClient())
    //    {
    //        using (var response = await httpClient.GetAsync("https://localhost:44305/api/Registers/" + username))
    //        {
    //            string apiResponse = await response.Content.ReadAsStringAsync();
    //            e = JsonConvert.DeserializeObject<UserRegister>(apiResponse);
    //        }
    //    }
    //    return View(e);

    //}

    //[HttpPost]
    //public async Task<ActionResult> Delete(UserRegister u)
    //{
    //    string username = (string)TempData["Username"];
    //    using (var httpClient = new HttpClient())
    //    {
    //        using (var response = await httpClient.DeleteAsync("https://localhost:44305/api/Registers/" + username))
    //        {
    //            string apiResponse = await response.Content.ReadAsStringAsync();
    //        }
    //    }
    //    _notyf.Success("Successfully Deleted!", 3);
    //    return RedirectToAction("GetAllUsers");
    //}
}