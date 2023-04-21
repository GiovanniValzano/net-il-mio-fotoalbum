using Azure;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoFormModel
    {
        public Photo Photo { get; set; } = new Photo();
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
        public List<int> SelectedCategoriesIds { get; set; } = new();
    }
}
