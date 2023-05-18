namespace AnkaBetaProject.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
