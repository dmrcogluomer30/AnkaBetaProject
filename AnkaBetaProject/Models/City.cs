using System.ComponentModel.DataAnnotations;

namespace AnkaBetaProject.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Şehir adı zorunlu bir alandır.")]
        public string Name { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
    }
}
