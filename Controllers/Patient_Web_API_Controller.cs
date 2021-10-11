using Entity_Model_Layer.Models;
using HMSBAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Patient_Web_API_Controller : ControllerBase
    {
        private HMSServicesPat ser_pat;
        private Hosptial_Management_SystemContext db;
        public Patient_Web_API_Controller( HMSServicesPat serpat,Hosptial_Management_SystemContext d)
        {
            ser_pat = serpat;
            db = d;
        }
        [HttpGet]
        public IActionResult getallpatient(int? page)
        {
            return Ok(ser_pat.GetPatients(page));
        }
        [HttpGet("{PatientId}")]
        public IActionResult getpatietbyid(int PatientId)
        {
            return Ok(ser_pat.GetPatient(PatientId));
        }
        //[Route("[action]/{Pname}")]
        //[HttpGet]
        // [ActionName("getpatientid")]
        //[HttpGet("{Pname}")]
        [HttpGet("getpid")]
        public IActionResult getpatientid(string Pname)
        {
            Patient pid = null;
            int id = 0;
            //string storedproc = ($"exec sp_getpidbyname {Pname}");
            pid=  db.Patients.Where(v=>v.PName==Pname).OrderByDescending(p=>p.PatientId).FirstOrDefault();
            return Ok(pid);
            // Patient p=(Patient)db.Patients.FromSqlRaw(storedproc);
            // int pid = p.PatientId;
            //pid =(Patient) db.Patients.FromSqlRaw<Patient>(storedproc);
            //id = pid.PatientId;
            //return id;


        }
        [HttpPost]
        public IActionResult insertpatientdet(Patient dat)
        {
            return Ok(ser_pat.Addpatient(dat));
        }
        [HttpPut]
        public IActionResult updatepatientdet(Patient dat)
        {
            return Ok(ser_pat.updatepatient(dat));
        }
    }
}
