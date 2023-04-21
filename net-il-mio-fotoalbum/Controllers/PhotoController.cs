using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

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
                return NotFound($"Post with id {id} not found.");
            }

            return View(photo);
        }
    }
}
