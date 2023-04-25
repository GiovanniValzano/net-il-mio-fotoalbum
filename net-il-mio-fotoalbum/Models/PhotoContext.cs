using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : IdentityDbContext<IdentityUser>

    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-photographer;Integrated Security=True;Encrypt=False");
        }

        public void Seed()
        {
            if(!Photos.Any())
            {
                var seed = new Photo[]
                {
                    new()
                    {
                        Title = "Pace Orientale",
                        Description = "Trasmette pace e serenità attraverso la forme geometriche del terreno e ai colori vivaci della narura al tramonto.",
                        ImgSrc = "/img/sunset.jpg",
                        Visible = true
                    },
                    new()
                    {
                        Title = "The Bridge",
                        Description = "Ritrae l'imponenza dell'opera dell'uomo sulla natura, fa riflettere sul conetto di superamento di un ostacolo infatti come un ponte ci permette di superare un fiume e ci fa raggiungere l'altra sponda, la collaborazione umana e l'avanzare della tecnica ci conduce verso il progresso. ",
                        ImgSrc = "/img/ny-bridge.jpg",
                        Visible = true
                    }
                };

                Photos.AddRange(seed);
            }

            if (!Categories.Any())
            {
                var seed = new Category[]
                {
                    new()
                    {
                        Name = "Paesaggi"
                    },
                    new()
                    {
                        Name = "Ritratti"
                    },
                    new()
                    {
                        Name = "Street photography "
                    }
                };

                Categories.AddRange(seed);
            }

            SaveChanges();
        }
    }
}
