using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Inserire un nome per la categoria")]
        [StringLength(100, ErrorMessage = "Il nome della categoria deve contenere un massimo di 100 caratteri")]
        public string Name { get; set; } = string.Empty;

        public List<Photo> Categories { get; set;} = new List<Photo>();
    }
}
