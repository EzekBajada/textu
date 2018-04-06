using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rt_texteditor.Models;

namespace rt_texteditor.Controllers
{
    public class ProjectsController : Controller
    {
        private TextEditorDatabaseEntities db = new TextEditorDatabaseEntities();

        // GET: Projects
        public ActionResult Index()
        {
            var projectTables = db.ProjectTables.Include(p => p.AspNetUser);
            return View(projectTables.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTable projectTable = db.ProjectTables.Find(id);
            if (projectTable == null)
            {
                return HttpNotFound();
            }
            return View(projectTable);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "project_id,title,user_id")] ProjectTable projectTable)
        {
            if (ModelState.IsValid)
            {
                projectTable.user_id = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
                db.ProjectTables.Add(projectTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", projectTable.user_id);
            return View(projectTable);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTable projectTable = db.ProjectTables.Find(id);
            if (projectTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", projectTable.user_id);
            return View(projectTable);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "project_id,title,user_id")] ProjectTable projectTable)
        {
            if (ModelState.IsValid)
            {
                projectTable.user_id = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
                db.Entry(projectTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", projectTable.user_id);
            return View(projectTable);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTable projectTable = db.ProjectTables.Find(id);
            if (projectTable == null)
            {
                return HttpNotFound();
            }
            return View(projectTable);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectTable projectTable = db.ProjectTables.Find(id);
            db.ProjectTables.Remove(projectTable);
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
