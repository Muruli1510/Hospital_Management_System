using Entity_Model_Layer.Models;
using HMSBAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Mvc;


namespace Hospital_Management_System.Controllers
{
    public class UIController : Controller
    {
        public static string message = "";
        public static string Pname = "";
        public static string pname = "";
        public static int Pid = 0;
        private HMSServicesPat db;
        public UIController(HMSServicesPat d)
        {
            db = d;
        }
        public IActionResult showpatientdetails(int? page)
        {
            IPagedList<Patient> p = null;
            IEnumerable<Patient> cos = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var resp = cln.GetAsync("Patient_Web_API_/" + page);
            resp.Wait();
            var res = resp.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<IEnumerable<Patient>>();
                data.Wait();
               cos = data.Result;
                p = (IPagedList<Patient>)cos.ToPagedList(page??1,8);
            }
            return View(p);
        }
        public IActionResult showpatientbyid(int PatientId)
        {
            Patient cos = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getpatId = cln.GetAsync("Patient_Web_API_/" + PatientId);
            getpatId.Wait();
            var res = getpatId.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<Patient>();
                data.Wait();
                cos = data.Result;
            }
            return View(cos);
        }
        public int getpatientidbyname(string Pname)
        {
            Patient cos = null;
            Patient p = null;
            int id = 0;
            using (var c = new HttpClient())
            {
                using (var resp = c.GetAsync("http://localhost:47793/api/Patient_Web_API_/getpid?Pname=" + Pname).Result)
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var data = resp.Content.ReadAsAsync<Patient>();
                        data.Wait();
                        p = data.Result;
                        id = p.PatientId;
                    }
                }
            }
            return id;
        }
        public IEnumerable<RoomDatum> getrooms()
        {
            IEnumerable<RoomDatum> dat = null;
            // RoomDatum r = null;
            // int id = 0;
            using (var c = new HttpClient())
            {
                using (var resp = c.GetAsync("http://localhost:47793/api/Room_Data_Web_API_").Result)
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var data = resp.Content.ReadAsAsync<IEnumerable<RoomDatum>>();
                        data.Wait();
                        dat = data.Result;
                        //ViewData["rooms"] = dat;


                    }
                }
            }
            return dat;
        }
        public IActionResult getpatientbyname(string Pname)
        {
            Patient p = null;
            int id = 0;
            using (var c = new HttpClient())
            {
                using (var resp = c.GetAsync("http://localhost:47793/api/Patient_Web_API_/getpid?Pname=" + Pname).Result)
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        var data = resp.Content.ReadAsAsync<Patient>();
                        data.Wait();
                        p = data.Result;
                        id = p.PatientId;

                    }
                }
            }
            return View("getpatientidbyname", p);
        }
        public IEnumerable<Lab> getlabs()
        {
            IEnumerable<Lab> labs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getlab = cln.GetAsync("Lab_Web_API_");
            getlab.Wait();
            var res = getlab.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<IEnumerable<Lab>>();
                data.Wait();
                labs = data.Result;

            }
            return labs;
        }
        public IActionResult insertpatient()
        {
            IEnumerable<Doctor> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getdoc = cln.GetAsync("Doctor_Web_API_");
            getdoc.Wait();
            var res = getdoc.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<IEnumerable<Doctor>>();
                data.Wait();
                docs = data.Result;
                SelectList list = new SelectList(docs, "DoctorId", "DoctorName", "Department");
                ViewBag.doctorlist = list;
            }
            return View();
        }
        [HttpPost]
        public IActionResult insertpatient(Patient details)
        {
            //var type = "I";
            var docid = details.DoctorId;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var post = cln.PostAsJsonAsync<Patient>("Patient_Web_API_/", details);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                // TempData["pname"] = details.PName;
                pname = details.PName;
                var type = details.PType;
                if (type == "I")
                {

                    return RedirectToAction("insertinpatient");
                }
                else if (type == "O")
                {
                    return RedirectToAction("insertoutpatient");
                }
                //return RedirectToAction("showpatientdetails");
            }
            return View(details);
        }
        public IActionResult Updatepatientdetails(int PatientId)
        {
            IEnumerable<Doctor> docs = null;
            Patient patient = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getpat = cln.GetAsync("Patient_Web_API_/" + PatientId);
            getpat.Wait();
            var res = getpat.Result;
            var getdoc = cln.GetAsync("Doctor_Web_API_");
            getdoc.Wait();
            var resdod = getdoc.Result;
            if (res.IsSuccessStatusCode && resdod.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<Patient>();
                data.Wait();
                patient = data.Result;
                var docdata = resdod.Content.ReadAsAsync<IEnumerable<Doctor>>();
                docdata.Wait();
                docs = docdata.Result;
                SelectList list = new SelectList(docs, "DoctorId", "DoctorName", "Department");
                ViewBag.doctorlist = list;

            }
            return View(patient);
        }
        [HttpPost]
        public IActionResult updatepatientdetails(Patient data)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var putpat = cln.PutAsJsonAsync<Patient>("Patient_Web_API_", data);
            putpat.Wait();
            var res = putpat.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showpatientdetails");
            }
            return View(data);
        }
        public IActionResult insertinpatient()
        {
            ViewBag.err = message;
            List<string> rlist = new List<string>();
            Pname = pname;
            Pid = getpatientidbyname(Pname);
            Console.WriteLine(Pid);
            IEnumerable<RoomDatum> roomsls = getrooms();
            //rlist.Add("select");
            foreach (var i in roomsls)
            {
                rlist.Add(Convert.ToString(i.RoomNo));
            }
            //SelectList Pid = new SelectList(pid);
            SelectList rooms = new SelectList(roomsls, "roomno");
            ViewBag.IpId = Pid;
            ViewBag.RoomNos = rlist;
            return View();
        }
        [HttpPost]
        public IActionResult insertinpatient(InPatient data)
        {
            BillDatum bd = new BillDatum();
            bd.PId = Pid;
            Patient p = patdetbyid(Pid);
            int did = (int)p.DoctorId;
            int dfee = docfee(did);
            bd.PType = p.PType;
            bd.DoctorId = p.DoctorId;
            bd.DoctorFees = dfee;
            //InPatient det = new InPatient();            
            data.IpId = Pid;
            DateTime indate = (DateTime)data.AdmissionDate;
            DateTime disdate = (DateTime)data.DischargeDate;
            data.AdmissionDate = indate.Date;
            data.DischargeDate = disdate.Date;
            if (data.AdmissionDate > data.DischargeDate)
            {
                message = "Discharge date should be greater the admission date please select valid date";
                return RedirectToAction("insertinpatient");
            }
            int datediff = (disdate - indate).Days;
            bd.TotalDays = datediff;
            int rid = (int)data.RoomNo;
            int roomfee = getroomfee(rid);
            bd.RoomCharge = (roomfee * datediff);
            bd.TotalAmount = (bd.DoctorFees + bd.RoomCharge + bd.LabFees);
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var putinpat = cln.PostAsJsonAsync<InPatient>("In_Patient_Web_API_/", data);
            putinpat.Wait();
            var res = putinpat.Result;
            if (res.IsSuccessStatusCode)
            {
                insertintobill(bd);
                return RedirectToAction("showpatientdetails");
            }
            return View(data);
        }
        public IActionResult insertoutpatient()
        {
            ViewBag.err = message;
            //Pname = TempData["pname"].ToString();
            Pname = pname;
            Pid = getpatientidbyname(Pname);
            ViewBag.OpId = Pid;
            return View();
        }
        [HttpPost]
        public IActionResult insertoutpatient(Outpatient data)
        {
            BillDatum bd = new BillDatum();
            data.OpId = Pid;
            Patient p = patdetbyid(Pid);
            int did = (int)p.DoctorId;
            int dfee = docfee(did);
            bd.PId = Pid;
            bd.PType = p.PType;
            bd.DoctorId = p.DoctorId;
            bd.DoctorFees = dfee;
            bd.TotalAmount = dfee;
            DateTime t = DateTime.Now;
            DateTime trdate = (DateTime)data.TreatmentDate;
            data.TreatmentDate = trdate.Date;
            int days = (t - trdate).Days;
            if (days > 30)
            {
                message = "Date should not be older than a month please select valid date";
                return RedirectToAction("insertoutpatient");
            }
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var putoutpat = cln.PostAsJsonAsync<Outpatient>("Out_Patient_Web_API_/", data);
            putoutpat.Wait();
            var res = putoutpat.Result;
            if (res.IsSuccessStatusCode)
            {
                insertintobill(bd);
                return RedirectToAction("showpatientdetails");
            }
            return View(data);

        }
        public IActionResult validatepid()
        {
            ViewBag.err = message;

            return View();
        }
        [HttpGet]
        public IActionResult validatepid1(int patientid)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getpatId = cln.GetAsync("Patient_Web_API_/" + patientid);
            getpatId.Wait();
            var res = getpatId.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<Patient>();
                data.Wait();
                Patient p = data.Result;

                if (p != null)
                {
                    Pid = p.PatientId;
                    return RedirectToAction("addlab");
                }
                else
                {
                    message = "Enter valid Patient Id";
                    return RedirectToAction("validatepid");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult addlab()
        {
            List<string> lablist = new List<string>();
            ViewBag.err = message;
            IEnumerable<Lab> tests = getlabs();
            //rlist.Add("select");
            foreach (var i in tests)
            {
                lablist.Add(Convert.ToString(i.TestType));
            }
            SelectList ls = new SelectList(lablist, "TestType");
            ViewBag.labtests = ls;

            return View();
        }
        [HttpPost]
        public IActionResult addlab(Lab data)
        {
            string test = data.TestType;
            double amount = getlabamount(test);
            //data.TestAmount = data.TestType;
           // BillDatum bill = new BillDatum();
           // bill.PId = Pid;
            BillDatum b = getprebill(Pid);

           // double preamount = b.LabFees;
            b.LabFees += amount;
            b.TotalAmount += amount;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getpatId = cln.PutAsJsonAsync("Bill_Data_Web_API_/", b);
            getpatId.Wait();
            var res = getpatId.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showpatientdetails");
            }

            return View();
        }
        public double getlabamount(string test)
        {
            Lab b = null;
            double amount = 0;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getpatId = cln.GetAsync("Lab_Web_API_/" + test);
            getpatId.Wait();
            var res = getpatId.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<Lab>();
                data.Wait();
                b = data.Result;
                amount = (double)b.TestAmount;
            }
            return amount;
        }
        public BillDatum getprebill(int Pid)
        {
            //Pid = 150;
            //double preamount = 0;
            BillDatum b = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getpatId = cln.GetAsync("Bill_Data_Web_API_/" + Pid);
            getpatId.Wait();
            var res = getpatId.Result;
            if (res.IsSuccessStatusCode)
            {
                var predata = res.Content.ReadAsAsync<BillDatum>();
                predata.Wait();
                b = predata.Result;
                //preamount = (double)b.LabFees;
            }
            return b;

        }
        public void insertintobill(BillDatum bd)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var ibd= cln.PostAsJsonAsync("Bill_Data_Web_API_/", bd);
            ibd.Wait();
            var res = ibd.Result;
            if (res.IsSuccessStatusCode)
            {
                Console.WriteLine("inserted");
            }
        }
        public Patient patdetbyid(int Pid)
        {
            Patient p = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getpat = cln.GetAsync("Patient_Web_API_/" + Pid);
            getpat.Wait();
            var res = getpat.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<Patient>();
                data.Wait();
                p = data.Result;
            }
            return p;
        }
        public int docfee(int did)
        {
            Doctor d = new Doctor();
            int docfee = 0;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getdocfee = cln.GetAsync("Doctor_Web_API_/" + did);
            getdocfee.Wait();
            var res = getdocfee.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<Doctor>();
                data.Wait();
                d = data.Result;
                docfee =(int) d.DoctorFees;
            }

            return docfee;
        }
        public int getroomfee(int rid)
        {
            RoomDatum r = null;
            int rfee = 0;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var getrfee = cln.GetAsync("Room_Data_Web_API_/" + rid);
            getrfee.Wait();
            var res = getrfee.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<RoomDatum>();
                data.Wait();
                r = data.Result;
                rfee = (int)r.RoomFees;
            }
            return rfee;
        }
        public IActionResult showbilldetails()
        {
            IEnumerable<BillDatum> b = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("http://localhost:47793/api/");
            var resp = cln.GetAsync("Bill_Data_Web_API_" );
            resp.Wait();
            var res = resp.Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsAsync<IEnumerable<BillDatum>>();
                data.Wait();
                b = data.Result;
                
            }
            return View(b);
        }
    }
}
