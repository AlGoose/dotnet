using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTavern.Models;
using WebTavern.Models.EFModels;

namespace WebTavern.Controllers
{
    public class TypeAttributeController : Controller
    {
        private TavernContext db = new TavernContext();

        // GET: TypeAttribute
        public ActionResult Index()
        {
            var typeAttributes = db.TypeAttributes.Include(t => t.Attribute).Include(t => t.Type);
            return View(typeAttributes.ToList());
        }

        // GET: TypeAttribute/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAttribute typeAttribute = db.TypeAttributes.Find(id);
            if (typeAttribute == null)
            {
                return HttpNotFound();
            }
            return View(typeAttribute);
        }

        // GET: TypeAttribute/Create
        public ActionResult Create()
        {
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            return View();
        }

        // POST: TypeAttribute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeId,AttributeId")] TypeAttribute typeAttribute)
        {
            if (ModelState.IsValid)
            {
                db.TypeAttributes.Add(typeAttribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name", typeAttribute.AttributeId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", typeAttribute.TypeId);
            return View(typeAttribute);
        }

        // GET: TypeAttribute/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAttribute typeAttribute = db.TypeAttributes.Find(id);
            if (typeAttribute == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name", typeAttribute.AttributeId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", typeAttribute.TypeId);
            return View(typeAttribute);
        }

        // POST: TypeAttribute/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeId,AttributeId")] TypeAttribute typeAttribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeAttribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name", typeAttribute.AttributeId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", typeAttribute.TypeId);
            return View(typeAttribute);
        }

        // GET: TypeAttribute/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAttribute typeAttribute = db.TypeAttributes.Find(id);
            if (typeAttribute == null)
            {
                return HttpNotFound();
            }
            return View(typeAttribute);
        }

        // POST: TypeAttribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeAttribute typeAttribute = db.TypeAttributes.Find(id);
            db.TypeAttributes.Remove(typeAttribute);
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
