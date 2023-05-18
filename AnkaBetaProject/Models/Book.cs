using System.ComponentModel.DataAnnotations;

namespace AnkaBetaProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kitap adı zorunlu bir alandır.")]
        public string Title { get; set; }
        public int WriterId { get; set; }
        public virtual Writer Writer { get; set; }

        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }
    }

    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int WriterId { get; set; }
        public string WriterName { get; set; }
        public int LibraryId { get; set; }
        public string LibraryName { get; set; }
    }

    public class BookCreateModel
    {
        public string Title { get; set; }
        public int WriterId { get; set; }
        public int LibraryId { get; set; }
    }

    public class BookUpdateModel
    {
        public string Title { get; set; }
        public int? WriterId { get; set; }
        public int? LibraryId { get; set; }
    }
}
