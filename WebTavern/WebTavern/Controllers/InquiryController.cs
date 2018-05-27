using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebTavern.Models;
using WebTavern.Models.EFModels;
using WebTavern.Models.ViewModels;

namespace WebTavern.Controllers
{
    public class InquiryController : Controller
    {
        private TavernContext db = new TavernContext();

        // GET: Inquiry
        public async Task<ActionResult> Index(double? rating, string sign, string name, int page = 1)
        {
            IQueryable<Drink> drinks = db.Drinks;

            var bingo = (
                from o in db.Drinks
                join d in db.Recipes on o.Id equals d.DrinkId
                where d.Ingredient.Name == "Лимон"
                select o
            ).ToList();

            drinks = bingo.AsQueryable();

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

            var count =  drinks.Count();
            drinks = drinks.OrderBy(p => p.Name);
            var items =  drinks.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Drinks = items
            };
            return View("~/Views/Drink/Index.cshtml", viewModel);
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

            return View("~/Views/Drink/Details.cshtml", drinkDetails);
        }
    }
}