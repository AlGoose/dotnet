using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebTavern.Models;
using WebTavern.Models.EFModels;
using WebTavern.Models.ViewModels;

namespace WebTavern.Controllers
{
    public class DrinkController : Controller
    {
        private TavernContext db = new TavernContext();

        // GET: Drink
        public async Task<ActionResult> Index(double? rating, string sign, string name, int page = 1)
        {
            IQueryable<Drink> drinks = db.Drinks;
            string[] signs = { "=", ">", "<", ">=", "<=" };
            SelectList signsList = new SelectList(signs);
            ViewBag.SignsList = signsList;

            if (rating != null && rating != 0)
            {
                switch (sign)
                {
                    case ">":
                        drinks = drinks.Where(p => p.Rating > rating);
                        break;
                    case "<":
                        drinks = drinks.Where(p => p.Rating < rating);
                        break;
                    case ">=":
                        drinks = drinks.Where(p => p.Rating >= rating);
                        break;
                    case "<=":
                        drinks = drinks.Where(p => p.Rating <= rating);
                        break;
                    default:
                        drinks = drinks.Where(p => p.Rating == rating);
                        break;

                }
            }

            if (!String.IsNullOrEmpty(name))
            {
                drinks = drinks.Where(p => p.Name.Contains(name));
            }

            int pageSize = 10;   // количество элементов на странице
            
            var count = await drinks.CountAsync();
            drinks = drinks.OrderBy(p => p.Name);
            var items = await drinks.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Drinks = items
            };
            return View(viewModel);
        }

        // GET: Drink/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }

            IQueryable<Recipe> recipes = db.Recipes;
            recipes = recipes.Where(p => p.DrinkId == id);

            List<String> ingredients = new List<String>();
            foreach (var item in recipes)
            {
                Ingredient tmp = db.Ingredients.Find(item.IngredientId);
                ingredients.Add(tmp.Name);
            }

            DrinkDetails drinkDetails = new DrinkDetails
            {
                Drink = drink,
                Ingredients = ingredients.ToList()
            };

            return View(drinkDetails);
        }

        // GET: Drink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drink/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Rating")] Drink drink, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {

                if (uploadImage != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    // установка массива байтов
                    drink.Image = imageData;
                }
                db.Drinks.Add(drink);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(drink);
        }

        // GET: Drink/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drink/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Drink drink, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                byte[] image = null;
                Drink tmp = db.Drinks.Find(drink.Id);

                if (uploadImage == null)
                {
                    image = tmp.Image;
                    tmp.Image = image;
                }
                else
                {
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        image = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    tmp.Image = image;
                }
                tmp.Id = drink.Id;
                tmp.Name = drink.Name;
                tmp.Rating = drink.Rating;
                tmp.Ratings = drink.Ratings;
                tmp.Recipes = drink.Recipes;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drink);
        }

        // GET: Drink/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drink drink = db.Drinks.Find(id);
            db.Drinks.Remove(drink);
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
