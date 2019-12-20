using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SAT.DATA;

namespace SAT.UI.Controllers
{
    [Authorize(Roles = "Student, Admin")]
    public class EnrollmentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Enrollments
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.ScheduledClass).Include(e => e.Student);

            #region Custom Student Enrollement Records

            //see if the user is in the role of student
            if (User.IsInRole("Student"))
            {
                //get the current user's Id -- this should match the StudentId as well
                var id = User.Identity.GetUserId();

                //return enrollments base on the currentuser/student's id
                enrollments = enrollments.Where(x => x.StudentId == id);

                //check and see if they have enrollments, if not send them somewhere else
                if (enrollments.Count() == 0)
                {
                    Session["NoEnrollment"] = "You currently have no enrollments";
                    return RedirectToAction("Create");
                }
            };
            #endregion

            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses.Where(x => x.ScheduledClassStatus.SCSID == 1), "ScheduledClassId", "Cours.CourseName");
            //ViewBag.ScheduledClass = (IEnumerable<ScheduledClass>)db.ScheduledClasses.ToList();

            ViewBag.StudentId = new SelectList(db.Students.Where(x => x.SSID == 1), "StudentID", "FullName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentId,StudentId,ScheduledClassId,EnrollmentDate")] Enrollment enrollment)
        {
            //before we check for validation we are going to assign the parameter
            //the enrollmentdate property
            enrollment.EnrollmentDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses, "ScheduledClassId", "InstructorName", enrollment.ScheduledClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses, "ScheduledClassId", "InstructorName", enrollment.ScheduledClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentId,StudentId,ScheduledClassId,EnrollmentDate")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ScheduledClassId = new SelectList(db.ScheduledClasses, "ScheduledClassId", "InstructorName", enrollment.ScheduledClassId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentID", "FirstName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
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
