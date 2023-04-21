using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Inserire un titolo per la foto")]
        [StringLength(100, ErrorMessage = "Il titolo deve contenere un massimo di 100 caratteri")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Inserire una descrizione per la foto")]
        [Column(TypeName = "text")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Inserire un riferimento per la foto")]
        public string ImgSrc { get; set; } = string.Empty;
        public bool Visible { get; set; } = false;

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
