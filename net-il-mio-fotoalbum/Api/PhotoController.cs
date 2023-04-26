using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        // get: /api/Photo[?title=<...>]
        [HttpGet]
        public IActionResult GetPhotos([FromQuery] string? title)
        {
            using var ctx = new PhotoContext();

            var photos = ctx.Photos
                .Where(p => title == null || p.Title.ToLower().Contains(title.ToLower()))
                .ToList();

            return Ok(photos);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeletePhoto(int id)
        {
            using var ctx = new PhotoContext();
            var savedPhoto = ctx.Photos.FirstOrDefault(p => p.Id == id);

            if (savedPhoto is null)
            {
                return NotFound();
            }

            ctx.Photos.Remove(savedPhoto);
            ctx.SaveChanges();

            return Ok();
        }
    }
}
