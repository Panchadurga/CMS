using AspNetCoreHero.ToastNotification.Abstractions;
using CmsClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class PatientController : Controller
    {
        private readonly INotyfService _notyf;
        public PatientController(INotyfService notyf)
        {
            _notyf = notyf;
        }
        string Baseurl = "https://wbcmsapi.azurewebsites.net/";
        //Get all the List of patients
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> GetAllPatients()
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            List<Patient> PatientInfo = new List<Patient>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Patients");

                if (Res.IsSuccessStatusCode)
                {
                    var PatientResponse = Res.Content.ReadAsStringAsync().Result;
                    PatientInfo = JsonConvert.DeserializeObject<List<Patient>>(PatientResponse);

                }
                return View(PatientInfo);
            }
        }
        //Adding a patient
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Create()
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Patient p)
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Patient pobj = new Patient();
            var dob = p.DOB;
            int age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            {
                age = age - 1;
            }
            p.Age = age;
            //  HttpClient obj = new HttpClient();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("api/Patients", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //ViewBag.Message = "Succesfully Added.";
                    pobj = JsonConvert.DeserializeObject<Patient>(apiResponse);
                }
            }
            _notyf.Success("Successfully Added.", 3);
            return RedirectToAction("GetAllPatients");
        }

        //Calculate age using dateofbirth
        //public async Task<IActionResult> Age(string dob)
        //{
        //    DateTime DOB = Convert.ToDateTime(dob);
        //    string dateofbirth = DOB.Year + "-" + DOB.Month + "-" + DOB.Day;       
        //    using (var client = new HttpClient())
        //    {
        //        int Age = 0;
        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = await client.GetAsync("/api/Patients/GetAgebyDateofBirth/" + dateofbirth);

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var PatientResponse = Res.Content.ReadAsStringAsync().Result;
        //            Age = JsonConvert.DeserializeObject<int>(PatientResponse);
        //            ViewBag.age = Age;

        //        }
        //    }
        //    return PartialView("Age");
        //}
        //Edit the details of the patient
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Edit(int id)
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Patient p = new Patient();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://wbcmsapi.azurewebsites.net/api/Patients/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<Patient>(apiResponse);
                }
            }
            return View(p);

        }
       

        [HttpPost]
        public async Task<IActionResult> Edit(Patient p)
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var dob = p.DOB;
            int age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            {
                age = age - 1;
            }
            p.Age = age;
            Patient p1 = new Patient();
            using (var httpClient = new HttpClient())
            {
                int id = p.PatientId;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://wbcmsapi.azurewebsites.net/api/Patients/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    p1 = JsonConvert.DeserializeObject<Patient>(apiResponse);
                }
            }
            _notyf.Success("Successfully Updated.", 3);
            return RedirectToAction("GetAllPatients");
        }
        //Delete a particular patient
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            TempData["PatientId"] = id;
            Patient p = new Patient();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://wbcmsapi.azurewebsites.net/api/Patients/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    p = JsonConvert.DeserializeObject<Patient>(apiResponse);
                }
            }
            return View(p);

        }
      
        [HttpPost]
        public async Task<ActionResult> Delete(Patient p)
        {
            var u = HttpContext.Session.GetString("username");
            if (u == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int patientid = Convert.ToInt32(TempData["PatientId"]);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://wbcmsapi.azurewebsites.net/api/Patients/" + patientid))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            _notyf.Success("Successfully Deleted.", 3);
            return RedirectToAction("GetAllPatients");
        }
    }
}
