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
    public class Room_Data_Web_API_Controller : ControllerBase
    {
        private HMSServicesRoomData db;
        public Room_Data_Web_API_Controller(HMSServicesRoomData d)
        {
            db = d;
        }
        [HttpGet]
        public IActionResult getroomsdetails()
        {
            return Ok(db.getallrooms());
        }
        [HttpGet("{rid}")]
        public IActionResult getroom(int rid)
        {
            return Ok(db.getroom(rid));
        }
    }
}
