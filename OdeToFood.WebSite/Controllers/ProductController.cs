using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using OdeToFood.Data.Model;
using System.IO;
using OdeToFood.WebSite.Services;

namespace OdeToFood.WebSite.Controllers
{
    public class ProductController : BaseController
    {
        private readonly BaseDataService<Product> MyContext = new BaseDataService<Product>();
        private readonly RestoDbContext db = new RestoDbContext();
        public ActionResult Index()
        {
            // var products = db.Product.Include(x => x.Artist);
            var products = MyContext.Get(null, null, "Artist");
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("/content/products"), filename);
                    file.SaveAs(path);

                    product.Image = filename;
                    this.CheckAuditPattern(product, true);
                    db.Product.Add(product);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
            }

            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FullName", product.ArtistId);
            ViewBag.MessageDanger = "¡Error al cargar el Producto con su imagén.";
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FullName", product.ArtistId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("/content/products"), filename);
                    file.SaveAs(path);
                    product.Image = filename;
                }
                this.CheckAuditPattern(product, false);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
            }

            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FullName", product.ArtistId);
            ViewBag.MessageDanger = "¡Error al modificar el Producto con su imagén.";
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var product = MyContext.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Product.Remove(product);
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