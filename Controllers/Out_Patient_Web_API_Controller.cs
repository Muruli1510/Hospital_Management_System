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
    public class Out_Patient_Web_API_Controller : ControllerBase
    {
        private HMSServicesOutPatient db;
        public Out_Patient_Web_API_Controller(HMSServicesOutPatient d)
        {
            db = d;
        }
        [HttpGet]
        public IActionResult getalloutpatients()
        {
            db.getoutpatients();
            return Ok();
        }
        [HttpPost]
        public IActionResult insertoutpatientdet(Outpatient data)
        {
            return Ok(db.insertoutpatient(data));
        }
        //[HttpPut]
        //public IActionResult updateoutpatientdet(Outpatient data)
        //{
        //    return Ok(db.updateoutpatient(data));
        //}
    }
}
