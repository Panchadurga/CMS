using AspNetCoreHero.ToastNotification.Abstractions;
using CmsClient.Helpers;
using CmsClient.Models;
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
                HttpResponseMessage Res = await client.GetAsync("api/UserSetups");

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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRegister u)
        {
            MainHelper main = new MainHelper(_configuration);

            UserRegister Uobj = new UserRegister();
            //Setting default value for this fields
            u.Status = false;
            u.CreationDate = DateTime.Now;
            u.SecurityCode = Helpers.RandomHelper.RandomString(6);


            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/UserSetups", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Uobj = JsonConvert.DeserializeObject<UserRegister>(apiResponse);
                        
                        //sending email to user
                        string body = "Hi " + u.Username + "Thanks for your registration!\nPlease verify your account using code\nSecurity Code: " + u.SecurityCode;
                        main.Send(_configuration["Gmail:Username"], u.Email, "Successfully Registered!", body);

                        //Notification message displays on the top of the view page
                        _notyf.Success("Successfully Registered!", 3);
                        //we can store any data in session for particular time period i.e browser
                        HttpContext.Session.SetString("username", u.Username);
                        HttpContext.Session.SetString("purpose", "login"); 
                        return RedirectToAction("Activate");
                    }
                    else
                    {
                        _notyf.Warning("Username is already taken",3);
                        return View(u);
                    }
                    
                }
            }
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
                HttpResponseMessage Res = await client.GetAsync("api/UserSetups");

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
                    int seconds = DateTime.Now.Subtract(i.CreationDate).Seconds;
                    if (seconds < 120)
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
                        using (var response = await httpClient.PutAsync("https://localhost:44305/api/UserSetups/" + username, content1))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string body = "Dear " + obj.Username + "\nYour account is activated!\nNow you can use your user credentials to log in to your account";
                                MainHelper main = new MainHelper(_configuration);
                                main.Send(_configuration["Gmail:username"], obj.Email, "Verification Process Done!", body);
                                _notyf.Success("Your account is activated!", 3);
                                return RedirectToAction("Login", "Login");
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
                        using (var response = await httpClient.PutAsync("https://localhost:44305/api/UserSetups/" + username, content1))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string body = "You have requested to reset your password\nNow you can update your password";
                                MainHelper main = new MainHelper(_configuration);
                                main.Send(_configuration["Gmail:username"], obj.Email, "Reset-Password Approved!", body);
                                _notyf.Success("Successfully verified", 3);
                                HttpContext.Session.SetString("username", obj.Username);
                                return RedirectToAction("ResetPassword", "User");
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
                using (var response = await httpClient.GetAsync("https://localhost:44305/api/UserSetups/GetUserSetup/" + username))
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
                using (var response = await httpClient.PutAsync("https://localhost:44305/api/UserSetups/" + username, content1))
                {
                    
                }
            }
            string body = "Security code: " + u.SecurityCode;
            MainHelper main = new MainHelper(_configuration);
            main.Send(_configuration["Gmail:username"], u.Email, "New Security Code", body);
            return RedirectToAction("Activate");



        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            UserRegister UserSetupInfo = new UserRegister();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/UserSetups/GetUserSetupbyEmail/" + email);

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserSetupInfo = JsonConvert.DeserializeObject<UserRegister>(UserSetupResponse);

                }
                
                UserSetupInfo.SecurityCode = Helpers.RandomHelper.RandomString(6);
                UserSetupInfo.CreationDate = DateTime.Now;

                using (var httpClient = new HttpClient())
                {

                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(UserSetupInfo), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:44305/api/UserSetups/" + UserSetupInfo.Username, content1))
                    {

                    }
                }
                string body = "Security Code: " + UserSetupInfo.SecurityCode;
                MainHelper main = new MainHelper(_configuration);
                main.Send(_configuration["Gmail:username"], UserSetupInfo.Email, "Forgot Password", body);
                HttpContext.Session.SetString("purpose", "reset");
                HttpContext.Session.SetString("username", UserSetupInfo.Username);

                return View("Activate");

            }
            

        }
        
        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(Resetpassword  obj)
        {
            UserRegister UserSetupInfo = new UserRegister();

            using (var client = new HttpClient())
            {
                string username = obj.Username;
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/UserSetups/GetUserSetup/" + username);

                if (Res.IsSuccessStatusCode)
                {
                    var UserSetupResponse = Res.Content.ReadAsStringAsync().Result;
                    UserSetupInfo = JsonConvert.DeserializeObject<UserRegister>(UserSetupResponse);

                }
                
                //update your password
                UserSetupInfo.Password = obj.Currentpassword;
                

                using (var httpClient = new HttpClient())
                {

                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(UserSetupInfo), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:44305/api/UserSetups/" + username, content1))
                    {

                    }
                }
                string body = "Your password has been changed successfully!\nUse your new password to login";
                MainHelper main = new MainHelper(_configuration);
                main.Send(_configuration["Gmail:username"], UserSetupInfo.Email, "Password Changed!", body);
                _notyf.Success("Password Changed", 3);
                return RedirectToAction("Login", "Login");

            }
            

        }



        //Edit the user details;
        //[HttpGet]
        //public async Task<IActionResult> Edit(string username)
        //{
        //    UserRegister u = new UserRegister();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:44305/api/UserSetups/" + username))
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
        //        using (var response = await httpClient.PutAsync("https://localhost:44305/api/UserSetups/" + username, content1))
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
        //        using (var response = await httpClient.GetAsync("https://localhost:44305/api/UserSetups/" + username))
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
        //        using (var response = await httpClient.DeleteAsync("https://localhost:44305/api/UserSetups/" + username))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    _notyf.Success("Successfully Deleted!", 3);
        //    return RedirectToAction("GetAllUsers");
        //}
    }
}
