using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Inserire l'email")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Inserire il testo del messaggio")]
        public string TextMessage { get; set; } = string.Empty;
    }
}
