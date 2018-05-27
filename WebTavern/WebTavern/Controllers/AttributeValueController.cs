using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTavern.Models;
using WebTavern.Models.EFModels;

namespace WebTavern.Controllers
{
    public class AttributeValueController : Controller
    {
        private TavernContext db = new TavernContext();

        // GET: AttributeValue
        public ActionResult Index()
        {
            var attributeValues = db.AttributeValues.Include(a => a.Attribute).Include(a => a.Ingredient);
            return View(attributeValues.ToList());
        }

        // GET: AttributeValue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            if (attributeValue == null)
            {
                return HttpNotFound();
            }
            return View(attributeValue);
        }

        // GET: AttributeValue/Create
        public ActionResult Create()
        {
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name");
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name");
            return View();
        }

        // POST: AttributeValue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IngredientId,AttributeId,Value")] AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                db.AttributeValues.Add(attributeValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name", attributeValue.AttributeId);
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", attributeValue.IngredientId);
            return View(attributeValue);
        }

        // GET: AttributeValue/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            if (attributeValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name", attributeValue.AttributeId);
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", attributeValue.IngredientId);
            return View(attributeValue);
        }

        // POST: AttributeValue/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IngredientId,AttributeId,Value")] AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributeValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "Name", attributeValue.AttributeId);
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", attributeValue.IngredientId);
            return View(attributeValue);
        }

        // GET: AttributeValue/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            if (attributeValue == null)
            {
                return HttpNotFound();
            }
            return View(attributeValue);
        }

        // POST: AttributeValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttributeValue attributeValue = db.AttributeValues.Find(id);
            db.AttributeValues.Remove(attributeValue);
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
