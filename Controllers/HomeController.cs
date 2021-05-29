using GET_WELL_SOON_CLINIC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GET_WELL_SOON_CLINIC.Controllers
{
    public class HomeController : Controller
    {
        private GET_WELL_SOON_CLINICEntities db = new GET_WELL_SOON_CLINICEntities();
        public async Task<ActionResult> Index()
        {
            return View(await db.Doctors.ToListAsync());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Schedule(int? id)        
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Appointment> appointment = db.Appointments.Where(x=>x.DoctorsID==id).ToList();
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}