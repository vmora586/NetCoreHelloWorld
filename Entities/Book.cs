using System.ComponentModel.DataAnnotations;
using BooksApi.Entities;

namespace Entities
{
    public class Book
    {
        public int id { get; set; }
        [Required]
        public string Tittle { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Author Author {get; set;}
    }
}