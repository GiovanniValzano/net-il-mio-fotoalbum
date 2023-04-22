using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;

        public PhotoController(ILogger<PhotoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Portfolio()
        {
            using var ctx = new PhotoContext();
            var photos = ctx.Photos.Include(p => p.Categories).ToArray();

            return View(photos);
        }

        public IActionResult Detail(int id)
        {
            using var ctx = new PhotoContext();
            var photo = ctx.Photos.Include(p => p.Categories).SingleOrDefault(p => p.Id == id);

            if (photo is null)
            {
                return NotFound($"Foto con id {id} non trovata.");
            }

            return View(photo);
        }

        public IActionResult Create()
        {
            using var ctx = new PhotoContext();
            var formModel = new PhotoFormModel
            {
                Categories = ctx.Categories.ToArray(),
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel form)
        {
            using var ctx = new PhotoContext();

            if (!ModelState.IsValid)
            {
                form.Categories = ctx.Categories.ToArray();

                return View(form);
            }

            form.Photo.Categories = ctx.Categories.Where(c => form.SelectedCategoriesIds.Contains(c.Id)).ToList();

            ctx.Photos.Add(form.Photo);
            ctx.SaveChanges();

            return RedirectToAction("Portfolio");
        }
    }
}
