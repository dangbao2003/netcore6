using BanSachWeb.Data;
using BanSachWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanSachWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category objcategory)
        {
            if (objcategory.Name == objcategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name must not same displayorder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(objcategory);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Create Sucsessfully";
                return RedirectToAction("index");
            }
            return View(objcategory);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            var categoryfromDb = _db.Categories.Find(id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category objcategory)
        {
            if (objcategory.Name == objcategory.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name must not same displayorder");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(objcategory);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Update Sucsessfully";
                return RedirectToAction("index");
            }
            return View(objcategory);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromDb = _db.Categories.Find(id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            return View(categoryfromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["Sucess"] = "Category Delete Sucsessfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }
    }
}
