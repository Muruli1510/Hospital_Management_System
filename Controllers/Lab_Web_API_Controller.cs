
using Entity_Model_Layer.Models;
using HMSBAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Lab_Web_API_Controller : ControllerBase
    {
        private HMSServicesLab db;
        private Hosptial_Management_SystemContext dd;
        public Lab_Web_API_Controller(HMSServicesLab d,Hosptial_Management_SystemContext ddd)
        {
            db = d;
            dd = ddd;
        }
        [HttpGet]
        public IActionResult getlabtests()
        {
            return Ok( db.getlabdet());
        }
        [HttpGet("{test}")]
        public IActionResult getlabtest(string test)
        {
            Lab l = null;
            //pid = db.Patients.Where(v => v.PName == Pname).OrderByDescending(p => p.PatientId).FirstOrDefault();
            l = dd.Labs.Where(v => v.TestType == test).FirstOrDefault();
            return Ok(l);
        }
    }
}
