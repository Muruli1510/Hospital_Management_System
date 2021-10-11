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
    public class Doctor_Web_API_Controller : ControllerBase
    {
        private HMSServicesDoc db;
        public Doctor_Web_API_Controller(HMSServicesDoc d)
        {
            db = d;
        }
        [HttpGet]
        public IActionResult getalldoctors()
        {
            return Ok(db.getdoctors());
        }
        [HttpGet("{Did}")]
        public IActionResult getdoctor(int Did)
        {
            return Ok(db.getdoctor(Did));
        }
    }
}
