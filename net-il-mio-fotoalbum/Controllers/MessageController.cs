using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessageController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            using var ctx = new PhotoContext();
            var messages = ctx.Messages.ToArray();
            return View(messages);
        }
        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using var ctx = new PhotoContext();

            var mesToDelete = ctx.Messages.FirstOrDefault(m => m.Id == id);

            if (mesToDelete is null)
            {
                return View("NotFound");
            }

            ctx.Messages.Remove(mesToDelete);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
