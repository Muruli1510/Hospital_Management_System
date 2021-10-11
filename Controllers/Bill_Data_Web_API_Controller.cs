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
    public class Bill_Data_Web_API_Controller : ControllerBase
    {
        private HMSServicesBillData db;
        private Hosptial_Management_SystemContext dd;
        public Bill_Data_Web_API_Controller(HMSServicesBillData d, Hosptial_Management_SystemContext ddd)
        {
            db = d;
            dd = ddd;
        }
        [HttpGet]
        public IActionResult getbilldata()
        {
            return Ok(db.getbills());
        }
        [HttpGet("{Pid}")]
        public IActionResult getdata(int Pid)
        {
            var entity = dd.BillData.Where(i => i.PId == Pid).FirstOrDefault();
            return Ok(entity);
           // return Ok(db.getdetbypid(Pid));
        }
        [HttpPost]
        public IActionResult insertbilldat(BillDatum data)
        {
            return Ok(db.addbill(data));
        }
        [HttpPut]
        public IActionResult updatebilldat(BillDatum b)
        {
            return Ok(db.updatebill(b));
        }
    }
}
