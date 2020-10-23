using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace BooksApi.Entities
{
    public class Author
    {
        public int id {get; set;}
        
        [Required]
        public string name {get; set;}

        public List<Book> Books{get; set;}

        public DateTime? birthDate {get;set;}
        public string code {get;set;}
    }
}