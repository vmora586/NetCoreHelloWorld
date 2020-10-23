using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Dtos
{
    public class UpdateAuthorDto
    {
        [Required]
        public string name { get; set; }
        public DateTime birthDate { get;set; }
    }
}