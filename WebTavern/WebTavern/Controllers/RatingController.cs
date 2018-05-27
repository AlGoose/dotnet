using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTavern.Models;
using WebTavern.Models.EFModels;

namespace WebTavern.Controllers
{
    public class RatingController : Controller
    {
        private TavernContext db = new TavernContext();

        // GET: Rating
        public ActionResult Index()
        {
            var ratings = db.Ratings.Include(r => r.Drink);
            return View(ratings.ToList());
        }

        // GET: Rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // GET: Rating/Create
        public ActionResult Create()
        {
            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name");
            return View();
        }

        // POST: Rating/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DrinkId,Value")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Ratings.Add(rating);
                db.SaveChanges();

                double count = 0;
                double result = 0;

                IQueryable<Rating> ratings = db.Ratings;
                ratings = ratings.Where(p => p.DrinkId == rating.DrinkId);

                foreach (var item in ratings)
                {
                    count++;
                    result += item.Value;
                }

                Drink drink = db.Drinks.Find(rating.DrinkId);
                double ready = Math.Round(result / count, 1);
                drink.Rating = ready;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name", rating.DrinkId);
            return View(rating);
        }

        // GET: Rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name", rating.DrinkId);
            return View(rating);
        }

        // POST: Rating/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DrinkId,Value")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DrinkId = new SelectList(db.Drinks, "Id", "Name", rating.DrinkId);
            return View(rating);
        }

        // GET: Rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
            db.SaveChanges();

            double count = 0;
            double result = 0;

            IQueryable<Rating> ratings = db.Ratings;
            ratings = ratings.Where(p => p.DrinkId == rating.DrinkId);

            foreach (var item in ratings)
            {
                count++;
                result += item.Value;
            }

            Drink drink = db.Drinks.Find(rating.DrinkId);
            double ready = Math.Round(result / count, 1);
            drink.Rating = ready;

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
