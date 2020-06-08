using SharpGalery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SharpGalery.Controllers
{
    public class ImagesController : Controller
    {

        private ApplicationContext db = new ApplicationContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Models.Image model)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.ImagePath = "~/Img/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Img/"), fileName);
                model.ImageFile.SaveAs(fileName);

                db.Images.Add(model);
                db.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Index");
            }


            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image model)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.ImagePath = "~/Img/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Img/"), fileName);
                model.ImageFile.SaveAs(fileName);

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                Image image = db.Images.Find(id);
                if (image != null)
                {
                    return View(image);
                }
            }

            return RedirectToAction("Add","Images");
           
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}