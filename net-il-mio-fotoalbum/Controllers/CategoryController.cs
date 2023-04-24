using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var ctx = new PhotoContext();
            var categories = ctx.Categories.ToArray();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            using var ctx = new PhotoContext();

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            ctx.Categories.Add(category);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            using var ctx = new PhotoContext();

            var cat = ctx.Categories.FirstOrDefault(c => c.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Category category)
        {
            using var ctx = new PhotoContext();
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var catToUpdate = ctx.Categories.FirstOrDefault(c => c.Id == id);

            if (catToUpdate == null)
            {
                return NotFound();
            }

            catToUpdate.Name = category.Name;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using var ctx = new PhotoContext();

            var catToDelete = ctx.Categories.FirstOrDefault(c => c.Id == id);

            if (catToDelete is null)
            {
                return View("NotFound");
            }

            ctx.Categories.Remove(catToDelete);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
