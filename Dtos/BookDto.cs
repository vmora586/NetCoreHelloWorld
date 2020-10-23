using System.ComponentModel.DataAnnotations;

namespace BooksApi.Dtos
{
    public class BookDto
    {
        public int id { get; set; }
        [Required]
        public string Tittle { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}