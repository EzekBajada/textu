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
    public class TasksController : Controller
    {
        private TextEditorDatabaseEntities db = new TextEditorDatabaseEntities();

        // GET: Tasks
        public ActionResult Index()
        {
            var taskTables = db.TaskTables.Include(t => t.ProjectTable);
            return View(taskTables.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTable taskTable = db.TaskTables.Find(id);
            if (taskTable == null)
            {
                return HttpNotFound();
            }
            return View(taskTable);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.project_id = new SelectList(db.ProjectTables, "project_id", "title");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "task_id,title,project_id")] TaskTable taskTable)
        {
            if (ModelState.IsValid)
            {
                db.TaskTables.Add(taskTable);
                db.SaveChanges();
                return RedirectToAction("Index", "Projects");
            }

            ViewBag.project_id = new SelectList(db.ProjectTables, "project_id", "title", taskTable.project_id);
            return View(taskTable);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTable taskTable = db.TaskTables.Find(id);
            if (taskTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.project_id = new SelectList(db.ProjectTables, "project_id", "title", taskTable.project_id);
            return View(taskTable);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "task_id,title,project_id")] TaskTable taskTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.project_id = new SelectList(db.ProjectTables, "project_id", "title", taskTable.project_id);
            return View(taskTable);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskTable taskTable = db.TaskTables.Find(id);
            if (taskTable == null)
            {
                return HttpNotFound();
            }
            return View(taskTable);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskTable taskTable = db.TaskTables.Find(id);
            db.TaskTables.Remove(taskTable);
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
