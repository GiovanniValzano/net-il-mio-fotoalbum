using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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

        public IActionResult Update(int id)
        {
            using var ctx = new PhotoContext();

            var photo = ctx.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

            if (photo == null)
            {
                return NotFound();
            }

            var formModel = new PhotoFormModel
            {
                Photo = photo,  
                Categories = ctx.Categories.ToList(),
                SelectedCategoriesIds = photo.Categories!.Select(t => t.Id).ToList()
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PhotoFormModel form)
        {
            using var ctx = new PhotoContext();

            if (!ModelState.IsValid)
            {
                form.Categories = ctx.Categories.ToArray();

                return View(form);
            }

            var savedPhoto = ctx.Photos.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);

            if (savedPhoto is null)
            {
                return View("NotFound");
            }

            savedPhoto.Title = form.Photo.Title;
            savedPhoto.Description = form.Photo.Description;
            savedPhoto.ImgSrc = form.Photo.ImgSrc;
            savedPhoto.Visible = form.Photo.Visible;
            savedPhoto.Categories = ctx.Categories.Where(t => form.SelectedCategoriesIds.Contains(t.Id)).ToList();

            ctx.SaveChanges();

            return RedirectToAction("Portfolio");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using var ctx = new PhotoContext();

            var photoToDelete = ctx.Photos.FirstOrDefault(p => p.Id == id);

            if (photoToDelete is null)
            {
                return View("NotFound");
            }

            ctx.Photos.Remove(photoToDelete);
            ctx.SaveChanges();

            return RedirectToAction("Portfolio");
        }

    }
}
