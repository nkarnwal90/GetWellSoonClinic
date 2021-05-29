using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GET_WELL_SOON_CLINIC.Models;

namespace GET_WELL_SOON_CLINIC.Controllers
{
    public class AppointmentsController : Controller
    {
        private GET_WELL_SOON_CLINICEntities db = new GET_WELL_SOON_CLINICEntities();

        // GET: Appointments
        public async Task<ActionResult> Index()
        {
            return View(await db.Appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.Doctors_id = new SelectList(db.Doctors, "DoctorsID", "FirstName");//the soure of dropdownlist
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AppointmentsID,LastName,FirstName,PhoneNumber,DateOfBirth,Gender,picture,Address,DoctorsID,AppointmentDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Doctors_id = new SelectList(db.Doctors, "DoctorsID", "FirstName", appointment.DoctorsID);//book.Author_id sets pre-options of dropdownlist

                var count = db.Appointments.Where(x => x.DoctorsID == appointment.DoctorsID && x.AppointmentDate == appointment.AppointmentDate).Count();
                if(count == 6)
                {
                    return View("ErrorDoctorAppointmentLimit");
                }

                db.Appointments.Add(appointment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Doctors_id = new SelectList(db.Doctors, "DoctorsID", "FirstName");//the soure of dropdownlist
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AppointmentsID,LastName,FirstName,PhoneNumber,DateOfBirth,Gender,picture,Address,DoctorsID,AppointmentDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await db.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Appointment appointment = await db.Appointments.FindAsync(id);
            db.Appointments.Remove(appointment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
