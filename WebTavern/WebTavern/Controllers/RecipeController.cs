using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTavern.Models;
using WebTavern.Models.EFModels;

namespace WebTavern.Controllers
{
    public class RecipeController : Controller
    {
        private TavernContext db = new TavernContext();

        // GET: Recipe
        public ActionResult Index()
        {
            var recipes = db.Recipes.Include(r => r.Drink).Include(r => r.Ingredient);
            return View(recipes.ToList());
        }

        // GET: Recipe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name");
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name");
            return View();
        }

        // POST: Recipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DrinkId,IngredientId,Amount")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name", recipe.DrinkId);
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", recipe.IngredientId);
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name", recipe.DrinkId);
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", recipe.IngredientId);
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DrinkId,IngredientId,Amount")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name", recipe.DrinkId);
            ViewBag.IngredientId = new SelectList(db.Ingredients, "Id", "Name", recipe.IngredientId);
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipes.Find(id);
            db.Recipes.Remove(recipe);
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
