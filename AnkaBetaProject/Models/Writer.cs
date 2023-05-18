using System.ComponentModel.DataAnnotations;

namespace AnkaBetaProject.Models
{
    public class Writer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Yazar adı zorunlu bir alandır.")]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }

    public class WriterViewModel
    {
        public int WriterId { get; set; }
        public string Name { get; set; }
    }

    public class WriterCreateModel
    {
        public string Name { get; set; }
    }

    public class WriterUpdateModel
    {
        public string Name { get; set; }
    }
}
