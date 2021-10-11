using Entity_Model_Layer.Models;
using HMSBAL;
using Hospital_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private Patient_Web_API_Controller db;
        private HMSServicesPat db;
        private HMSServicesDoc dd;
        private Hosptial_Management_SystemContext dnl;
        public HomeController(ILogger<HomeController> logger,  HMSServicesPat d,Hosptial_Management_SystemContext dbb,HMSServicesDoc ddd)
        {
            _logger = logger;
            db = d;
            dnl = dbb;
            dd = ddd;
        }
        

        public IActionResult getpatdet(int? page)
        {
            //db.getallpatient();
            //return View(db.getallpatient());
            return View(db.GetPatients(page));
        }
        public IActionResult Details(int PatientId)
        {
            ViewData["pid"] = PatientId;
            return View(db.GetPatient(PatientId));
        }
        public IActionResult Create()
        {          
            Doctor d = new Doctor();
            IEnumerable<Doctor> dl = dnl.Doctors.ToList();
            SelectList list = new SelectList(dl, "DoctorId", "DoctorName", "Department");
            ViewBag.doctorlist = list;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Patient dat)
        {
            int d =(int) dat.DoctorId;
            db.Addpatient(dat);
            return RedirectToAction("getpatdet");
        }
        [HttpGet]
        public IActionResult Edit(int PatientId)
        {
            Doctor d = new Doctor();
            IEnumerable<Doctor> dl = dnl.Doctors.ToList();
            SelectList list = new SelectList(dl, "DoctorId", "DoctorName", "Department");
            ViewBag.doctorlist = list;
            Patient p =db.GetPatient(PatientId);
            return View(p);
        }
        [HttpPost]
        public IActionResult Edit(Patient dat)
        {
            if (ModelState.IsValid)
            {
                db.updatepatient(dat);
                return RedirectToAction("getpatdet");
            }
            return View();
        }

        public IActionResult getdoctors()
        {
            return View(dd.getdoctors());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
