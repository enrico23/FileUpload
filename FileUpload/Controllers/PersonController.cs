using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileUpload.DAL;
using FileUpload.Models;
using System.IO;

namespace FileUpload.Controllers
{
    public class PersonController : Controller
    {
        private PersonDbContext db = new PersonDbContext();
        private static string[] extensions = new[] { "pdf", "doc", "docx" };

        // GET: Person
        public ActionResult Index()
        {
            return View(db.Person.ToList());
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            ApplicationConfiguration _config = new ApplicationConfiguration();
            ViewBag.MaxSize = _config.maxFileSize;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                string cvPath = "";
                string vFolderPath = "";

                var checkperson = db.Person.Where(s => s.Email == model.Email).SingleOrDefault();
                if (checkperson == null)
                {
                    vFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/app_data/upload");
                    cvPath = System.IO.Path.Combine(vFolderPath, model.uploadFile.FileName);

                    Person newPerson = new Person();
                    newPerson.Name = model.Name;
                    newPerson.Surname = model.Surname;
                    newPerson.Email = model.Email;
                    newPerson.FilePath = cvPath;
                    db.Person.Add(newPerson);
                    db.SaveChanges();

                    /* FILE UPLOAD*/

                    if (model.uploadFile != null && model.uploadFile.ContentLength > 0)
                    {

                        /* CHECK EXTENSION */
                        var extension = model.uploadFile.FileName.Split('.').Last();
                        if (!extensions.Any(i => i.Equals(extension, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            throw new Exception("File extension not allowed!");
                            
                        }
                        else
                        {

                            if (!Directory.Exists(vFolderPath))
                            {
                                Directory.CreateDirectory(vFolderPath);
                            }

                            model.uploadFile.SaveAs(cvPath);

                        }

                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = "User already registered";
                    return View(model);
                }


            }

            return View(model);
        } 

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Email,Name,Surname,FilePath")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
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
