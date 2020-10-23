using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApi.Dtos
{
    public class CreateAuthorDto
    {
        [Required]
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public string code { get; set; }
    }
}