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
    public class CodeController : Controller
    {
        private TextEditorDatabaseEntities db = new TextEditorDatabaseEntities();

        // GET: Code
        public ActionResult Index()
        {
            var codeTables = db.CodeTables.Include(c => c.AspNetUser).Include(c => c.TaskTable);
            return View(codeTables.ToList());
        }

        // GET: Code/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTable codeTable = db.CodeTables.Find(id);
            if (codeTable == null)
            {
                return HttpNotFound();
            }
            return View(codeTable);
        }

        // GET: Code/Create
        public ActionResult Create(int id)
        {
            //ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email");
            //ViewBag.task_id = new SelectList(db.TaskTables, "task_id", "title");
            //return View();
            var model = new CodeTable();
            model.task_id = id;
            foreach (var item in db.CodeTables)
            {
                if (item.task_id == id)
                {
                    model.text += item.text;
                }
            }
            return View(model);
        }

        // POST: Code/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "task_id,line_no,user_id,text")] CodeTable codeTable)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in db.CodeTables)
                {
                    if (item.task_id == codeTable.task_id)
                    {
                        db.CodeTables.Remove(item);
                    }
                }
                db.SaveChanges();
                int tId = codeTable.task_id;
                string txt = codeTable.text;
                if (txt != null)
                {
                    string[] lst = txt.Split(new Char[] { '\n' });
                    for (int i = 0; i < lst.Length; i++)
                    {
                        codeTable = new CodeTable();
                        codeTable.user_id = db.AspNetUsers.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Id;
                        codeTable.task_id = tId;
                        codeTable.text = lst[i];
                        codeTable.line_no = i + 1;
                        db.CodeTables.Add(codeTable);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Create");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", codeTable.user_id);
            ViewBag.task_id = new SelectList(db.TaskTables, "task_id", "title", codeTable.task_id);
            return View(codeTable);
        }

        // GET: Code/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTable codeTable = db.CodeTables.Find(id);
            if (codeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", codeTable.user_id);
            ViewBag.task_id = new SelectList(db.TaskTables, "task_id", "title", codeTable.task_id);
            return View(codeTable);
        }

        // POST: Code/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "task_id,line_no,user_id,text")] CodeTable codeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "Email", codeTable.user_id);
            ViewBag.task_id = new SelectList(db.TaskTables, "task_id", "title", codeTable.task_id);
            return View(codeTable);
        }

        // GET: Code/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeTable codeTable = db.CodeTables.Find(id);
            if (codeTable == null)
            {
                return HttpNotFound();
            }
            return View(codeTable);
        }

        // POST: Code/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeTable codeTable = db.CodeTables.Find(id);
            db.CodeTables.Remove(codeTable);
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
