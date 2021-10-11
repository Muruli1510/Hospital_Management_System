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
    public class In_Patient_Web_API_Controller : ControllerBase
    {
        private HMSServicesInPatient db;
        public In_Patient_Web_API_Controller(HMSServicesInPatient d)
        {
            db = d;
        }
        [HttpPost]
        public IActionResult insertinpatientdet(InPatient data)
        {
            return Ok(db.insertinpatient(data));
        }
    }
}
