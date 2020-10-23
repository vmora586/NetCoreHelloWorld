using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Dtos
{
    public class AuthorDto
    {
        public int id { get; set; } 
        [Required]
        public string name { get; set; }
        public string code { get; set; }
        public DateTime birthDate { get; set; }
        public List<BookDto> Books { get; set; }
    }
}
